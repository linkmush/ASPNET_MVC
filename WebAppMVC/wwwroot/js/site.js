const toggleMenu = () => {
    const menu = document.getElementById('menu');
    const accountButtons = document.getElementById('account-buttons');

    menu.classList.toggle('hide');
    accountButtons.classList.toggle('hide');
    document.body.classList.toggle('menu-open');
};

const checkScreenSize = () => {
    if (window.innerWidth >= 1200) {
        document.getElementById('menu').classList.remove('hide');
        document.getElementById('account-buttons').classList.remove('hide');
    } else {
        document.getElementById('menu').classList.add('hide');
        document.getElementById('account-buttons').classList.add('hide');
    }
};

window.addEventListener('resize', checkScreenSize);
checkScreenSize();


document.addEventListener('DOMContentLoaded', function () {
    let sw = document.querySelector('#switch-mode')

    sw.addEventListener('change', function () {
        let theme = this.checked ? "dark" : "light"

        fetch(`/settings/ChangeTheme?mode=${theme}`)
            .then(res => {
                if (res.ok)
                    window.location.reload()
                else
                    console.log('did not work')
            })
    })
})

document.addEventListener('DOMContentLoaded', function () {

    handleProfileImageUpload()


})

function handleProfileImageUpload() {

    try {

        let fileUploader = document.querySelector('#fileUploader')

        if (fileUploader != undefined) {

            fileUploader.addEventListener('change', function () {
                if (this.files.length > 0)
                    this.form.submit()
            })
        }
    }
    catch { }
}

document.addEventListener('DOMContentLoaded', function () {
    select();
    searchQuery();
})

function select() {
    try {
        let select = document.querySelector('.select')
        let selected = select.querySelector('.selected')
        let selectOptions = select.querySelector('.select-options')

        selected.addEventListener('click', function () {
            selectOptions.style.display = (selectOptions.style.display === 'block') ? 'none' : 'block'
        })

        let options = selectOptions.querySelectorAll('.option')
        options.forEach(function (option) {
            option.addEventListener('click', function () {
                selected.innerHTML = this.textContent
                selectOptions.style.display = 'none'
                let category = this.getAttribute('data-value')
                selected.setAttribute('data-value', category)
                updateCourseByFilter()
            })
        })
    }
    catch { }
}

function searchQuery() {
    try {
        document.querySelector('#searchQuery').addEventListener('keyup', function () {
            updateCourseByFilter()
        })
    }
    catch { }
}

function updateCourseByFilter() {
    const category = document.querySelector('.select .selected').getAttribute('data-value') || 'all'
    const searchQuery = document.querySelector('#searchQuery').value
    const url = `/courses?category=${encodeURIComponent(category)}&searchQuery=${encodeURIComponent(searchQuery)}`

    fetch(url)
        .then(res => res.text())
        .then(data => {
            const parser = new DOMParser()
            const dom = parser.parseFromString(data, 'text/html')
            document.querySelector('.courses-square').innerHTML = dom.querySelector('.courses-square').innerHTML

            const pagination = dom.querySelector('.pagination') ? dom.querySelector('.pagination').innerHTML : ''
            document.querySelector('.pagination').innerHTML = pagination
        })
}

function removeCourseFromSavedUI(courseId) {
    const savedCourseElements = document.querySelectorAll(`.box-savedcourses .grid-item[data-courseid="${courseId}"]`);

    savedCourseElements.forEach(element => {
        const tooltip = bootstrap.Tooltip.getInstance(element.querySelector('[data-bs-toggle="tooltip"]'));
        if (tooltip) {
            tooltip.dispose();
        }

        element.remove();
    });

    reinitializeTooltips();
}

function reinitializeTooltips() {
    const tooltipTriggerList = document.querySelectorAll('[data-bs-toggle="tooltip"]');
    tooltipTriggerList.forEach(tooltipTriggerEl => {
        if (!bootstrap.Tooltip.getInstance(tooltipTriggerEl)) {
            new bootstrap.Tooltip(tooltipTriggerEl);
        }
    });
}

function saveCourseToLocalStorage(courseId) {
    let savedCourses = JSON.parse(localStorage.getItem('savedCourses')) || [];

    if (!savedCourses.includes(courseId)) {
        savedCourses.push(courseId);
        localStorage.setItem('savedCourses', JSON.stringify(savedCourses));
    }
}

document.addEventListener('DOMContentLoaded', function () {
    reinitializeTooltips();

    const savedCourses = JSON.parse(localStorage.getItem('savedCourses')) || [];

    savedCourses.forEach(courseId => {
        const savedCourseElement = document.querySelector(`.grid-item[data-courseid="${courseId}"] .button-top`);

        if (savedCourseElement) {
            savedCourseElement.classList.add('saved');
        }
    });

    document.addEventListener('click', function (e) {
        if (e.target && e.target.matches("a.button-top, a.button-top i")) {
            e.preventDefault();

            var link = e.target.closest("a.button-top");
            var courseId = parseInt(link.getAttribute('data-courseid'), 10);
            console.log("Course ID:", courseId);

            if (link.classList.contains('saved')) {
                fetch(`/Courses/DeleteSavedCourse`, {
                    method: 'POST',
                    headers: {
                        'Content-Type': 'application/json'
                    },
                    body: JSON.stringify({ CourseId: courseId })
                })
                    .then(response => response.json())
                    .then(data => {
                        if (data.success) {
                            link.classList.remove('saved');
                            removeCourseFromSavedUI(courseId);

                            let savedCourses = JSON.parse(localStorage.getItem('savedCourses')) || [];
                            const index = savedCourses.indexOf(courseId);
                            if (index > -1) {
                                savedCourses.splice(index, 1);
                                localStorage.setItem('savedCourses', JSON.stringify(savedCourses));
                            }
                        } else {
                            console.log("Error deleting course.");
                        }
                    })
                    .catch(error => {
                        console.log("Error deleting course:", error);
                    });
            } else {
                fetch('/Courses/SaveCourse', {
                    method: 'POST',
                    headers: {
                        'Content-Type': 'application/json'
                    },
                    body: JSON.stringify({ CourseId: courseId })
                })
                    .then(response => response.json())
                    .then(data => {
                        if (data.success) {
                            saveCourseToLocalStorage(courseId);
                            link.classList.add('saved');
                            reinitializeTooltips();
                        } else {
                            console.log("Error saving course.");
                        }
                    })
                    .catch(error => {
                        console.log("Error saving course:", error);
                    });
            }
        }
    });
});

document.getElementById('deleteAllBtn').addEventListener('click', function (e) {
    e.preventDefault();

    var form = document.createElement('form');
    form.method = 'post';
    form.action = '/Account/Saved';

    document.body.appendChild(form);
    form.submit();
});
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
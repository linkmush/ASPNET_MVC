using Infrastructure.Dtos;
using Infrastructure.Entities;

namespace WebAppMVC.ViewModels.Views;

public class SavedCoursesViewModel
{
    public ProfileInfoViewModel? ProfileInfo { get; set; }
    public List<CourseDto>? SavedCourses { get; set; }
}

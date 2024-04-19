using Infrastructure.Models;

namespace WebAppMVC.ViewModels.Views;

public class CourseViewModel
{
    public IEnumerable<Category>? Categories { get; set; }
    public IEnumerable<Course>? Courses { get; set; }
    public Pagination? Pagination { get; set; }
}

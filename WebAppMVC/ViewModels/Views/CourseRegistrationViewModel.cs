using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace WebAppMVC.ViewModels.Views;

public class CourseRegistrationViewModel
{
    [Required]
    [Display(Name = "Title")]
    public string Title { get; set; } = null!;

    [Display(Name = "Price")]
    public string? Price { get; set; }

    [Display(Name = "Discount price")]
    public string? DiscountPrice { get; set; }

    [Display(Name = "Hours")]
    public string? Hours { get; set; }

    [Display(Name = "Is a best seller")]
    public bool IsBestSeller { get; set; } = false;

    [Display(Name = "Likes in Numbers")]
    public string? LikesInNumbers { get; set; }

    [Display(Name = "Likes in Points")]
    public string? LikesInPoints { get; set; }

    [Display(Name = "Author(s)")]
    public string? Author { get; set; }
}

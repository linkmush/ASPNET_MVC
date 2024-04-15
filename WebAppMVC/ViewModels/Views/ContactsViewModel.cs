using System.ComponentModel.DataAnnotations;

namespace WebAppMVC.ViewModels.Views;

public class ContactsViewModel
{
    [DataType(DataType.Text)]
    [Display(Name = "Full name", Prompt = "Enter your full name", Order = 0)]
    [Required(ErrorMessage = "Full name is required")]
    [MinLength(2, ErrorMessage = "Full name should be at least 5 characters")]
    public string FullName { get; set; } = null!;

    [Display(Name = "Email address", Prompt = "Enter your email address", Order = 2)]
    [DataType(DataType.EmailAddress)]
    [Required(ErrorMessage = "Email is required")]
    [RegularExpression("^[^@\\s]+@[^@\\s]+\\.[^@\\s]{2,}$", ErrorMessage = "Your email address is invalid")]
    public string Email { get; set; } = null!;

    [Display(Name = "Service")]
    public string? Service { get; set; }

    [Display(Name = "Message", Prompt = "Enter your message here...")]
    [Required(ErrorMessage = "Message is required")]
    public string Message { get; set; } = null!;
}

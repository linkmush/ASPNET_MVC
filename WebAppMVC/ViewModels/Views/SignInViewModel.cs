using Infrastructure.Models;
using System.ComponentModel.DataAnnotations;

namespace WebAppMVC.ViewModels.Views;

public class SignInViewModel
{
    public string Title { get; set; } = "Sign in";
    public string? ErrorMessage { get; set; }

    [Display(Name = "Email address", Prompt = "Enter your email address", Order = 3)]
    [DataType(DataType.EmailAddress)]
    [Required(ErrorMessage = "Email is required")]
    public string Email { get; set; } = null!;

    [Display(Name = "Password", Prompt = "Enter your password", Order = 4)]
    [DataType(DataType.Password)]
    [Required(ErrorMessage = "Password is required")]
    public string Password { get; set; } = null!;

    [Display(Name = "Remember me", Order = 5)]
    public bool RememberMe { get; set; }
}

using System.ComponentModel.DataAnnotations;

namespace Infrastructure.Models;

public class SecurityModel
{
    [Display(Name = "Current Password", Prompt = "Enter your current password", Order = 0)]
    [DataType(DataType.Password)]
    [Required(ErrorMessage = "Password is required")]
    public string CurrentPassword { get; set; } = null!;

    [Display(Name = "New Password", Prompt = "Enter your new password", Order = 1)]
    [DataType(DataType.Password)]
    [Required(ErrorMessage = "Password is required")]
    [RegularExpression("^(?=.*[a-z])(?=.*[A-Z])(?=.*\\d)(?=.*[@$!%*?&])[A-Za-z\\d@$!%*?&]{8,}$", ErrorMessage = "Invalid password, must be stronger.")]
    public string NewPassword { get; set; } = null!;

    [Display(Name = "Confirm password", Prompt = "Confirm your password", Order = 2)]
    [DataType(DataType.Password)]
    [Required(ErrorMessage = "Password must be confirmed")]
    [Compare(nameof(NewPassword), ErrorMessage = "Password does not match")]
    public string ConfirmPassword { get; set; } = null!;
}

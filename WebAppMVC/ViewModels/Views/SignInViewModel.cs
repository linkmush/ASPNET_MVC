using Infrastructure.Models;

namespace WebAppMVC.ViewModels.Views;

public class SignInViewModel
{
    public string Title { get; set; } = "Sign in";
    public SignInModel Form { get; set; } = new SignInModel();
    public string? ErrorMessage { get; set; }
}

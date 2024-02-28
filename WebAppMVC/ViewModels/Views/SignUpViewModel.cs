using WebAppMVC.Models;

namespace WebAppMVC.ViewModels.Views;

public class SignUpViewModel
{
    public string Title { get; set; } = "Sign up";
    public SignUpModel Form { get; set; } = new SignUpModel();

}

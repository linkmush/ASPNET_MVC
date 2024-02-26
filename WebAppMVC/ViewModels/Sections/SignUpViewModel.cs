using WebAppMVC.Models;

namespace WebAppMVC.ViewModels.Sections;

public class SignUpViewModel
{
    public string Title { get; set; } = "Sign up";
    public SignUpModel Form { get; set; } = new SignUpModel();

}

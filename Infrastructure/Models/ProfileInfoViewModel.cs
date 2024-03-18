namespace WebAppMVC.ViewModels.Views;

public class ProfileInfoViewModel
{
    public string? ProfileImage { get; set; }
    public string FirstName { get; set; } = null!;
    public string LastName { get; set; } = null!;
    public string Email { get; set; } = null!;
    public bool IsExternalAccount { get; set; }
}

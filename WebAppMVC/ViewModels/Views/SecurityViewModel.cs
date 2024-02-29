using WebAppMVC.Models;

namespace WebAppMVC.ViewModels.Views;

public class SecurityViewModel
{
    public string Title { get; set; } = "Security";
    public SecurityModel Security { get; set; } = new SecurityModel();
    public DeleteAccountModel DeleteAccount { get; set; } = new DeleteAccountModel();
    public AccountDetailsBasicInfoModel BasicInfo { get; set; } = new AccountDetailsBasicInfoModel()
    {
        ProfileImage = "Images/icon-picture.svg",
        FirstName = "Oskar",
        LastName = "Lindqvist",
        Email = "Oskar@domain.com"
    };
}

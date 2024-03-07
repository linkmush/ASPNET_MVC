using Infrastructure.Models;

namespace WebAppMVC.ViewModels.Views;

public class SecurityViewModel
{
    public string Title { get; set; } = "Security";
    public SecurityModel? Security { get; set; }
    public DeleteAccountModel? DeleteAccount { get; set; }
    public AccountDetailsBasicInfoModel BasicInfo { get; set; } = new AccountDetailsBasicInfoModel
    {
        ProfileImage = "Images/icon-picture.svg",
        FirstName = "Oskar",
        LastName = "Lindqvist",
        Email = "Oskar@domain.com"
    };
}

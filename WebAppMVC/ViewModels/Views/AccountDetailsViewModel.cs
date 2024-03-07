using Infrastructure.Models;

namespace WebAppMVC.ViewModels.Views;

public class AccountDetailsViewModel
{
    public string Title { get; set; } = "Account Details";
    public AccountDetailsBasicInfoModel BasicInfo { get; set; } = new AccountDetailsBasicInfoModel()
    {
        ProfileImage = "Images/icon-picture.svg",
        FirstName = "Oskar",
        LastName = "Lindqvist",
        Email = "Oskar@domain.com"
    };
    public AccountDetailsAddressInfoModel AddressInfo { get; set; } = new AccountDetailsAddressInfoModel();
}

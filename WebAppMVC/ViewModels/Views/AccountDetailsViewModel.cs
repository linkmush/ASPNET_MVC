using Infrastructure.Models;

namespace WebAppMVC.ViewModels.Views;

public class AccountDetailsViewModel
{
    public ProfileInfoViewModel? ProfileInfo { get; set; }

    public AccountDetailsBasicInfoModel? BasicInfo { get; set; }

    public AccountDetailsAddressInfoModel? AddressInfo { get; set; }
}

using Infrastructure.Models;

namespace WebAppMVC.ViewModels.Views;

public class SecurityViewModel
{
    public string Title { get; set; } = "Security";
    public SecurityModel? Security { get; set; }
    public DeleteAccountModel? DeleteAccount { get; set; }
    public ProfileInfoViewModel? ProfileInfo { get; set; }
}

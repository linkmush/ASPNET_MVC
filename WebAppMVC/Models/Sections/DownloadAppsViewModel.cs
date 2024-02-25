using WebAppMVC.Models.Components;

namespace WebAppMVC.Models.Sections;

public class DownloadAppsViewModel
{
    public string Id { get; set; } = null!;
    public ImageViewModel PhoneApp { get; set; } = new ImageViewModel();
    public string Title { get; set; } = null!;
    public List<AppViewModel>? Apps { get; set; } 
    public ImageViewModel AppStore { get; set; } = new ImageViewModel();
    public ImageViewModel GooglePlay { get; set; } = new ImageViewModel();
}

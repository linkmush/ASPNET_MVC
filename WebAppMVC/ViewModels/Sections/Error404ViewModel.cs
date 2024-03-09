using WebAppMVC.ViewModels.Components;

namespace WebAppMVC.ViewModels.Sections;

public class Error404ViewModel
{
    public string? Id { get; set; }
    public ImageViewModel ErrorImage { get; set; } = new ImageViewModel();
    public string? Title { get; set; }
    public string? Text { get; set; }
    public LinkViewModel ErrorLink { get; set; } = new LinkViewModel();
}

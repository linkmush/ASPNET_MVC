using WebAppMVC.ViewModels.Components;

namespace WebAppMVC.ViewModels.Sections;

public class ShowcaseViewModel
{
    public string? Id { get; set; }
    public ImageViewModel ShowcaseImage { get; set; } = null!;
    public string? Title { get; set; }
    public string? Text { get; set; }
    public string? BrandsText { get; set; }
    public List<ImageViewModel>? Brands { get; set; }
    public LinkViewModel Link { get; set; } = new LinkViewModel();
}

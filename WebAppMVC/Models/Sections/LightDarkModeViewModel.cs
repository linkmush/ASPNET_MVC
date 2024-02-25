using WebAppMVC.Models.Components;

namespace WebAppMVC.Models.Sections;

public class LightDarkModeViewModel
{
    public string? Id { get; set; }
    public string? Title { get; set; }
    public string? TextTitle { get; set; }
    public ImageViewModel SliderButton { get; set; } = new ImageViewModel();
    public ImageViewModel DarkModeImage { get; set; } = new ImageViewModel();
    public ImageViewModel LightModeImage { get; set; } = new ImageViewModel();

}

using WebAppMVC.Models.Components;

namespace WebAppMVC.Models.Sections;

public class FeaturesViewModel
{
    public string? Id { get; set; }
    public string? Title { get; set; }
    public string? Text { get; set; }
    public List<FeatureItemViewModel> Tools { get; set; } = new List<FeatureItemViewModel>();
}

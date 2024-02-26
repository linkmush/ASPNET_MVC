using WebAppMVC.ViewModels.Components;

namespace WebAppMVC.ViewModels.Sections;

public class IntegrateToolsViewModel
{
    public string? Id { get; set; }
    public string? Title { get; set; }
    public string? Text { get; set; }
    public List<ImageViewModel>? ToolList { get; set; }
}

namespace WebAppMVC.ViewModels.Components;

public class LinkViewModel
{
    public string ControllerName { get; set; } = null!;
    public string ActionName { get; set; } = null!;
    public string LinkText { get; set; } = null!;
    public string? IconClass { get; set; }
}

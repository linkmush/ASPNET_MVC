using WebAppMVC.ViewModels.Sections;

namespace WebAppMVC.ViewModels.Views;

public class ErrorViewModel
{
    public Error404ViewModel Error { get; set; } = new Error404ViewModel()
    {
        Id = "Error404",
        ErrorImage = new() { ImageUrl = "Images/404.svg", AltText = "Error404 Image" },
        Title = "Ooops!",
        Text = "The page you are looking for is not available.",
        ErrorLink = new() { ControllerName = "Home", ActionName = "Index", LinkText = "Go to homepage" }
    };
}

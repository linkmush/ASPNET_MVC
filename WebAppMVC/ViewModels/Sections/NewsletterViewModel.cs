using System.ComponentModel.DataAnnotations;
using WebAppMVC.ViewModels.Components;

namespace WebAppMVC.ViewModels.Sections;

public class NewsletterViewModel
{
    public string? Id { get; set; }
    public string? Title { get; set; }
    public string? Text { get; set; }
    public ImageViewModel Arrow { get; set; } = new ImageViewModel();
    public RegisterNewsletterViewModel Newsletter { get; set; } = new RegisterNewsletterViewModel();
    public LinkViewModel Link { get; set; } = new LinkViewModel();
    public string? ErrorMessage { get; set; }
}

using System.ComponentModel.DataAnnotations;
using WebAppMVC.ViewModels.Components;

namespace WebAppMVC.ViewModels.Sections;

public class NewsletterViewModel
{
    [Display(Name = "Daily Newsletter", Order = 0)]
    public bool DailyNewsletter { get; set; }

    [Display(Name = "Advertising Updates", Order = 1)]
    public bool AdvertisingUpdates { get; set; }

    [Display(Name = "Week in Review", Order = 2)]
    public bool WeekInReview { get; set; }

    [Display(Name = "Event Updates", Order = 3)]
    public bool EventUpdates { get; set; }

    [Display(Name = "Startups Weekly", Order = 4)]     /*Jag måste ha samma properties i min viewmodel som i min DTO på webapi för att få det att funka. Men det är okej att ha flera properties i min viewmodel som inte finns med i min DTO på webapiet.*/
    public bool StartupsWeekly { get; set; }

    [Display(Name = "Podcasts", Order = 5)]
    public bool Podcasts { get; set; }

    [Display(Name = "Email address", Prompt = "Enter your email address", Order = 6)]
    [DataType(DataType.EmailAddress)]
    [Required(ErrorMessage = "Email is required")]
    [RegularExpression("^[^@\\s]+@[^@\\s]+\\.[^@\\s]{2,}$", ErrorMessage = "Your email address is invalid")]
    public string Email { get; set; } = null!;

    public string? Id { get; set; }
    public string? Title { get; set; }
    public string? Text { get; set; }
    public ImageViewModel Arrow { get; set; } = new ImageViewModel();
    public string? ErrorMessage { get; set; }
}

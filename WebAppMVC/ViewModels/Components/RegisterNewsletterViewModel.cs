using System.ComponentModel.DataAnnotations;

namespace WebAppMVC.ViewModels.Components;

public class RegisterNewsletterViewModel
{
    [Display(Name = "Daily Newsletter", Order = 0)]
    public bool DailyNewsletter { get; set; }

    [Display(Name = "Advertising Updates", Order = 1)]
    public bool AdvertisingUpdates { get; set; }

    [Display(Name = "Week in Review", Order = 3)]
    public bool WeekInReview { get; set; }

    [Display(Name = "Event Updates", Order = 4)]
    public bool EventUpdates { get; set; }

    [Display(Name = "Startups Weekly", Order = 5)]
    public bool StartupsWeekly { get; set; }

    [Display(Name = "Podcasts", Order = 6)]
    public bool Podcasts { get; set; }

    [Display(Name = "Email address", Prompt = "Your Email", Order = 7)]
    [DataType(DataType.EmailAddress)]
    [Required(ErrorMessage = "Email is required")]
    [RegularExpression("^[^@\\s]+@[^@\\s]+\\.[^@\\s]{2,}$", ErrorMessage = "Your email address is invalid")]
    public string Email { get; set; } = null!;
}

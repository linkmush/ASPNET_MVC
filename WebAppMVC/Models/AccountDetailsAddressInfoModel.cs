using System.ComponentModel.DataAnnotations;

namespace WebAppMVC.Models;

public class AccountDetailsAddressInfoModel
{
    [Display(Name = "Addressline_1", Prompt = "Enter your address", Order = 0)]
    [Required(ErrorMessage = "Address is required")]
    public string AddressLine_1 { get; set; } = null!;

    [Display(Name = "AddressLine_2", Prompt = "Enter your address", Order = 1)]
    public string? AddressLine_2 { get; set; }

    [Display(Name = "Postal Code", Prompt = "Enter your postal code", Order = 2)]
    [DataType(DataType.PostalCode)]
    [Required(ErrorMessage = "Postal code is required")]
    public string PostalCode { get; set; } = null!;

    [Display(Name = "City", Prompt = "Enter your city", Order = 3)]
    [Required(ErrorMessage = "City is required")]
    public string City { get; set; } = null!;
}

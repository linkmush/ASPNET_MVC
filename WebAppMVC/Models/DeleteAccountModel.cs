using System.ComponentModel.DataAnnotations;
using WebAppMVC.Helpers;

namespace WebAppMVC.Models;

public class DeleteAccountModel
{
    [Display(Name = "Delete Account")]
    [CheckBoxRequired(ErrorMessage = "You must confirm to delete your account.")]
    public bool ConfirmDelete { get; set; } = false;
}

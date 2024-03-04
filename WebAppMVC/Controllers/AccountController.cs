using Microsoft.AspNetCore.Mvc;
using WebAppMVC.Models;
using WebAppMVC.ViewModels.Views;

namespace WebAppMVC.Controllers;

public class AccountController : Controller
{
    //private readonly AccountService _accountService;

    //public AccountController(AccountService accountService)
    //{
    //    _accountService = accountService;
    //}

    [Route("/account")]
    public IActionResult Details()
    {
        var viewModel = new AccountDetailsViewModel();
        //viewModel.BasicInfo = _accountService.GetBasicInfo();
        //viewModel.AddressInfo = _accountService.GetAddressinfo();

        return View(viewModel);
    }

    [HttpPost]
    public IActionResult BasicInfo(AccountDetailsViewModel viewModel)
    {
        //_accountService.SaveBasicInfo(viewModel.BasicInfo);

        return RedirectToAction(nameof(Details));
    }

    [HttpPost]
    public IActionResult AddressInfo(AccountDetailsViewModel viewModel)
    {
        //_accountService.SaveAddressInfo(viewModel.AddressInfo);

        return RedirectToAction(nameof(Details), viewModel);
    }

    [HttpGet]
    [Route("/account/security")]
    public IActionResult Security()
    {
        var viewModel = new SecurityViewModel();
        return View(viewModel);
    }

    [HttpPost]
    public IActionResult ChangePassword(SecurityViewModel viewModel)
    {
        if (!ModelState.IsValid)
        {
            // Om validering inte funkar, retunera view med validation errors
            return View("Security", viewModel);
        }

        return RedirectToAction(nameof(Security));
    }

    //[HttpPost]
    //public IActionResult Delete(DeleteAccountModel deleteModel)
    //{
    //    if (!ModelState.IsValid)
    //    {
    //        return View("Security", new SecurityViewModel { DeleteAccount = deleteModel });
    //    }

    //    // måste nog ha en service för att testa om det funkar. 

    //    return RedirectToAction("Index", "Home");
    //}
}

using Infrastructure.Entities;
using Infrastructure.Models;
using Infrastructure.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using WebAppMVC.ViewModels.Views;

namespace WebAppMVC.Controllers;

public class AccountController(SignInManager<UserEntity> signInManager, UserManager<UserEntity> userManager, AccountService accountService) : Controller
{
    private readonly SignInManager<UserEntity> _signInManager = signInManager;
    private readonly UserManager<UserEntity> _userManager = userManager;
    private readonly AccountService _accountService = accountService;

    [HttpGet]
    [Route("/account")]
    public async Task<IActionResult> Details()
    {
        if (!_signInManager.IsSignedIn(User))
        {
            return RedirectToAction("SignIn", "Auth");
        }

        var userEntity = await _userManager.GetUserAsync(User);
        var viewModel = new AccountDetailsViewModel()
        {
            User = userEntity!
        };
        //viewModel.BasicInfo = _accountService.GetBasicInfo();
        //viewModel.AddressInfo = _accountService.GetAddressinfo();

        return View(viewModel);
    }

    [HttpPost]
    public async Task<IActionResult> BasicInfo(AccountDetailsViewModel viewModel)
    {
        var result = await _userManager.UpdateAsync(viewModel.User);   // byt ut _userManger mot din service. Det vill säga _accountService
        if (!result.Succeeded)
        {
            ModelState.AddModelError("Failed To Save Data", "Failed to update contact");
            ViewData["ErrorMessage"] = "Failed to save data";
        }

        return RedirectToAction(nameof(Details), viewModel);
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

    [HttpPost]
    public IActionResult Delete(DeleteAccountModel deleteModel)
    {
        if (!ModelState.IsValid)
        {
            return View("Security", new SecurityViewModel { DeleteAccount = deleteModel });
        }

        // måste nog ha en service för att testa om det funkar. 

        return RedirectToAction("Index", "Home");
    }
}

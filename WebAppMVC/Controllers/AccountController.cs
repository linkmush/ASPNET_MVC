using Infrastructure.Entities;
using Infrastructure.Models;
using Infrastructure.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using WebAppMVC.ViewModels.Views;

namespace WebAppMVC.Controllers;

[Authorize]  // kräver att du måste vara inloggad för att se dessa sidor. 
public class AccountController(SignInManager<UserEntity> signInManager, UserManager<UserEntity> userManager, AddressManager AddressManager) : Controller
{
    private readonly SignInManager<UserEntity> _signInManager = signInManager;
    private readonly UserManager<UserEntity> _userManager = userManager;
    private readonly AddressManager _addressManager = AddressManager;

    #region Details
    [HttpGet]
    [Route("/account")]
    public async Task<IActionResult> Details()
    {
        var viewModel = new AccountDetailsViewModel();

        viewModel.ProfileInfo = await PopulateProfileInfoAsync();

        viewModel.BasicInfo ??= await PopulateBasicInfoAsync();

        viewModel.AddressInfo ??= await PopulateAddressInfoAsync();


        return View(viewModel);
    }
    #endregion

    #region [HttpPost] Details
    [HttpPost]
    [Route("/account")]
    public async Task<IActionResult> Details(AccountDetailsViewModel viewModel)
    {
        if (viewModel.BasicInfo != null)
        {
            if (viewModel.BasicInfo.FirstName != null && viewModel.BasicInfo.LastName != null && viewModel.BasicInfo.Email != null)
            {
                var user = await _userManager.GetUserAsync(User);
                if (user != null)
                {
                    user.FirstName = viewModel.BasicInfo.FirstName;
                    user.LastName = viewModel.BasicInfo.LastName;
                    user.Email = viewModel.BasicInfo.Email;
                    user.PhoneNumber = viewModel.BasicInfo.Phone;
                    user.Bio = viewModel.BasicInfo.Biography;

                    var result = await _userManager.UpdateAsync(user);

                    if (result.Succeeded)
                    {
                        ViewData["SuccessMessage"] = "Successfully Saved Data";
                    }
                    else
                    {
                        ModelState.AddModelError("Failed To Save Data", "Failed to update contact");
                        ViewData["ErrorMessage"] = "Failed to save data";
                    }
                }
            }
        }
        if (viewModel.AddressInfo != null)
        {
            if (viewModel.AddressInfo.AddressLine_1 != null && viewModel.AddressInfo.PostalCode != null && viewModel.AddressInfo.City != null)
            {
                var user = await _userManager.GetUserAsync(User);
                if (user != null)
                {
                    var address = await _addressManager.GetAddressAsync(user.Id);
                    if (address != null)
                    {
                        address.AddressLine_1 = viewModel.AddressInfo.AddressLine_1;
                        address.AddressLine_2 = viewModel.AddressInfo.AddressLine_2;
                        address.PostalCode = viewModel.AddressInfo.PostalCode;
                        address.City = viewModel.AddressInfo.City;

                        user.Address = address;

                        var result = await _addressManager.UpdateAddressAsync(address);
                        if (result)
                        {
                            ViewData["SuccessMessage"] = "Successfully Saved Data";
                        }
                        else
                        {
                            ModelState.AddModelError("Failed To Save Data", "Failed to update contact");
                            ViewData["ErrorMessage"] = "Failed to save data";
                        }
                    }
                    else
                    {
                        address = new AddressEntity
                        {
                            AddressLine_1 = viewModel.AddressInfo.AddressLine_1,
                            AddressLine_2 = viewModel.AddressInfo.AddressLine_2,
                            PostalCode = viewModel.AddressInfo.PostalCode,
                            City = viewModel.AddressInfo.City,
                        };

                        user.Address = address;

                        var result = await _addressManager.CreateAddressAsync(address);
                        if (result)
                        {
                            ViewData["SuccessMessage"] = "Successfully Saved Data";
                        }
                        else
                        {
                            ModelState.AddModelError("Failed To Save Data", "Failed to update contact");
                            ViewData["ErrorMessage"] = "Failed to save data";
                        }
                    }
                }
            }
        }
        viewModel.ProfileInfo = await PopulateProfileInfoAsync();

        viewModel.BasicInfo ??= await PopulateBasicInfoAsync();

        viewModel.AddressInfo ??= await PopulateAddressInfoAsync();

        return View(viewModel);
    }
    #endregion

    #region Security
    [HttpGet]
    [Route("/account/security")]
    public async Task<IActionResult> Security()
    {
        var viewModel = new SecurityViewModel();

        viewModel.ProfileInfo = await PopulateProfileInfoAsync();

        return View(viewModel);
    }

    [HttpPost]
    [Route("/account/security")]
    public async Task<IActionResult> Security(SecurityViewModel viewModel)
    {
        if (ModelState.IsValid)
        {
            var user = await _userManager.GetUserAsync(User);
            if (user != null)
            {
                var changePassword = await _userManager.ChangePasswordAsync(user, viewModel.Security!.CurrentPassword, viewModel.Security.NewPassword);
                if (changePassword.Succeeded)
                {
                    ViewData["SuccessMessage"] = "New password created";
                }
                else
                {
                    ModelState.AddModelError("IncorrectValues", "Incorrect password");
                    ViewData["ErrorMessage"] = "Incorrect password, try again.";
                }
            }
        }

        viewModel.ProfileInfo = await PopulateProfileInfoAsync();
        return View(viewModel);
    }
    #endregion

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

    public async Task<ProfileInfoViewModel> PopulateProfileInfoAsync()
    {
        var user = await _userManager.GetUserAsync(User);

        return new ProfileInfoViewModel
        {
            FirstName = user!.FirstName,
            LastName = user.LastName,
            Email = user.Email!,
            IsExternalAccount = user.IsExternalAccount,
        };
    }

    public async Task<AccountDetailsBasicInfoModel> PopulateBasicInfoAsync()
    {
        var user = await _userManager.GetUserAsync(User);

        return new AccountDetailsBasicInfoModel
        {
            UserId = user!.Id,
            FirstName = user.FirstName,
            LastName = user.LastName,
            Email = user.Email!,
            IsExternalAccount = user.IsExternalAccount,
            Phone = user.PhoneNumber,
            Biography = user.Bio,
        };
    }

    public async Task<AccountDetailsAddressInfoModel> PopulateAddressInfoAsync()
    {
        var user = await _userManager.GetUserAsync(User);
        if (user != null)
        {
            var address = await _addressManager.GetAddressAsync(user.Id);
            if (address != null)
            {
                return new AccountDetailsAddressInfoModel
                {
                    AddressLine_1 = address.AddressLine_1,
                    AddressLine_2 = address.AddressLine_2,
                    PostalCode = address.PostalCode,
                    City = address.City,
                    IsExternalAccount = user.IsExternalAccount,
                };
            }
        }

        return new AccountDetailsAddressInfoModel();
    }
}

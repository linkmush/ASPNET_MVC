﻿using Infrastructure.Entities;
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
                    var newAddress = new AddressEntity
                    {
                        AddressLine_1 = viewModel.AddressInfo.AddressLine_1,
                        AddressLine_2 = viewModel.AddressInfo.AddressLine_2,
                        PostalCode = viewModel.AddressInfo.PostalCode,
                        City = viewModel.AddressInfo.City,
                    };

                    var existingAddress = await _addressManager.GetExistingAddressAsync(newAddress);

                    if (existingAddress != null)
                    {
                        user.Address = existingAddress;
                    }
                    else
                    {
                        var result = await _addressManager.CreateAddressAsync(newAddress);
                        if (result)
                        {
                            user.Address = newAddress;
                        }
                        else
                        {
                            ModelState.AddModelError("Failed To Save Data", "Failed to update contact");
                            ViewData["ErrorMessage"] = "Failed to save data";
                        }
                    }

                    var updateResult = await _userManager.UpdateAsync(user);
                    if (updateResult.Succeeded)
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

        viewModel.ProfileInfo = await PopulateProfileInfoAsync();

        viewModel.BasicInfo ??= await PopulateBasicInfoAsync();

        viewModel.AddressInfo ??= await PopulateAddressInfoAsync();

        return View(viewModel);
    }
    #endregion

    #region Security Password
    [HttpGet]
    [Route("/account/security")]
    public async Task<IActionResult> Security(string errorMessage)
    {
        var viewModel = new SecurityViewModel();

        viewModel.ProfileInfo = await PopulateProfileInfoAsync();

        ViewData["ErrorMessage"] = errorMessage;

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

    #region Security Delete
    [HttpPost]
    public async Task<IActionResult> Delete(DeleteAccountModel deleteModel)
    { 
        var user = await _userManager.GetUserAsync(User);
        if (user != null)
        {
            if (deleteModel.ConfirmDelete == true)
            {
                var result = await _userManager.DeleteAsync(user);
                if (result.Succeeded)
                {
                    await _signInManager.SignOutAsync(); // SignOutAsync() tar bort autentiseringscookies, vilket förhindrar obehörig åtkomst även om användarens konto tas bort från databasen.
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    ModelState.AddModelError("DeleteError", "Could not delete account");
                    //ViewData["ErrorMessage"] = "Something went wrong, could not delete account. Contact WebAdmin.";
                    return RedirectToAction("Security", "Account", new { errorMessage = "Something went wrong, could not delete account. Contact WebAdmin." });
                }
            }
            else
            {
                ModelState.AddModelError("DeleteError", "Could not delete account");
                //ViewData["ErrorMessage"] = "Something went wrong, could not delete account. Contact WebAdmin.";
                return RedirectToAction("Security", "Account", new { errorMessage = "You must confirm to delete your account." });
            }
        }

        return RedirectToAction("Security", "Account");
    }
    #endregion

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

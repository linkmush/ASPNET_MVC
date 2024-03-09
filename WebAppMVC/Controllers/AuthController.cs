using Infrastructure.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebAppMVC.ViewModels.Views;

namespace WebAppMVC.Controllers;

public class AuthController : Controller
{

    private readonly UserManager<UserEntity> _userManager;

    public AuthController(UserManager<UserEntity> userManager)
    {
        _userManager = userManager;
    }

    [Route("/signup")]        // route är det som avgör sökvägen i webbläsaren.
    [HttpGet]
    public IActionResult SignUp()
    {
        ViewData["Title"] = "Sign Up";
        var viewModel = new SignUpViewModel();
        return View(viewModel);
    }

    [Route("/signup")]        // route är det som avgör sökvägen i webbläsaren.
    [HttpPost]
    public async Task<IActionResult> SignUp(SignUpViewModel viewModel)
    {
        if (ModelState.IsValid)                                        
        {
            var exists = await _userManager.Users.AnyAsync(x => x.Email == viewModel.Form.Email);
            if (exists)
            {
                ModelState.AddModelError("AlreadyExists", "User with same email already exists");
                ViewData["ErrorMessage"] = "User with same email already exists";
                return View(viewModel);
            }

            var userEntity = new UserEntity    // kan flytta ut denna till en egen factory och hämta in den istället för att mappa upp här.
            {
                FirstName = viewModel.Form.FirstName,
                LastName = viewModel.Form.LastName,
                Email = viewModel.Form.Email,
                UserName = viewModel.Form.Email,
            };

            var result = await _userManager.CreateAsync(userEntity, viewModel.Form.Password);              // detta är om det funkar. 
            if (result.Succeeded)
            {
                return RedirectToAction("SignIn", "Auth");
            } 
        }

        return View(viewModel);                         // om det inte går går vi tillbaks till viewmodel(dvs signup sidan i detta fall)
    }

    [Route("/signin")]
    [HttpGet]
    public IActionResult SignIn()
    {
        ViewData["Title"] = "Sign In";
        var viewModel = new SignInViewModel();
        return View(viewModel);
    }

    [Route("/signin")]
    [HttpPost]
    public IActionResult SignIn(SignInViewModel viewModel)
    {

        if (ModelState.IsValid)
        {
                                                                        // detta är om det funkar. 
                return RedirectToAction("Details", "Account");
        }

        // var result = _authService.SignInAsync(viewModel.Form);
        // if (result)
        //     return RedirectToAction("Account", "Details");
        viewModel.ErrorMessage = "Incorrect email or password";
        return View(viewModel);
    }

    public new IActionResult SignOut()
    {
        return RedirectToAction("Index", "Home");
    }
}

using Infrastructure.Services;
using Microsoft.AspNetCore.Mvc;
using WebAppMVC.ViewModels.Views;

namespace WebAppMVC.Controllers;

public class AuthController(UserService userService) : Controller
{
    private readonly UserService _userService = userService;

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
            var result = await _userService.CreateUserAsync(viewModel.Form);
            if (result.StatusCode == Infrastructure.Models.StatusCode.OK)               // detta är om det funkar. 
                return RedirectToAction("SignIn", "Auth");
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

        if (!ModelState.IsValid)
        {
            return View(viewModel);
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

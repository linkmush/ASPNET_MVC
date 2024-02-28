using Microsoft.AspNetCore.Mvc;
using WebAppMVC.ViewModels.Views;

namespace WebAppMVC.Controllers;

public class AuthController : Controller
{

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
    public IActionResult SignUp(SignUpViewModel viewModel)
    {
        if (!ModelState.IsValid)
            return View(viewModel);

        return RedirectToAction("SignIn", "Auth");
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
        //     return RedirectToAction("Account", "Index");

        viewModel.ErrorMessage = "Incorrect email or password";
        return View(viewModel);
    }

    public new IActionResult SignOut()
    {
        return RedirectToAction("Index", "Home");
    }
}

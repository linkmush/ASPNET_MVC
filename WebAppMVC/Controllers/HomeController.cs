using Microsoft.AspNetCore.Mvc;
using WebAppMVC.ViewModels.Sections;
using WebAppMVC.ViewModels.Views;

namespace WebAppMVC.Controllers;

public class HomeController : Controller
{
    public IActionResult Index()
    {
        var viewModel = new HomeIndexViewModel();

        ViewData["Title"] = viewModel.Title;

        return View(viewModel);
    }

    [HttpPost]
    public IActionResult Newsletter(NewsletterViewModel viewModel)
    {
        if (!ModelState.IsValid)
        {
            return View(viewModel);
        }

        viewModel.ErrorMessage = "Incorrect email";
        return View(viewModel);
    }
}

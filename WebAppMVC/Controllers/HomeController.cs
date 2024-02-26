using Microsoft.AspNetCore.Mvc;
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
}

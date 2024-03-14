using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebAppMVC.ViewModels.Sections;
using WebAppMVC.ViewModels.Views;

namespace WebAppMVC.Controllers;

[Authorize]   // skyddar sidan, skyddar alla actions. Annars sätter man den över den action man vill skydda. kräver att du måste inloggad för att se dessa actions/sidor.
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

    [AllowAnonymous]   // tillåter användare att kolla sidan även om du kör Authorize längst uppe.
    [Route("/error")]
    public IActionResult Error404(int statusCode)
    {
        var viewModel = new ErrorViewModel();
        return View(viewModel);
    }

}

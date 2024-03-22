using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Text;
using WebAppMVC.ViewModels.Sections;
using WebAppMVC.ViewModels.Views;

namespace WebAppMVC.Controllers;

//[Authorize]   skyddar sidan, skyddar alla actions. Annars sätter man den över den action man vill skydda. kräver att du måste inloggad för att se dessa actions/sidor.
public class HomeController : Controller
{
    private readonly HttpClient _http;

    public HomeController(HttpClient http)
    {
        _http = http;
    }

    public IActionResult Index()
    {
        var viewModel = new HomeIndexViewModel();

        ViewData["Title"] = viewModel.Title;

        return View(viewModel);
    }

    [HttpPost]
    public async Task<IActionResult> Index(NewsletterViewModel viewModel)
    {
        if (ModelState.IsValid)
        {
            try
            {
                var content = new StringContent(JsonConvert.SerializeObject(viewModel), Encoding.UTF8, "application/json");
                var response = await _http.PostAsync("https://localhost:7091/api/subscribers", content);

                if (response.IsSuccessStatusCode)
                {
                    ViewData["Status"] = "Success";
                }
                else if (response.StatusCode == System.Net.HttpStatusCode.Conflict)
                {
                    ViewData["Status"] = "AlreadyExists";
                }
            }
            catch
            {
                ViewData["Status"] = "ConnectionFailed";
            }
        }
        else
        {
            ViewData["Status"] = "Invalid";
        }

        var homeViewModel = new HomeIndexViewModel();

        return View("Index", homeViewModel);
    }

    //[AllowAnonymous]   // tillåter användare att kolla sidan även om du kör Authorize längst uppe.
    [Route("/error")]
    public IActionResult Error404(int statusCode)
    {
        var viewModel = new ErrorViewModel();
        return View("Index", viewModel);
    }

}

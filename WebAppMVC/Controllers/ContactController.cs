using Microsoft.AspNetCore.Mvc;

namespace WebAppMVC.Controllers;

public class ContactController : Controller
{
    public IActionResult Index()
    {
        ViewData["Title"] = "Contact Us";
        return View();
    }
}

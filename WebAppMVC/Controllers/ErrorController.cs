using Microsoft.AspNetCore.Mvc;

namespace WebAppMVC.Controllers
{
    public class ErrorController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}

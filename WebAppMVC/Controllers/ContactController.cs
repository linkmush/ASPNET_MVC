using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System.Text;
using WebAppMVC.ViewModels.Views;
using static System.Net.WebRequestMethods;

namespace WebAppMVC.Controllers;

public class ContactController(HttpClient http, IConfiguration configuration) : Controller
{
    private readonly HttpClient _http = http;
    private readonly IConfiguration _configuration = configuration;

    [Route("/contact")]
    public IActionResult Index()
    {
        var contactViewModel = new ContactsViewModel();
        ViewData["Title"] = "Contact Us";
        return View(contactViewModel);
    }

    [HttpPost]
    public async Task<IActionResult> SubmitContact(ContactsViewModel viewModel)
    {
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var content = new StringContent(JsonConvert.SerializeObject(viewModel), Encoding.UTF8, "application/json");
                    var response = await _http.PostAsync($"https://localhost:7091/api/contacts?key={_configuration["ApiKey"]}", content);

                    if (response.IsSuccessStatusCode)
                    {
                        ViewData["Status"] = "Success";
                    }
                    else if (response.StatusCode == System.Net.HttpStatusCode.Conflict)
                    {
                        ViewData["Status"] = "AlreadyExists";
                    }
                    else if (response.StatusCode == System.Net.HttpStatusCode.Unauthorized)
                    {
                        ViewData["Status"] = "Unauthorized";
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

            var contactViewModel = new ContactsViewModel();

            return View("Index", contactViewModel);
        }
    }
}

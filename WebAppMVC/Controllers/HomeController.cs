﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Text;
using WebAppMVC.ViewModels.Sections;
using WebAppMVC.ViewModels.Views;

namespace WebAppMVC.Controllers;

public class HomeController(HttpClient http, IConfiguration configuration) : Controller
{
    private readonly HttpClient _http = http;
    private readonly IConfiguration _configuration = configuration;

    public IActionResult Index()
    {
        if (HttpContext.Request.Cookies.TryGetValue("AccessToken", out var token))
        {

        }

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
                var response = await _http.PostAsync($"https://localhost:7091/api/subscribers?key={_configuration["ApiKey"]}", content);

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

        var homeViewModel = new HomeIndexViewModel();

        return View("Index", homeViewModel);
    }

    [Route("/error")]
    public IActionResult Error404(int statusCode)
    {
        var viewModel = new ErrorViewModel();
        return View("Error404", viewModel);
    }

}
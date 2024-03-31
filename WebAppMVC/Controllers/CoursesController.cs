using Infrastructure.Dtos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Net.Http.Headers;
using System.Text;
using WebAppMVC.ViewModels.Components;
using WebAppMVC.ViewModels.Views;

namespace WebAppMVC.Controllers;

[Authorize]
public class CoursesController(IConfiguration configuration, HttpClient http) : Controller
{
    private readonly IConfiguration _configuration = configuration;
    private readonly HttpClient _http = http;

    [Route("/courses")]
    public async Task<IActionResult> Course()
    {
        if (HttpContext.Request.Cookies.TryGetValue("AccessToken", out var token))
        {
            _http.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

            var response = await _http.GetAsync($"https://localhost:7091/api/courses?key={_configuration["ApiKey"]}");
            if (response.IsSuccessStatusCode)
            {
                var courses = JsonConvert.DeserializeObject<IEnumerable<CourseDto>>(await response.Content.ReadAsStringAsync());
                return View(courses);
            }
        }

        return RedirectToAction("Error404", "Home");
    }

    public async Task<IActionResult> CourseDetails(string id)
    {
        if (HttpContext.Request.Cookies.TryGetValue("AccessToken", out var token))
        {
            _http.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

            var response = await _http.GetAsync($"https://localhost:7091/api/courses/{id}?key={_configuration["ApiKey"]}");
            if (response.IsSuccessStatusCode)
            {
                var course = JsonConvert.DeserializeObject<CourseDto>(await response.Content.ReadAsStringAsync());
                return View(course);
            }
        }

        return RedirectToAction("Error404", "Home");
    }

    //[Route("/courses")]
    //public async Task<IActionResult> Index()
    //{
    //    using var http = new HttpClient();
    //    var response = await http.GetAsync($"https://localhost:7091/api/courses?key={_configuration["ApiKey"]}");    /* <--------- hela course sidan*/
    //    var json = await response.Content.ReadAsStringAsync();
    //    var courseDtoList = JsonConvert.DeserializeObject<IEnumerable<CourseDto>>(json);

    //    var viewModelList = courseDtoList?.Select(courseDto => new CourseRegistrationViewModel
    //    {
    //        Title = courseDto.Title,
    //        Price = courseDto.Price,
    //        DiscountPrice = courseDto.DiscountPrice,
    //        Hours = courseDto.Hours,
    //        IsBestSeller = courseDto.IsBestSeller,
    //        LikesInNumbers = courseDto.LikesInNumbers,
    //        LikesInPoints = courseDto.LikesInPoints,
    //        Author = courseDto.Author,
    //        ImageUrl = courseDto.ImageUrl,
    //    }).ToList();

    //    return View(viewModelList);
    //}

    //[Route("/coursedetails")]
    //public async Task<IActionResult> CourseDetails(string id)
    //{
    //    using var http = new HttpClient();
    //    var response = await http.GetAsync($"https://localhost:7091/api/courses/{id}?key={_configuration["ApiKey"]}");      /*<------ Single course sidan*/
    //    var json = await response.Content.ReadAsStringAsync();
    //    var data = JsonConvert.DeserializeObject<CourseDto>(json);

    //    var viewModel = new CourseRegistrationViewModel
    //    {
    //        Id = data!.Id,
    //        Title = data.Title,
    //        Price = data.Price,
    //        DiscountPrice = data.DiscountPrice,
    //        Hours = data.Hours,
    //        IsBestSeller = data.IsBestSeller,
    //        LikesInNumbers = data.LikesInNumbers,
    //        LikesInPoints = data.LikesInPoints,
    //        Author = data.Author,
    //        ImageUrl = data.ImageUrl,
    //    };

    //    return View("CourseDetails", viewModel);
    //}

    //[Route("/subscribe")]
    //public IActionResult Subscribe()
    //{
    //    ViewData["Subscribed"] = false;
    //    return View();
    //}

    //[Route("/subscribe")]
    //[HttpPost]
    //public async Task<IActionResult> Subscribe(SubscribeEntity model)         /*  <------ subscribe läggs i home controller*/
    //{
    //    if (ModelState.IsValid)
    //    {
    //        using var http = new HttpClient();

    //        var json = JsonConvert.SerializeObject(model);
    //        using var content = new StringContent(json, Encoding.UTF8, "application/json");

    //        var response = await http.PostAsync("https://localhost:7091/api/subscribers", content);
    //        if (response.IsSuccessStatusCode)
    //        {
    //            ViewData["Subscribed"] = true;
    //        }
    //    }

    //    return View();
    //}
}

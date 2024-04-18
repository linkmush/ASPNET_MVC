using Azure;
using Infrastructure.Dtos;
using Infrastructure.Entities;
using Infrastructure.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System.Net.Http.Headers;
using System.Security.Claims;
using System.Text;
using WebAppMVC.ViewModels.Components;
using WebAppMVC.ViewModels.Views;

namespace WebAppMVC.Controllers;

[Authorize]
public class CoursesController(IConfiguration configuration, HttpClient http, CategoryService categoryService, CourseService courseService, UserManager<UserEntity> userManager, SavedCourseService savedCourseService) : Controller
{
    private readonly IConfiguration _configuration = configuration;
    private readonly HttpClient _http = http;
    private readonly CategoryService _categoryService = categoryService;
    private readonly CourseService _courseService = courseService;
    private readonly UserManager<UserEntity> _userManager = userManager;
    private readonly SavedCourseService _savedCourseService = savedCourseService;

    [Route("/courses")]
    public async Task<IActionResult> Course(string category = "", string searchQuery = "")
    {
        var viewModel = new CourseViewModel
        {
            Categories = await _categoryService.GetCategoriesAsync(),
            Courses = await _courseService.GetCourseAsync(category, searchQuery),
        };

        return View(viewModel);
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

    [HttpPost]
    public async Task<IActionResult> SaveCourse([FromBody] SaveCourseDto saveCourseDto)
    {
        if (ModelState.IsValid)
        {
            try
            {
                var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

                if (userId != null && saveCourseDto.CourseId != 0)
                {
                    await _savedCourseService.SaveCourseForUserAsync(saveCourseDto.CourseId, userId);

                    return Json(new { success = true });
                }
            }
            catch (Exception)
            {
                return Json(new { success = false });
            }
        }

        return Json(new { success = false });
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

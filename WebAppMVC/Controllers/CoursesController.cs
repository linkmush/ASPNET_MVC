using Azure;
using Infrastructure.Dtos;
using Infrastructure.Entities;
using Infrastructure.Models;
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
    public async Task<IActionResult> Course(string category = "", string searchQuery = "", int pageNumber = 1, int pageSize = 6)
    {
        var courseResult = await _courseService.GetCourseAsync(category, searchQuery, pageNumber, pageSize);

        var viewModel = new CourseViewModel
        {
            Categories = await _categoryService.GetCategoriesAsync(),
            Courses = courseResult.Courses,
            Pagination = new Pagination
            {
                PageSize = pageSize,
                CurrentPage = pageNumber,
                TotalPages = courseResult.TotalPages,
                TotalItems = courseResult.TotalItems
            }
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
}

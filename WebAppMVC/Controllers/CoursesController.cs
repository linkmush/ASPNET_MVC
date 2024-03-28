using Infrastructure.Dtos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Text;
using WebAppMVC.ViewModels.Components;
using WebAppMVC.ViewModels.Views;

namespace WebAppMVC.Controllers;

[Authorize]
public class CoursesController : Controller
{
    [Route("/courses")]
    public async Task<IActionResult> Index()
    {
        using var http = new HttpClient();
        var response = await http.GetAsync("https://localhost:7091/api/courses?key=YzUyZjYyZGEtYjc0Ny00ZDI4LWFkNmUtMTQ4ZTc0YjU0YTdk");    /* <--------- hela course sidan*/
        var json = await response.Content.ReadAsStringAsync();
        var courseDtoList = JsonConvert.DeserializeObject<IEnumerable<CourseDto>>(json);

        var viewModelList = courseDtoList?.Select(courseDto => new CourseRegistrationViewModel
        {
            Title = courseDto.Title,
            Price = courseDto.Price,
            DiscountPrice = courseDto.DiscountPrice,
            Hours = courseDto.Hours,
            IsBestSeller = courseDto.IsBestSeller,
            LikesInNumbers = courseDto.LikesInNumbers,
            LikesInPoints = courseDto.LikesInPoints,
            Author = courseDto.Author,
            ImageUrl = courseDto.ImageUrl,
        }).ToList();

        return View(viewModelList);
    }

    [Route("/coursedetails")]
    public async Task<IActionResult> CourseDetails()
    {
        using var http = new HttpClient();
        var response = await http.GetAsync("https://localhost:7091/api/courses/1");      /*<------ Single course sidan*/
        var json = await response.Content.ReadAsStringAsync();
        var data = JsonConvert.DeserializeObject<CourseDto>(json);

        return View(data);
    }

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

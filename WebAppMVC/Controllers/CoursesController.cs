using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Text;

namespace WebAppMVC.Controllers
{
    //    public class CoursesController : Controller
    //    {
    //        public async Task<IActionResult> Index()
    //        {
    //            using var http = new HttpClient();
    //            var response = await http.GetAsync("https://localhost:7091/api/courses");    /* <--------- hela course sidan*/
    //            var json = await response.Content.ReadAsStringAsync();
    //            var data = JsonConvert.DeserializeObject<IEnumerable<CourseEntity>>(json);

    //            return View(data);
    //        }

    //        [Route("/coursedetails")]
    //        public async Task<IActionResult> CourseDetails()
    //        {
    //            using var http = new HttpClient();
    //            var response = await http.GetAsync("https://localhost:7091/api/courses/1");      /*<------ Single course sidan*/
    //            var json = await response.Content.ReadAsStringAsync();
    //            var data = JsonConvert.DeserializeObject<CourseEntity>(json);

    //            return View(data);
    //        }

    //        [Route("/subscribe")]
    //        public IActionResult Subscribe()
    //        {
    //            ViewData["Subscribed"] = false;
    //            return View();
    //        }

    //        [Route("/subscribe")]
    //        [HttpPost]
    //        public async Task<IActionResult> Subscribe(SubscribeEntity model)         /*  <------ subscribe läggs i home controller*/
    //        {
    //            if (ModelState.IsValid)
    //            {
    //                using var http = new HttpClient();

    //                var json = JsonConvert.SerializeObject(model);
    //                using var content = new StringContent(json, Encoding.UTF8, "application/json");

    //                var response = await http.PostAsync("https://localhost:7091/api/subscribers", content);
    //                if (response.IsSuccessStatusCode)
    //                {
    //                    ViewData["Subscribed"] = true;
    //                }
    //            }

    //            return View();
    //        }
    //    }
}

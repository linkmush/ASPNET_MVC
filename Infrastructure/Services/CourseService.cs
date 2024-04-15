using Infrastructure.Models;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System.Net.Http.Headers;

namespace Infrastructure.Services;

public class CourseService
{
    private readonly HttpClient _httpClient;
    private readonly IConfiguration _configuration;

    public CourseService(HttpClient httpClient, IConfiguration configuration)
    {
        _httpClient = httpClient;
        _configuration = configuration;
        _httpClient.DefaultRequestHeaders.Remove("Authorization"); // denna tar bort bearer token när jag skickar ett request till web apiet. I min Signin så hämtar jag en bearer token när jag loggar in därför skickas det med automatiskt. Om jag ska ta bort denna så måste jag sätta Authorize på allt som jag ska hämta ut i web apiet. Alternativt koda om sign in så bearer token inte finns med i SignIn.
    }

    public async Task<IEnumerable<Course>> GetCourseAsync(string category = "", string searchQuery = "")
    {
        var response = await _httpClient.GetAsync($"{_configuration["ApiUris:Courses"]}?key={_configuration["ApiKey"]}&category={Uri.UnescapeDataString(category)}&searchQuery={Uri.UnescapeDataString(searchQuery)}");
        if (response.IsSuccessStatusCode)
        {
            var result = JsonConvert.DeserializeObject<CourseResult>(await response.Content.ReadAsStringAsync());

            if (result != null && result.Succeeded)
                return result.Courses ??= null!;
        }

        return null!;
    }
}

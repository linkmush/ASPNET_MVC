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
        _httpClient.DefaultRequestHeaders.Remove("Authorization");
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

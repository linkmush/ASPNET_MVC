using Infrastructure.Models;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System.Net.Http.Headers;
using System.Runtime.CompilerServices;

namespace Infrastructure.Services;

public class CategoryService
{
    private readonly HttpClient _httpClient;
    private readonly IConfiguration _configuration;

    public CategoryService(HttpClient httpClient, IConfiguration configuration)
    {
        _httpClient = httpClient;
        _configuration = configuration;
        _httpClient.DefaultRequestHeaders.Remove("Authorization");
    }

    public async Task<IEnumerable<Category>> GetCategoriesAsync()
    {
        var response = await _httpClient.GetAsync($"{_configuration["ApiUris:Categories"]}?key={_configuration["ApiKey"]}");
        if (response.IsSuccessStatusCode)
        {
            var categories = JsonConvert.DeserializeObject<IEnumerable<Category>>(await response.Content.ReadAsStringAsync());
            return categories ??= null!;
        }

        return null!;
    }
}
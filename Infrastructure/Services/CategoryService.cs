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
        _httpClient.DefaultRequestHeaders.Remove("Authorization"); // denna tar bort bearer token när jag skickar ett request till web apiet. I min Signin så hämtar jag en bearer token när jag loggar in därför skickas det med automatiskt. Om jag ska ta bort denna så måste jag sätta Authorize på allt som jag ska hämta ut i web apiet. Alternativt koda om sign in så bearer token inte finns med i SignIn.
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
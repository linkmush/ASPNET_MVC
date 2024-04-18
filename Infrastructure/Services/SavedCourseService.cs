using Infrastructure.Contexts;
using Infrastructure.Dtos;
using Infrastructure.Entities;
using Infrastructure.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System.Diagnostics;
using System.Security.Claims;
using static System.Net.WebRequestMethods;

namespace Infrastructure.Services;

public class SavedCourseService(DataContext context, UserManager<UserEntity> userManager, HttpClient httpClient, IConfiguration configuration)
{
    private readonly DataContext _context = context;
    private readonly UserManager<UserEntity> _userManager = userManager;
    public readonly HttpClient _http = httpClient;
    private readonly IConfiguration _configuration = configuration;

    public async Task SaveCourseForUserAsync(int courseId, string userId)
    {
        try
        {
            // Hämta användaren
            var user = await _userManager.FindByIdAsync(userId);

            if (user != null)
            {
                // Ta reda på om användaren redan har sparat kursen
                var savedCourse = await _context.SavedCourses.FirstOrDefaultAsync(x => x.UserId == userId && x.CourseId == courseId);

                if (savedCourse == null)
                {
                    // Om inte, skapa en ny sparad kurs och lägg till i databasen
                    savedCourse = new SavedCourseEntity
                    {
                        UserId = userId,
                        CourseId = courseId
                    };
                    _context.SavedCourses.Add(savedCourse);
                    await _context.SaveChangesAsync();
                }
            }
        }
        catch (Exception ex)
        {
            Debug.WriteLine($"Error saving courseId {courseId} for userId {userId}: {ex.Message}");
        }
    }

    public async Task<List<CourseDto>> GetCoursesByIdsAsync(string userId)
    {
        var courses = new List<CourseDto>();

        try
        {
            var savedCourses = await _context.SavedCourses.Where(x => x.UserId == userId).ToListAsync();

            var courseIds = savedCourses.Select(x => x.CourseId).ToList();

            foreach (var id in courseIds)
            {
                var response = await _http.GetAsync($"https://localhost:7091/api/courses/{id}?key={_configuration["ApiKey"]}");
                if (response.IsSuccessStatusCode)
                {
                    var courseJson = await response.Content.ReadAsStringAsync();
                    var course = JsonConvert.DeserializeObject<CourseDto>(courseJson);

                    if (course != null)
                    {
                        courses.Add(course);
                    }
                }
            }
        }
        catch (Exception ex)
        {
            Debug.WriteLine($"Error fetching saved courses for userId {userId}: {ex.Message}");
        }

        return courses;
    }

}

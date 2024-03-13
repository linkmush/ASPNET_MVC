using Infrastructure.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;

namespace Infrastructure.Helpers.Middlewares;

public class UserSessionValidationMiddleware(RequestDelegate next)
{
    private readonly RequestDelegate _next = next;

    public async Task InvokeAsync(HttpContext context, UserManager<UserEntity> userManager, SignInManager<UserEntity> signInManager)
    {

    }
}

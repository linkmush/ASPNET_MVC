using Infrastructure.Contexts;
using Infrastructure.Entities;
using Infrastructure.Helpers.Middlewares;
using Infrastructure.Services;
using Microsoft.AspNetCore.Authentication;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddRouting(x => x.LowercaseUrls = true);
builder.Services.AddControllersWithViews();

builder.Services.AddDbContext<DataContext>(x => x.UseSqlServer(builder.Configuration.GetConnectionString("SqlServer")));

builder.Services.AddDefaultIdentity<UserEntity>(x =>
{
    x.User.RequireUniqueEmail = true;
    x.SignIn.RequireConfirmedAccount = false;
    x.Password.RequiredLength = 8;
}).AddEntityFrameworkStores<DataContext>();

builder.Services.ConfigureApplicationCookie(x =>
{
    x.LoginPath = "/signin";
    x.LogoutPath = "/signout";
    x.AccessDeniedPath = "/denied";

    x.Cookie.HttpOnly = true;
    x.Cookie.SecurePolicy = CookieSecurePolicy.Always;
    x.ExpireTimeSpan = TimeSpan.FromMinutes(60);   // avgör hur länge användaren kan vara inaktiv innan den blir utloggad.
    x.SlidingExpiration = true;    // nollställer expiretimespan om man är aktiv igen innan expire tiden.
});

builder.Services.AddAuthentication()
    .AddFacebook(x =>
    {
        x.AppId = "1410996332869705";
        x.AppSecret = "8725b3ee2aaa0fe3ccffb9c8382efccf";
        x.Fields.Add("first_name");
        x.Fields.Add("last_name");
    })
    .AddGoogle(options =>
    {
        options.ClientId = "302258497567-cctb0l24gsij9lr27bpfj6uoaal8h7e7.apps.googleusercontent.com";
        options.ClientSecret = "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";
        options.Scope.Add("profile");

        options.ClaimActions.MapJsonKey(ClaimTypes.NameIdentifier, "sub");
        options.ClaimActions.MapJsonKey(ClaimTypes.GivenName, "given_name");
        options.ClaimActions.MapJsonKey(ClaimTypes.Surname, "family_name");
    });

builder.Services.AddHttpClient();
builder.Services.AddScoped<AddressManager>();
builder.Services.AddScoped<AccountManager>();

var app = builder.Build();
app.UseHsts();
app.UseStatusCodePagesWithReExecute("/error", "?statusCode={0}");      /*--- denna gör så att den går till error sidan om den inte hittar någon sida.*/
app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();

app.UseAuthentication();
app.UseUserSessionValidation();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();

using Infrastructure.Entities;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System.Security.Claims;
using System.Text;
using WebAppMVC.ViewModels.Views;

namespace WebAppMVC.Controllers;

public class AuthController(UserManager<UserEntity> userManager, SignInManager<UserEntity> signInManager, HttpClient http, IConfiguration configuration) : Controller
{

    private readonly UserManager<UserEntity> _userManager = userManager;
    private readonly SignInManager<UserEntity> _signInManager = signInManager;
    private readonly HttpClient _http = http;
    private readonly IConfiguration _configuration = configuration;

    #region Sign Up
    [Route("/signup")]     
    [HttpGet]
    public IActionResult SignUp()
    {
        if (_signInManager.IsSignedIn(User))
        {
            return RedirectToAction("Details", "Account");
        }

        ViewData["Title"] = "Sign Up";
        var viewModel = new SignUpViewModel();
        return View(viewModel);
    }

    [Route("/signup")]    
    [HttpPost]
    public async Task<IActionResult> SignUp(SignUpViewModel viewModel)
    {
        if (ModelState.IsValid)
        {
            var exists = await _userManager.Users.AnyAsync(x => x.Email == viewModel.Form.Email);
            if (exists)
            {
                ModelState.AddModelError("AlreadyExists", "User with same email already exists");
                ViewData["ErrorMessage"] = "User with same email already exists";
                return View(viewModel);
            }

            var userEntity = new UserEntity  
            {
                FirstName = viewModel.Form.FirstName,
                LastName = viewModel.Form.LastName,
                Email = viewModel.Form.Email,
                UserName = viewModel.Form.Email,
            };

            var result = await _userManager.CreateAsync(userEntity, viewModel.Form.Password);           
            if (result.Succeeded)
            {
                return RedirectToAction("SignIn", "Auth");
            }
        }

        return View(viewModel);   
    }
    #endregion

    #region Sign In
    [HttpGet]
    [Route("/signin")]
    public IActionResult SignIn(string returnUrl)
    {
        if (_signInManager.IsSignedIn(User))
        {
            return RedirectToAction("Details", "Account");
        }

        ViewData["ReturnUrl"] = returnUrl ?? Url.Content("~/");

        ViewData["Title"] = "Sign In";
        var viewModel = new SignInViewModel();
        return View(viewModel);
    }

    [HttpPost]
    [Route("/signin")]
    public async Task<IActionResult> SignIn(SignInViewModel viewModel, string returnUrl)
    {

        if (ModelState.IsValid)
        {
            if ((await _signInManager.PasswordSignInAsync(viewModel.Email, viewModel.Password, viewModel.RememberMe, false)).Succeeded) 
            {
                var content = new StringContent(JsonConvert.SerializeObject(viewModel), Encoding.UTF8, "application/json");
                var response = await _http.PostAsync($"https://localhost:7091/api/auth/token?key={_configuration["ApiKey"]}", content);
                if (response.IsSuccessStatusCode)
                {
                    var token = await response.Content.ReadAsStringAsync();
                    var cookieOption = new CookieOptions
                    {
                        HttpOnly = true,
                        Secure = true,
                        Expires = DateTime.Now.AddDays(1)
                    };

                    Response.Cookies.Append("AccessToken", token, cookieOption);
                }

                return LocalRedirect(returnUrl);
            }
        }

        ModelState.AddModelError("IncorrectValues", "Incorrect email or password");
        ViewData["ErrorMessage"] = "Incorrect email or password";
        return View(viewModel);
    }
    #endregion

    #region Sign Out
    [HttpGet]
    [Route("/signout")]
    public new async Task<IActionResult> SignOut()
    {
        await _signInManager.SignOutAsync();
        return RedirectToAction("Index", "Home");
    }
    #endregion

    #region External Account Facebook

    [HttpGet]
    public IActionResult Facebook()
    {
        var authProps = _signInManager.ConfigureExternalAuthenticationProperties("Facebook", Url.Action("FacebookCallback")); 
        return new ChallengeResult("Facebook", authProps);
    }

    [HttpGet]
    public async Task<IActionResult> FacebookCallback()
    {
        var info = await _signInManager.GetExternalLoginInfoAsync();
        if (info != null)
        {
            var userEntity = new UserEntity
            {
                FirstName = info.Principal.FindFirstValue(ClaimTypes.GivenName)!,
                LastName = info.Principal.FindFirstValue(ClaimTypes.Surname)!,
                Email = info.Principal.FindFirstValue(ClaimTypes.Email)!,
                UserName = info.Principal.FindFirstValue(ClaimTypes.Email)!,
                IsExternalAccount = true
            };

            var user = await _userManager.FindByEmailAsync(userEntity.Email);
            if (user == null)
            {
                var result = await _userManager.CreateAsync(userEntity);
                if (result.Succeeded)
                {
                    user = await _userManager.FindByEmailAsync(userEntity.Email);
                }
            }

            if (user != null)
            {
                if (user.FirstName != userEntity.FirstName || user.LastName != userEntity.LastName || user.Email != userEntity.Email)
                {
                    user.FirstName = userEntity.FirstName;
                    user.LastName = userEntity.LastName;
                    user.Email = userEntity.Email;

                    await _userManager.UpdateAsync(user);
                }

                await _signInManager.SignInAsync(user, isPersistent: false);

                if (HttpContext.User != null)
                {
                    return RedirectToAction("Details", "Account");
                }
            }
        }

        ModelState.AddModelError("InvalidFacebook Authenticaction", "Failed to log in with facebook.");
        ViewData["ErrorMessage"] = "Failed to authenticate with facebook.";
        return RedirectToAction("SignIn", "Auth");
    }

    #endregion

    #region External Account Google

    [HttpGet]
    public IActionResult Google()
    {
        var authProps = _signInManager.ConfigureExternalAuthenticationProperties("Google", Url.Action("GoogleCallback"));
        return new ChallengeResult("Google", authProps);
    }

    [HttpGet]
    public async Task<IActionResult> GoogleCallback()
    {
        var info = await _signInManager.GetExternalLoginInfoAsync();
        if (info != null)
        {
            var userEntity = new UserEntity
            {
                FirstName = info.Principal.FindFirstValue(ClaimTypes.GivenName)!,
                LastName = info.Principal.FindFirstValue(ClaimTypes.Surname)!,
                Email = info.Principal.FindFirstValue(ClaimTypes.Email)!,
                UserName = info.Principal.FindFirstValue(ClaimTypes.Email)!,
                IsExternalAccount = true
            };

            var user = await _userManager.FindByEmailAsync(userEntity.Email);
            if (user == null)
            {
                var result = await _userManager.CreateAsync(userEntity);
                if (result.Succeeded)
                {
                    user = await _userManager.FindByEmailAsync(userEntity.Email);
                }
            }

            if (user != null)
            {
                if (user.FirstName != userEntity.FirstName || user.LastName != userEntity.LastName || user.Email != userEntity.Email)
                {
                    user.FirstName = userEntity.FirstName;
                    user.LastName = userEntity.LastName;
                    user.Email = userEntity.Email;

                    await _userManager.UpdateAsync(user);
                }

                await _signInManager.SignInAsync(user, isPersistent: false);

                if (HttpContext.User != null)
                {
                    return RedirectToAction("Details", "Account");
                }
            }
        }

        ModelState.AddModelError("InvalidGoogle Authenticaction", "Failed to log in with google.");
        ViewData["ErrorMessage"] = "Failed to authenticate with google.";
        return RedirectToAction("SignIn", "Auth");
    }

    #endregion
}

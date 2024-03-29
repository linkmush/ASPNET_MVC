﻿using Microsoft.AspNetCore.Mvc;

namespace WebAppMVC.Controllers;

[Route("settings")]
public class SiteSettings : Controller
{
    [HttpGet("ChangeTheme")]
    public IActionResult ChangeTheme(string mode)
    {
        var option = new CookieOptions
        {
            Expires = DateTime.Now.AddDays(60),
        };
        Response.Cookies.Append("ThemeMode", mode, option);
        return Ok();
    }

    [HttpPost]
    public IActionResult CookieConsent()
    {
        var option = new CookieOptions
        {
            Expires = DateTime.Now.AddYears(1),
            HttpOnly = true,
            Secure = true
        };
        Response.Cookies.Append("CookieConsent", "true", option);
        return Ok();
    }
}

//namespace WebAppMVC.Controllers
//{
//    public class SiteSettings : Controller
//    {
//        public IActionResult ChangeTheme(string mode)
//        {
//            var option = new CookieOptions
//            {
//                Expires = DateTime.Now.AddDays(60),
//            };
//            Response.Cookies.Append("ThemeMode", mode, option);
//            return Ok();
//        }
//    }
//}
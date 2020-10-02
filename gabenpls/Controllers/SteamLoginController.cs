using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web;
using AngleSharp.Common;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using gabenpls.Extensions;
using Microsoft.AspNetCore.Http;

namespace Gabenpls.Controllers
{
    public class SteamLoginController : Controller
    {
        [HttpGet("~/loginView")]
        public async Task<IActionResult> LoginView()
        {
            return View("Index");
        }


        [HttpGet("~/login")]
        public async Task<IActionResult> Login()
        {

            return Challenge(new AuthenticationProperties { RedirectUri = "/" }, "Steam");
        }
        

        [HttpGet("~/signin-steam")]
        public IActionResult AfterLoginRedirection()
        {
            
            string steamId = HttpUtility
                .ParseQueryString(Request.QueryString.ToString())
                .Get("openid.identity")
                .Replace("https://steamcommunity.com/openid/id/", "");

            var cookieOptions = new CookieOptions {Expires = new DateTimeOffset(DateTime.Now.AddDays(1))};

            HttpContext.Response.Cookies.Append("SteamId", steamId, cookieOptions);

            return RedirectPermanent("/Achievements");
        }




        [HttpGet("~/signout"), HttpPost("~/signout")]
        public IActionResult SignOut()
        {
            HttpContext.Response.Cookies.Delete("SteamId");
            return SignOut(new AuthenticationProperties { RedirectUri = "/" }, CookieAuthenticationDefaults.AuthenticationScheme);
        }
    }
}

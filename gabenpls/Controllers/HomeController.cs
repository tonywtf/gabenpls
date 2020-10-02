using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using gabenpls.Clients;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using gabenpls.Models;

namespace gabenpls.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly SteamClient client;

        public HomeController(ILogger<HomeController> logger, SteamClient client)
        {
            _logger = logger;
            this.client = client;
        }

        public async Task<ViewResult> Index()
        {
            var steamId = HttpContext.Request.Cookies["SteamId"];
            if (steamId == null)
            {
                ViewData["avatarUrl"] = "~/Images/gaben.jpg";
                return View();
            }
            else
            {
                var playerSummaries = await client.GetPlayerSummaries(steamId);
                ViewData["avatarUrl"] = playerSummaries.avatar;
                return View();
            }
            
        }

        public IActionResult Privacy()
        {
            return View();
        }

        public IActionResult RedirectToLogin()
        {
            return RedirectPermanent("/Login");
        }


        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}

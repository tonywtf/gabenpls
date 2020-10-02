using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using gabenpls.Clients;
using gabenpls.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc;

namespace gabenpls.Controllers
{
    public class AchievementController : Controller
    {
        private readonly SteamClient client;
        private readonly ILogger<HomeController> _logger;
        public AchievementController(ILogger<HomeController> logger, SteamClient client)
        {
            _logger = logger;
            this.client = client;
        }
        
        [HttpGet("~/achievements")]
        public async Task<IActionResult> MainAchievementsPage()
        {
            var steamId = HttpContext.Request.Cookies["SteamId"];
            if (steamId == null)
            {
                ViewData["avatarUrl"] = "~/Images/gaben.jpg";
                return RedirectPermanent("/");
            }
            else
            {
                var playerSummaries = await client.GetPlayerSummaries(steamId);
                ViewData["avatarUrl"] = playerSummaries.avatar;
                var list = new List<Achievement>();
                var gameId = HttpContext.Request.Query["gameId"];
                var gamesList = await client.GetPlayerGames(steamId);
                ViewData["PlayerGamesList"] = gamesList;
                if (String.IsNullOrEmpty(gameId))
                {
                     list = await GetAllPlayerAchievements(steamId);
                }
                else
                {
                    list = await GetAchievementsForGame(steamId, int.Parse(gameId));
                }
                ViewData["AchList"] = list;
                return View("achievements");
            }
        }

        private async Task<List<Achievement>> GetAchievementsForGame(string steamId, int gameId)
        {
            var gameSchema = await client.GetSchemaForGame(gameId);
            var playerAchievements = await client.GetPlayerAchievements(steamId, gameId);
            var achievementsPercent = await client.GetAchievementsPercent(gameId);
            var achWithSchemaMergedList = new List<Achievement>();

            if (gameSchema.achievementList.Count != 0)
            {
                foreach (var ach in playerAchievements)
                {
                    foreach (var schemaAch in gameSchema.achievementList)
                    {
                        if (ach.apiName.Equals(schemaAch.apiName))
                        {
                            achWithSchemaMergedList.Add(new Achievement(
                                schemaAch.iconUrl,
                                schemaAch.title,
                                schemaAch.description,
                                ach.apiName,
                                ach.isAchieved,
                                schemaAch.iconUrlGray,
                                0,
                                ach.unlockTime,
                                null));
                        }
                    }
                }


                var resultList = new List<Achievement>();
                foreach (var ach in achWithSchemaMergedList)
                {
                    foreach (var achPercent in achievementsPercent)
                    {
                        if (ach.apiName.Equals(achPercent.apiName))
                        {
                            resultList.Add(new Achievement(
                                ach.iconUrl,
                                ach.title,
                                ach.description,
                                ach.apiName,
                                ach.isAchieved,
                                ach.iconUrlGray,
                                achPercent.percent,
                                ach.unlockTime,
                                null));
                        }
                    }
                }

                return resultList;
            }

            return gameSchema.achievementList;
        }

        private async Task<List<Achievement>> GetAllPlayerAchievements(string steamId)
        {
            var playerGamesList = await client.GetPlayerGames(steamId);
            var resultAchievementList = new List<Achievement>();
            var requests = new List<Task<List<Achievement>>>();
            foreach (var game in playerGamesList)
            {
                requests.Add(GetAchievementsForGame(steamId, game));
            }

            var result = await Task.WhenAll(requests);

            foreach (var gameAchievements in result)
            {
                resultAchievementList.AddRange(gameAchievements);
            }
            return resultAchievementList;
        }

        public async Task<List<Achievement>> GetAchievementsForGame(string steamId, Game game)
        {
            var achList = await GetAchievementsForGame(steamId, game.id);
            var fullGameAchievements = new List<Achievement>();
            foreach (var ach in achList)
            {
                fullGameAchievements.Add(new Achievement(
                    ach.iconUrl,
                    ach.title,
                    ach.description,
                    ach.apiName,
                    ach.isAchieved,
                    ach.iconUrlGray,
                    ach.percent,
                    ach.unlockTime,
                    game));
            }

            return fullGameAchievements;
        }
        
    }
    
    
}
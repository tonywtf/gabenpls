using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using gabenpls.Controllers;
using gabenpls.Models;
using gabenpls.Models.DataTransferObjects;
using Newtonsoft.Json;

namespace gabenpls.Clients
{
    public class SteamClient
    {
        private readonly IHttpClientFactory clientFactory;
        private static string STEAM_KEY = "00A01E3C408E32DE20C045C5FCCD944E";


        public SteamClient(IHttpClientFactory clientFactory)
        {
            this.clientFactory = clientFactory;
        }


        public async Task<PlayerSummaries> GetPlayerSummaries(string steamId)
        {
            var uriBuilder = new UriBuilder("http://api.steampowered.com/ISteamUser/GetPlayerSummaries/v0002/");
            var query = HttpUtility.ParseQueryString(uriBuilder.Query);
            query["key"] = STEAM_KEY;
            query["steamids"] = steamId;
            uriBuilder.Query = query.ToString();
            var url = uriBuilder.ToString();
            var request = new HttpRequestMessage(HttpMethod.Get, url);
            var client = clientFactory.CreateClient();
            var response = await client.SendAsync(request);


            if (response.IsSuccessStatusCode)
            {
                string body = await response.Content.ReadAsStringAsync();
                var playerSummariesResponse = JsonConvert.DeserializeObject<PlayerSummariesResponse>(body);
                return playerSummariesResponse.response.players[0];
            }

            throw new Exception(response.StatusCode.ToString());
        }

        public async Task<List<Achievement>> GetPlayerAchievements(string steamId, int gameId)
        {
            var uriBuilder = new UriBuilder("http://api.steampowered.com/ISteamUserStats/GetPlayerAchievements/v0001/");
            var query = HttpUtility.ParseQueryString(uriBuilder.Query);
            query["key"] = STEAM_KEY;
            query["steamid"] = steamId;
            query["appid"] = gameId.ToString();
            uriBuilder.Query = query.ToString();
            var url = uriBuilder.ToString();
            var request = new HttpRequestMessage(HttpMethod.Get, url);
            
            var client = clientFactory.CreateClient();
            var response = await client.SendAsync(request);

            if (response.IsSuccessStatusCode)
            {
                string body = await response.Content.ReadAsStringAsync();
                var playersAchievementDTO = JsonConvert.DeserializeObject<PlayerAchievementsDTO>(body);
                return Achievement.ParseFromDTO(playersAchievementDTO);
            }

            return new List<Achievement>();
            //throw new Exception(response.StatusCode.ToString() + gameId);
        }

        public async Task<List<Game>> GetPlayerGames(string steamId)
        {
            var uriBuilder = new UriBuilder("http://api.steampowered.com/IPlayerService/GetOwnedGames/v0001/");
            var query = HttpUtility.ParseQueryString(uriBuilder.Query);
            query["key"] = STEAM_KEY;
            query["steamid"] = steamId;
            query["include_played_free_games"] = "1";
            query["include_appinfo"] = "1";
            uriBuilder.Query = query.ToString();
            var url = uriBuilder.ToString();
            var request = new HttpRequestMessage(HttpMethod.Get, url);
            
            var client = clientFactory.CreateClient();
            var response = await client.SendAsync(request);
            
            if (response.IsSuccessStatusCode)
            {
                string body = await response.Content.ReadAsStringAsync();
                var playerGamesDTO = JsonConvert.DeserializeObject<PlayerGamesDTO>(body);
                return Game.ParseFromDTO(playerGamesDTO);
            }

            throw new Exception(response.StatusCode.ToString());
        }
        
        public async Task<GameSchema> GetSchemaForGame(int gameId)
        {
            var uriBuilder = new UriBuilder("http://api.steampowered.com/ISteamUserStats/GetSchemaForGame/v2/");
            var query = HttpUtility.ParseQueryString(uriBuilder.Query);
            query["key"] = STEAM_KEY;
            query["appid"] = gameId.ToString();
            uriBuilder.Query = query.ToString();
            var url = uriBuilder.ToString();
            var request = new HttpRequestMessage(HttpMethod.Get, url);
            
            var client = clientFactory.CreateClient();
            var response = await client.SendAsync(request);
            
            if (response.IsSuccessStatusCode)
            {
                string body = await response.Content.ReadAsStringAsync();
                var gameSchemaDTO = JsonConvert.DeserializeObject<GameSchemaDTO>(body);
                return GameSchema.ParseFromDTO(gameSchemaDTO, gameId);
            }

            throw new Exception(response.StatusCode.ToString());
        }
        
        public async Task<List<Achievement>> GetAchievementsPercent(int gameId)
        {
            var uriBuilder = new UriBuilder("http://api.steampowered.com/ISteamUserStats/GetGlobalAchievementPercentagesForApp/v0002/");
            var query = HttpUtility.ParseQueryString(uriBuilder.Query);
            query["gameid"] = gameId.ToString();
            query["format"] = "json";
            uriBuilder.Query = query.ToString();
            var url = uriBuilder.ToString();
            var request = new HttpRequestMessage(HttpMethod.Get, url);
            
            var client = clientFactory.CreateClient();
            var response = await client.SendAsync(request);
            
            if (response.IsSuccessStatusCode)
            {
                string body = await response.Content.ReadAsStringAsync();
                var achievementPercentDTO = JsonConvert.DeserializeObject<AchievementPercentDTO>(body);
                return Achievement.ParsePercentFromDTO(achievementPercentDTO);
            }
            return new List<Achievement>();
            //throw new Exception(response.StatusCode.ToString());
        }
    }
}

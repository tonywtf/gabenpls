using System.Collections.Generic;
using gabenpls.Models.DataTransferObjects;

namespace gabenpls.Models
{
    public class Game
    {
        public  int id { get; }

        public  string name { get; }

        public  string iconUrl { get; }

        public Game(int id, string name, string iconUrl)
        {
            this.id = id;
            this.name = name;
            this.iconUrl = iconUrl;
        }
        
        public static List<Game> ParseFromDTO(PlayerGamesDTO DTO)
        {
            var GamesList = new List<Game>();
           
            foreach (var game in DTO.response.games)
            {
                GamesList.Add(new Game(
                    game.appid,
                    game.name,
                    string.Format("http://media.steampowered.com/steamcommunity/public/images/apps/%d/%s.jpg",
                        game.appid, game.img_icon_url)));
            }

            return GamesList;
        }
    }
}
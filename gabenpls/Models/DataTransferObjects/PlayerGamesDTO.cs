using System.Collections.Generic;

namespace gabenpls.Models.DataTransferObjects
{
    public class GameDTO    {
        public int appid { get; set; } 
        public string name { get; set; } 
        public int playtime_forever { get; set; } 
        public string img_icon_url { get; set; } 
        public string img_logo_url { get; set; } 
        public bool has_community_visible_stats { get; set; } 
        public int playtime_windows_forever { get; set; } 
        public int playtime_mac_forever { get; set; } 
        public int playtime_linux_forever { get; set; } 
        public int? playtime_2weeks { get; set; } 
    }

    public class Response    {
        public int game_count { get; set; }
        public List<GameDTO> games { get; set; } 
    }

    public class PlayerGamesDTO    {
        public Response response { get; set; } 
    }
}
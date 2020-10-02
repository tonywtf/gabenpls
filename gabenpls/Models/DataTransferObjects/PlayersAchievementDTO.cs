using System.Collections.Generic;

namespace gabenpls.Models.DataTransferObjects
{
    
        public class AchievementDTO    {
            public string apiname { get; set; } 
            public int achieved { get; set; } 
            public int unlocktime { get; set; } 
        }

        public class PlayerstatsDTO    {
            public string steamID { get; set; } 
            public string gameName { get; set; } 
            public List<AchievementDTO> achievements { get; set; } 
            public bool success { get; set; } 
        }

        public class PlayerAchievementsDTO    {
            public PlayerstatsDTO playerstats { get; set; } 
        }

    
}
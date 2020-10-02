using System.Collections.Generic;

namespace gabenpls.Models.DataTransferObjects
{
    public class Stat    {
        public string name { get; set; } 
        public long defaultvalue { get; set; } 
        public string displayName { get; set; } 
    }

    public class AchievementDto    {
        public string name { get; set; } 
        public long defaultvalue { get; set; } 
        public string displayName { get; set; } 
        public int hidden { get; set; } 
        public string description { get; set; } 
        public string icon { get; set; } 
        public string icongray { get; set; } 
    }

    public class AvailableGameStats    {
        public List<Stat> stats { get; set; } 
        public List<AchievementDto> achievements { get; set; } 
    }

    public class GameParse    {
        public string gameName { get; set; } 
        public string gameVersion { get; set; } 
        public AvailableGameStats availableGameStats { get; set; } 
    }

    public class GameSchemaDTO    {
        public GameParse game { get; set; } 
    }

}
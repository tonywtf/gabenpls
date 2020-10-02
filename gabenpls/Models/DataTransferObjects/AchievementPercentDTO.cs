using System.Collections.Generic;

namespace gabenpls.Models.DataTransferObjects
{
    public class AchievementWithPercent    {
        public string name { get; set; } 
        public double percent { get; set; } 
    }

    public class Achievementpercentages    {
        public List<AchievementWithPercent> achievements { get; set; } 
    }

    public class AchievementPercentDTO    {
        public Achievementpercentages achievementpercentages { get; set; } 
    }

}
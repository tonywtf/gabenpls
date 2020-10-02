using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using gabenpls.Models.DataTransferObjects;

namespace gabenpls.Models
{
    public class Achievement
    {
        public string iconUrl { get; }
        public string title { get; }
        public string description { get; }
        public string apiName { get; }
        public bool isAchieved { get; }
        public string iconUrlGray { get; }
        public double percent { get; }
         public  Game game { get; }
        public  int unlockTime { get; }
       
       public Achievement(string iconUrl, string title, string description, string apiName, bool isAchieved, string iconUrlGray, double percent, int unlockTime, Game game)
       {
           this.iconUrl = iconUrl;
           this.title = title;
           this.description = description;
           this.apiName = apiName;
           this.isAchieved = isAchieved;
           this.iconUrlGray = iconUrlGray;
           this.percent = percent;
           this.unlockTime = unlockTime;
           this.game = game;

       }

       public static List<Achievement> ParseFromDTO(PlayerAchievementsDTO dto)
       {
           var achievementList = new List<Achievement>();
           if (dto.playerstats != null && dto.playerstats.achievements != null)
           {
               
               foreach (var a in dto.playerstats.achievements)
               {
                   achievementList.Add(new Achievement(
                       null,
                       null,
                       null,
                       a.apiname,
                       a.achieved == 1,
                       null,
                       0,
                       a.unlocktime,
                       null));
               }

               return achievementList;
           }

           return new List<Achievement>();
       }

       public static List<Achievement> ParseFromGameSchemaDTO(AvailableGameStats dto)
       {
           var achievementList = new List<Achievement>();
           if (dto.achievements != null)
           {
               foreach (var a in dto.achievements)
               {
                   achievementList.Add(new Achievement(
                       a.icon,
                       a.displayName,
                       a.description,
                       a.name,
                       false,
                       a.icongray,
                       0,
                       0,
                       null));
               }
           }

           return achievementList;
       }
       
       public static List<Achievement> ParsePercentFromDTO(AchievementPercentDTO dto)
       {
           var achievementList = new List<Achievement>();
           
           foreach (var a in dto.achievementpercentages.achievements)
           {
               achievementList.Add(new Achievement(
                   null,
                   null,
                   null,
                   a.name,
                   false,
                   null,
                   a.percent,
                   0,
                   null));
           }

           return achievementList;
       }
       
    }


}

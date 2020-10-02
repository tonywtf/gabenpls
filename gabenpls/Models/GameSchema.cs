using System.Collections.Generic;
using gabenpls.Models.DataTransferObjects;

namespace gabenpls.Models
{
    public class GameSchema
    {

        public int id { get; }
        public string name { get; }
        public List<Achievement> achievementList { get; }
        
        public GameSchema(int id, string name, List<Achievement> achievementList)
        {
            this.id = id;
            this.name = name;
            this.achievementList = achievementList;
        }


        public static GameSchema ParseFromDTO(GameSchemaDTO dto, int gameApiId)
        {
            var id = gameApiId;
            var name = "";
            if (!string.IsNullOrEmpty(dto.game.gameName))
            {
                name = dto.game.gameName;
            }

            var resultList = new List<Achievement>();
            if (dto.game != null && dto.game.availableGameStats != null)
            {
                resultList = Achievement.ParseFromGameSchemaDTO(dto.game.availableGameStats);
                
            }
            return new GameSchema(id, name, resultList);
        }
            
        
    }
    
    
}
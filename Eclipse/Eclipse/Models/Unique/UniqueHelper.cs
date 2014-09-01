using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Eclipse.Models.Unique
{
    public class UniqueHelper
    {
        public static UniqueMethods GetNewRandomUnique()
        {
            var list = new List<UniqueMethods>() { new HumanUnique(),new DracoUnique(), new EridaniUnique(), new HydranUnique(), new MechanemaUnique(), new OrionUnique(), new PlantaUnique() };
            var takenUniques = GameState.GetInstance().Players.Select(x => x.UniqueMethods);

            var refinedList =  list.Except(takenUniques).ToList();

            var rand = RandomGenerator.GetRandom();
            int index = rand.Next(refinedList.Count);

            return refinedList[index];
        }

        public static String GetRandomColor()
        {
            var list = new List<String> { "blue", "red", "green", "purple", "aqua", "maroon" };

            var index = GameState.GetInstance().NumberPlayers;
            return list[index];
        }
        
    }
}
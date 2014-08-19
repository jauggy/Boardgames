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
            var list = new List<UniqueMethods>() { new HumanUnique() };
            var takenUniques = GameState.GetInstance().CurrentPlayers.Select(x => x.UniqueMethods);

            var refinedList =  list.Except(takenUniques).ToList();

            var rand = new Random();
            int index = rand.Next(refinedList.Count);

            return refinedList[index];
        }
        
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Eclipse.Models.Playerboards
{
    public class ResourceSegment
    {
        public String Name { get; set; }
        public String Description { get; set; }

        public ResourceSegment(PopulationType type, PlayerBoard board)
        {
            var msg = "Storage: {0}[placeholder]</br>Production: {1}</br>Next Production: {2}";
            msg = String.Format(msg, board.GetStorage(type), board.GetProduction(type), board.GetNextProduction(type));
            if (type == PopulationType.Money)
               msg= msg.Replace("[placeholder]", "</br>Production less upkeep: " + (board.GetProduction(PopulationType.Money) - board.GetUpkeep()).ToString());
            else
                msg = msg.Replace("[placeholder]", "");

            Description = msg;

            Name = type.ToString();
        }
    }
}
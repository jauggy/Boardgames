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
            var msg = "Storage: {0}</br>Production: {1}</br>Next Production: {2}";
            msg = String.Format(msg, board.GetStorage(type), board.GetProduction(type), board.GetNextProduction(type));
            if (type == PopulationType.Money) 
                msg += "</br>Upkeep: " + board.GetUpkeep();

            Description = msg;

            Name = type.ToString();
        }
    }
}
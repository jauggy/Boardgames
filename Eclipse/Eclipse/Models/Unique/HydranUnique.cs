using Eclipse.Models.Hexes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Eclipse.Models.Unique
{
    public class HydranUnique : UniqueMethods
    {

        public override void PopulateStartingHex(Hex hex)
        {
            hex.AddOrangePlanet(1);
            hex.AddPinkPlanet(2, 1);
            hex.AddBrownPlanet(1, 1);
        }


        protected override List<string> GetStartingTechnolyNames()
        {
            return new List<String> { "Advanced Labs" };
        }

        protected override void SetupStorage(PlayerBoard board)
        {
            board.MoneyStorage = 2;
            board.ScienceStorage = 5;
            board.MaterialsStorage = 2;
        }
    }
}
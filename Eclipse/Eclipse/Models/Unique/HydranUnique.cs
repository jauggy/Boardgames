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

        public override void SetupPlayerboard(PlayerBoard board)
        {
            throw new NotImplementedException();
        }

        public override List<string> GetStartingTechnolyNames()
        {
            return new List<String> { "Advanced Labs" };
        }
    }
}
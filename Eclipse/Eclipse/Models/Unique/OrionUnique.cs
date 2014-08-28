using Eclipse.Models.Hexes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Eclipse.Models.Unique
{
    public class OrionUnique:UniqueMethods
    {

        public override void PopulateStartingHex(Hex hex)
        {
            hex.AddOrangePlanet(1, 1);
            hex.AddPinkPlanet(1);
            hex.AddBrownPlanet(2, 1);
        }

        public override void SetupPlayerboard(PlayerBoard board)
        {
            throw new NotImplementedException();
        }

        protected override List<string> GetStartingTechnolyNames()
        {
            return new List<String> { "Neutron Bombs", "Gauss Shield" };
        }
    }
}
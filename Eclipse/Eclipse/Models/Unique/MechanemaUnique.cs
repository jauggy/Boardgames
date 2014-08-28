using Eclipse.Models.Hexes;
using Eclipse.Models.Playerboards;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Eclipse.Models.Unique
{
    public class MechanemaUnique : UniqueMethods
    {

        public override void PopulateStartingHex(Hex hex)
        {
            hex.AddBrownPlanet(1, 1);
            hex.AddOrangePlanet(2, 1);
            hex.AddPinkPlanet(1);
        }

        public override void SetupPlayerboard(PlayerBoard board)
        {
            throw new NotImplementedException();
        }

        protected override List<string> GetStartingTechnolyNames()
        {
            return new List<String> { "Positron Computer" };
        }

        public override ShipBlueprint CreateInterceptorBlueprint()
        {
            var print =  base.CreateInterceptorBlueprint();
            print.MaterialCost = 2;
            return print;
        }
    }
}
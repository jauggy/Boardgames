using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Eclipse.Models.Hexes;

namespace Eclipse.Models.Unique
{
    public class HumanUnique : UniqueMethods
    {

        public override int GetNumberMovableShips()
        {
            return 3;
        }

        public override void PopulateStartingHex(Hex hex)
        {
            hex.AddOrangePlanet(2, 1);
            hex.AddBrownPlanet(1);
            hex.AddPinkPlanet(2, 1);
        }

        public override void SetupPlayerboard(PlayerBoard board)
        {
            throw new NotImplementedException();
        }
    }
}
using Eclipse.Models.Hexes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Eclipse.Models.Unique
{
    public class EridaniUnique : UniqueMethods
    {

        public override void PopulateStartingHex(Hex hex)
        {
            hex.AddOrangePlanet(2, 1);
            hex.AddPinkPlanet(2, 1);
        }

        public override void SetupPlayerboard(PlayerBoard board)
        {
            throw new NotImplementedException();
        }
    }
}
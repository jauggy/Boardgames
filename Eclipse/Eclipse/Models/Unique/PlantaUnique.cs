﻿using Eclipse.Models.Hexes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Eclipse.Models.Unique
{
    public class PlantaUnique : UniqueMethods
    {

        public override void PopulateStartingHex(Hex hex)
        {
            hex.AddPinkPlanet(1);
            hex.AddOrangePlanet(1);
            hex.AddBrownPlanet(1);
        }

        public override void SetupPlayerboard(PlayerBoard board)
        {
            throw new NotImplementedException();
        }
    }
}
﻿using Eclipse.Models.Playerboards;
using Eclipse.Models.Tech;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Eclipse.Models.Ships
{
    public class GalacticCenter:Ship
    {
        public GalacticCenter()
        {
            Name = ShipNames.GALACTIC_CENTER;
            IsAncient = true;

            var part = new ShipPart();
            var print = new ShipBlueprint();
            print.AddShipPart(part);

            this._getBlueprintFunc = ()=> {return print;};
        }
    }
}
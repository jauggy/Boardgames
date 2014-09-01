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

            IsAncient = true;

            var part = new BonusShipPart();
            part.CannonDamage = new List<int>{1,1,1,1};
            part.Computer = 1;
            this.AddShipPart(part);
        }
    }
}
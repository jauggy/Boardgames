using Eclipse.Models.Tech;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Eclipse.Models.Ships
{
    public class BonusShipPart:ShipPart
    {
        public BonusShipPart()
        {
            this.Name = "Bonus";

        }

        public BonusShipPart(int computer, int energy, int initiative):this()
        {
            Computer = computer;
            EnergySource = energy;
            Initiative = initiative;
        }
    }
}
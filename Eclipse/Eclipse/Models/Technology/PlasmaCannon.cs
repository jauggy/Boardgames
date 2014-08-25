using Eclipse.Models.Playerboards;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Eclipse.Models.Technology
{
    public class PlasmaCannon:Technology
    {
        public override ShipPart GetShipPart()
        {
            var part = new ShipPart();
            part.CannonDamage = new List<int> { 2 };
            part.Name = "Plasma Cannon";
            part.EnergyRequirement = 2;
            return part;
         
        }
    }
}
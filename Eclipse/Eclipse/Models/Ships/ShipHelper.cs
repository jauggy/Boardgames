using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Eclipse.Models.Ships
{
    public class ShipHelper
    {
        public static String GetDescription(IShipPart part)
        {
            var list = new List<String>();
            if (part.CannonDamage.Count > 0)
            {
                list.Add("Cannon_Damage: " + String.Join("+", part.CannonDamage));
            }
            if (part.MissileDamage.Count > 0)
            {
                list.Add("Missile_Damage: " + String.Join("+", part.MissileDamage));
            }
            if (part.Shield > 0)
                list.Add("Shield: " + part.Shield);
            if (part.Hull > 0)
                list.Add("Hull: " + part.Hull);
            if (part.Computer > 0)
                list.Add("Computer: " + part.Computer);

            if (part.EnergySource > 0)
                list.Add("Energy_Source: " + part.EnergySource);
            if (part.Initiative > 0)
                list.Add("Initiative: " + part.Initiative);
            if (part.Movement > 0)
                list.Add("Movement: " + part.Movement);
            if (part.EnergyRequirement > 0)
                list.Add("Energy_Requirement: " + part.EnergyRequirement);

            return String.Join(", ", list);
        }
    }
}
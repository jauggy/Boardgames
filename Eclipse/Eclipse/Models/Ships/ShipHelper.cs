using Eclipse.Models.Playerboards;
using Eclipse.Models.Tech;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Eclipse.Models.Ships
{
    public class ShipHelper
    {
        public static String GetFullDescription(IEnumerable<ShipPart> parts)
        {
            var bp = new ShipBlueprint();
            bp.ShipParts = parts.ToList();

            return GetFullDescription(bp);
        }
        public static String GetFullDescription(IShipPart part)
        {
            var list = new List<String>();
            if (part.CannonDamage!= null && part.CannonDamage.Count > 0)
            {
                list.Add("Cannon Damage: " + String.Join("+", part.CannonDamage));
            }else list.Add("Cannon Datamage:");
            if (part.MissileDamage!=null && part.MissileDamage.Count > 0)
            {
                list.Add("Missile Damage: " + String.Join("+", part.MissileDamage));
            }
            else list.Add("Missile Damage:");
            list.Add("Shield: " + part.Shield);
            list.Add("Hull: " + part.Hull);
            list.Add("Computer: " + part.Computer);
            
            list.Add("Initiative: " + part.Initiative);
            list.Add("Movement: " + part.Movement);
            list.Add("Energy Requirement: " + part.EnergyRequirement);
            var energy = ("<span class='text-danger'> Energy Source: " + part.EnergySource + "</span>");

            if (part.EnergySource >= part.EnergyRequirement)
                energy = energy.Replace("danger", "success");
            list.Add(energy);
            return String.Join("</br>", list);
        }



        public static String GetDescription(IShipPart part)
        {
            var list = new List<String>();
            if (part.CannonDamage!= null && part.CannonDamage.Count > 0)
            {
                list.Add("Cannon Damage: " + String.Join("+", part.CannonDamage));
            }
            if (part.MissileDamage!=null && part.MissileDamage.Count > 0)
            {
                list.Add("Missile Damage: " + String.Join("+", part.MissileDamage));
            }
            if (part.Shield > 0)
                list.Add("Shield: " + part.Shield);
            if (part.Hull > 0)
                list.Add("Hull: " + part.Hull);
            if (part.Computer > 0)
                list.Add("Computer: " + part.Computer);

            if (part.EnergySource > 0)
                list.Add("Energy Source: " + part.EnergySource);
            if (part.Initiative > 0)
                list.Add("Initiative: " + part.Initiative);
            if (part.Movement > 0)
                list.Add("Movement: " + part.Movement);
            if (part.EnergyRequirement > 0)
                list.Add("Energy Requirement: " + part.EnergyRequirement);

            return String.Join("</br>", list);
        }


    }
}
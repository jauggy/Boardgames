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
            }else list.Add("Cannon Damage:");
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

        public static String GetDescription(Ship ship)
        {
            var list = new List<String>();
            if (ship.CannonDamage != null && ship.CannonDamage.Count > 0)
            {
                list.Add("Cannon Damage: " + String.Join("+", ship.CannonDamage));
            }
            else list.Add("Cannon Damage:");
            if (ship.MissileDamage != null && ship.MissileDamage.Count > 0)
            {
                list.Add("Missile Damage: " + String.Join("+", ship.MissileDamage));
            }
            else list.Add("Missile Damage:");
            list.Add("Shield: " + ship.Shield);
            list.Add("Hull: " + ship.Hull);
            list.Add("Computer: " + ship.Computer);

            list.Add("Initiative: " + ship.Initiative);
            list.Add("Movement: " + ship.Movement);

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


        public static String GetShortDescription(IShipPart part)
        {
            var list = new List<String>();
            if (part.CannonDamage != null && part.CannonDamage.Count > 0)
            {
                list.Add(MultString("A",part.CannonDamage));
            }
            if (part.MissileDamage != null && part.MissileDamage.Count > 0)
            {
                list.Add(MultString("M",part.MissileDamage));
            }
            if (part.Shield > 0)
                list.Add(MultString("s", part.Shield));
            if (part.Hull > 0)
                list.Add(MultString("h", part.Hull));
            if (part.Computer > 0)
                list.Add(MultString("c", part.Computer));

            if (part.EnergySource > 0)
                list.Add(MultString("e", part.EnergySource));
            if (part.Initiative > 0)
                list.Add(MultString("i", part.Initiative));
            if (part.Movement > 0)
                list.Add(MultString("v", part.Movement));
          //  if (part.EnergyRequirement > 0)
              //  list.Add("Energy Requirement: " + part.EnergyRequirement);

            return String.Join("", list);
        }

        private static String MultString(string s, int times)
        {
            var result = "";
            for(int i =0; i<times; i++)
            {
                result += s;
            }

            return result;
        }

        private static String MultString(string s, List<int> damage)
        {
            var result = "";
            for (int i = 0; i < damage.Count; i++)
            {
                result += MultString(s, damage[i]);
            }

            return result;
        }



    }
}
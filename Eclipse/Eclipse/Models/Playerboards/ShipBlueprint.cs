using Eclipse.Models.Tech;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Eclipse.Models.Playerboards
{
    public class ShipBlueprint
    {
        public List<ShipPart> ShipParts { get; set; }

        public int Computer { get { return ShipParts.Sum(x => x.Computer); } }
        public int Shield { get { return ShipParts.Sum(x => x.Shield); } }
        public int Hull { get { return ShipParts.Sum(x => x.Hull); } }
        public int EnergyRequirement { get { return ShipParts.Sum(x => x.EnergyRequirement); } }
        public int EnergySource { get { return ShipParts.Sum(x => x.EnergySource); } }
        public int Initiative { get { return ShipParts.Sum(x => x.Initiative); } }
        public int MaterialCost { get; set; }
        public ShipBlueprint()
        {
            ShipParts = new List<ShipPart>();
        }

        public void AddShipPart(ShipPart part)
        {
            ShipParts.Add(part);
        }

        public void RemoveShipPart(ShipPart part)
        {
            ShipParts.Remove(part);
        }
        public bool IsValid()
        {
            return EnergyRequirement <= EnergySource;
        }

        public List<int> GetCannonDamage()
        {
            var list = new List<int>();
            foreach(var s in ShipParts)
            {
                list.AddRange(s.CannonDamage);
            }

            return list;
        }

        public List<int> GetMissileDamage()
        {
            var list = new List<int>();
            foreach (var s in ShipParts)
            {
                list.AddRange(s.MissileDamage);
            }

            return list;
        }

    }
}
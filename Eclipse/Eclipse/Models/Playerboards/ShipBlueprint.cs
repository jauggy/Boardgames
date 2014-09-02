using Eclipse.Models.Ships;
using Eclipse.Models.Tech;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Eclipse.Models.Playerboards
{
    public class ShipBlueprint : IShipPart
    {
        public List<ShipPart> ShipParts { get; set; }

        public int Computer { get { return ShipParts.Sum(x => x.Computer); }  }
        public int Shield { get { return ShipParts.Sum(x => x.Shield); } }
        public int Hull { get { return ShipParts.Sum(x => x.Hull); } }
        public int EnergyRequirement { get { return ShipParts.Sum(x => x.EnergyRequirement); } }
        public int EnergySource { get { return ShipParts.Sum(x => x.EnergySource); } }
        public int Initiative { get { return ShipParts.Sum(x => x.Initiative); } }
        public int MaterialCost { get; set; }
        public int Size { get; set; }
        public bool IsBonus { get; set; }

        public int Movement { get{ return ShipParts.Sum(x => x.Movement); }  }
        public List<int> MissileDamage { get { return GetMissileDamage(); } }
        public List<int> CannonDamage { get { return GetCannonDamage(); } }
        public String Name { get; set; }
        public String Description { get { return GetDescription(); } }
        public ShipBlueprint()
        {
            IsBonus = false;
            ShipParts = new List<ShipPart>();
        }

        public ShipBlueprint(int size, int materialCost):this()
        {
            Size = size;
            MaterialCost = materialCost;
        }

        public void AddShipPart(ShipPart part)
        {
            ShipParts.Add(part);
        }

        public void SetBonus(int computer, int energySource, int initiative)
        {
            this.ShipParts.RemoveAll(x => x.IsBonus);
            AddShipPart(new BonusShipPart(computer, energySource, initiative));
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
                if(s.CannonDamage!=null)
                    list.AddRange(s.CannonDamage);
            }

            return list;
        }

        public List<int> GetMissileDamage()
        {
            var list = new List<int>();
            foreach (var s in ShipParts)
            {
                if(s.MissileDamage!=null)
                    list.AddRange(s.MissileDamage);
            }

            return list;
        }

        public ShipBlueprint SetShipName(String name)
        {
            Name = name;
            return this;
        }

        public String GetDescription()
        {
            return ShipHelper.GetDescription(this);//.Replace(", ","</br>").Replace("_"," ");
        }

    }
}
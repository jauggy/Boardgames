using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Eclipse.Models.Tech
{
    public class BasicShipPart
    {

        public static List<ShipPart> GetAllBasics()
        {
            return new List<ShipPart>
            {
                GetIonCannon(),
                GetElectronComputer(),GetNuclearDrive(),GetNuclearSource(), GetHull()
            };
        }
        public static ShipPart GetIonCannon()
        {
            var part = new ShipPart("Ion Cannon");
            part.CannonDamage = new List<int> { 1 };
            part.EnergyRequirement = 1;
            part.IsBasic = true;
            return part;
        }

        public static ShipPart GetElectronComputer()
        {
            var part = new ShipPart("Electron Computer");
            part.Computer = 1;
            part.IsBasic = true;
            return part;
        }

        public static ShipPart GetNuclearDrive()
        {
            var part = new ShipPart("Nuclear Drive");
            part.Initiative = 1;
            part.Movement = 1;
            part.EnergyRequirement = 1;
            part.IsBasic = true;
            return part;
        }

        public static ShipPart GetNuclearSource()
        {
            var part = new ShipPart("Nuclear Source");
            part.EnergySource = 3;
            part.IsBasic = true;
            return part;
        }

        public static ShipPart GetHull()
        {
            var part = new ShipPart("Hull");
            part.Hull = 1;
            part.IsBasic = true;
            return part;
        }

        public static ShipPart GetGaussShield()
        {
            var part = new ShipPart("Gauss Sheild");
            part.Shield = 1;
            part.IsBasic = true;
            return part;
        }
    }
}
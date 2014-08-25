using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Eclipse.Models.Tech
{
    public class TechnologyFactory
    {
        private List<Technology> Technologies { get; set; }
        public List<Technology> GetAllTechs()
        {
            var list = new List<Technology>();
            list.Add(new Technology(GetPlasmaCannon()));
            list.Add(new Technology(GetPhaseShield()));
            list.Add(new Technology(GetTachyonSource()));
            list.Add(new Technology(Get))
            


        }

        private void AddShipPartToTech(ShipPart part)
        {
            Technologies.Add(new Technology(part.Name, part));
        }

        private ShipPart GetPhaseShield()
        {
            var part = new ShipPart("Phase Shield");
            part.Shield = 2;
            part.EnergyRequirement = 1;
            return part;
        }

        private ShipPart GetTachyonSource()
        {
            var part = new ShipPart("Tachyon Source");
            part.EnergySource = 9;
            return part;
        }

        private ShipPart GetPlasmaMissile()
        {
            var part = new ShipPart("Plasma Missile");
            part.MissileDamage = new List<int> { 2, 2 };
            return part;
        }

        private ShipPart GetPlasmaCannon()
        {
            var part = new ShipPart("Plasma Cannon");
            part.CannonDamage = new List<int> { 2 };
            part.EnergyRequirement = 2;
            return part;
        }
        public ShipPart GetGluonComputer()
        {
            var part = new ShipPart("Gluon Computer");
            part.Computer = 3;
            part.EnergyRequirement = 2;
            part.Initiative = 2;
            return part;
        }

        public ShipPart GetGaussShield()
        {
            var part = new ShipPart("Gauss Shield");
            part.Shield = 1;
            return part;
        }

        public ShipPart GetImprovedHull()
        {
            var part = new ShipPart("Improved Hull");
            part.Hull = 2;
            return part;
        }

        public ShipPart GetFusionSource()
        {
            var part = new ShipPart("Fusion Source");
            part.EnergySource = 6;
            return part;
        }

        public ShipPart GetPositronComputer()
        {
            var part = new ShipPart("Positron Computer");
            part.Computer = 2;
            part.Initiative = 1;
            part.EnergyRequirement = 1;
            return part;
        }

        public ShipPart GetTachyonDrive()
        {
            var part = new ShipPart("Tachyon Drive");
            part.Movement = 3;
            part.Initiative = 3;
            part.EnergyRequirement = 3;
            return part;
        }





    }
}
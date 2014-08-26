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
            //Technologies with ship parts
            list.Add(new Technology(GetPlasmaCannon(),6,4, TechnologyType.Military));
            list.Add(new Technology(GetPhaseShield(),8,5, TechnologyType.Military));
            list.Add(new Technology(GetTachyonSource(),12,6, TechnologyType.Military));
            list.Add(new Technology(GetPlasmaMissile(),14,7, TechnologyType.Military));
            list.Add(new Technology(GetPlasmaCannon(),6,4, TechnologyType.Military));
            list.Add(new Technology(GetGluonComputer(),16,8, TechnologyType.Military));


            list.Add(new Technology(GetGaussShield(),2,2, TechnologyType.Grid));
            list.Add(new Technology(GetImprovedHull(),4,3, TechnologyType.Grid));
            list.Add(new Technology(GetFusionSource(),6,4, TechnologyType.Grid));
            list.Add(new Technology(GetPositronComputer(),8,5, TechnologyType.Grid));
            list.Add(new Technology(GetTachyonDrive(), 12, 6, TechnologyType.Grid));
            list.Add(new Technology(GetAntimatterCannon(), 14, 7, TechnologyType.Grid));



            list.Add(new Technology(GetFusionDrive(), 4, 3, TechnologyType.Nano));

            return list;
        }

        private ShipPart GetFusionDrive()
        {
            var part = new ShipPart("Fusion Drive");
            part.Movement = 2;
            part.Initiative = 2;
            part.EnergyRequirement = 2;
            return part;
        }

        private ShipPart GetAntimatterCannon()
        {
            var part = new ShipPart("Antimatter Cannon");
            part.CannonDamage = new List<int> { 4 };
            part.EnergyRequirement = 4;
            return part;
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
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Eclipse.Models.Tech
{
    public class TechnologyFactory
    {
        private static List<Technology> Technologies { get; set; }
        public List<Technology> GetAllTechs()
        {
            if (Technologies == null)
            {
                Technologies = new List<Technology>();

                //Technologies with ship parts
                listAdd(new Technology(GetPlasmaCannon(), 6, 4, TechnologyType.Military));
                listAdd(new Technology(GetPhaseShield(), 8, 5, TechnologyType.Military));
                listAdd(new Technology(GetTachyonSource(), 12, 6, TechnologyType.Military));
                listAdd(new Technology(GetPlasmaMissile(), 14, 7, TechnologyType.Military));
                listAdd(new Technology(GetPlasmaCannon(), 6, 4, TechnologyType.Military));
                listAdd(new Technology(GetGluonComputer(), 16, 8, TechnologyType.Military));


                listAdd(new Technology(GetGaussShield(), 2, 2, TechnologyType.Grid));
                listAdd(new Technology(GetImprovedHull(), 4, 3, TechnologyType.Grid));
                listAdd(new Technology(GetFusionSource(), 6, 4, TechnologyType.Grid));
                listAdd(new Technology(GetPositronComputer(), 8, 5, TechnologyType.Grid));
                listAdd(new Technology(GetTachyonDrive(), 12, 6, TechnologyType.Grid));
                listAdd(new Technology(GetAntimatterCannon(), 14, 7, TechnologyType.Grid));



                listAdd(new Technology(GetFusionDrive(), 4, 3, TechnologyType.Nano));

                listAdd(new Technology("Neutron Bombs", 2, 2, TechnologyType.Military)).Description = "You may destroy population without rolling dice";
                listAdd(new Technology("Starbase", 4, 3, TechnologyType.Military)).Description = "You may build starbases";
                listAdd(new Technology("Advanced Mining", 10, 6, TechnologyType.Military)).Description = "You may populate advanced materials squares";
                listAdd(new Technology("Advanced Economy", 10, 6, TechnologyType.Grid)).Description = "You may populate advanced economy squares";
                listAdd(new Technology("Nanorobots", 2, 2, TechnologyType.Nano)).Description = "You may build one additional ship or structure";
                listAdd(new Technology("Quantum Grid", 16, 8, TechnologyType.Grid)).Description ="Two additional influence disks";
                listAdd(new Technology("Advanced Robotics", 6, 4, TechnologyType.Nano)).Description = "One additional influence disk";
                listAdd(new Technology("Orbitol", 8, 5, TechnologyType.Nano)).Description = "You may build orbitols";
                listAdd(new Technology("Advanced Labs", 10, 6, TechnologyType.Nano)).Description = "You may populate advanced science squares";
                listAdd(new Technology("Monolith", 12, 6, TechnologyType.Nano)).Description = "You may build monoliths";
                listAdd(new Technology("Artifact Key", 14, 7, TechnologyType.Nano)).Description = "Take 5 resources of one type for each artifact on controlled hexes";
                listAdd(new Technology("Wormhole Generator", 16, 8, TechnologyType.Nano)).Description = "Explore, influence and move through hex edge that has a wormhole on one side";
            }
            return Technologies;
        }


        private Technology listAdd(Technology tech)
        {
            Technologies.Add(tech);
            return tech;
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
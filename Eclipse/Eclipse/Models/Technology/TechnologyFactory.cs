using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Eclipse.Models.Technology
{
    public class TechnologyFactory
    {
        public List<Technology> GetAllTechs()
        {
            var list = new List<Technology>();
            list.Add(new Technology("Plasma Cannon", GetPlasmaCannonShipPart()));
            list.Add(new Technology("Phase Shield"), Get)
            


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
        }
    }
}
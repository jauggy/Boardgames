﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Eclipse.Models.Technology
{
    public class ShipPart
    {
        public List<int> CannonDamage { get; set; }
        public int Shield { get; set; }
        public int Hull { get; set; }
        public int Computer { get; set; }
        public int EnergyRequirement { get; set; }
        public int EnergySource { get; set; }
        public List<int> MissileDamage { get; set; }
        public int Initiative { get; set; }
        public int Movement { get; set; }
        public String Name { get; set; }

        public ShipPart(String name)
        {
            Name = name;
            CannonDamage = new List<int>();
            MissileDamage = new List<int>();
        }
    }
}
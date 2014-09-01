using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Eclipse.Models.Ships
{
    public class DamageDice
    {
        public int Damage { get; set; }
        public int Value { get; set; }
        public int AdjustedValue { get; set; }
        public DamageDice(int damage, int computer)
        {
            Damage = damage;
            Value = RandomGenerator.GetDice();
            AdjustedValue = Value + computer;
        }
    }
}
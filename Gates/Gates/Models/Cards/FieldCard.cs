using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GatesOfLoyang.Models.Cards
{
    public class FieldCard
    {
        public int Size { get; set; }
        public List<Vegetable> AllowedVegetables { get; set; }
        public List<Vegetable> CurrentVegetables { get; set; }

        public bool IsBrown { get; set; }
    }
}
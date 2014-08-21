using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Eclipse.Models
{
    public enum PopulationType
    {
        Money, Science, Production
    }

    public class PopulationSquare
    {
        public bool IsOccupied { get; set; }
        public int PlayerId { get; set; }
        public PopulationType Type { get; set; }
    }
}
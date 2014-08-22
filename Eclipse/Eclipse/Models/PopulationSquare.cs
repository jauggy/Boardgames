using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Eclipse.Models
{
    public enum PopulationType
    {
        Money, Science, Materials
    }

    public class PopulationSquare
    {
        public bool IsOccupied { get; set; }
        public int PlayerId { get; set; }
        public PopulationType Type { get; set; }

        public PopulationSquare Copy()
        {
            return null;
        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Eclipse.Models
{
    public class Planet
    {
        public PopulationSquare[] NormalPopulationSquares { get; set; }
        public PopulationSquare[] AdvancedPopulationSquares { get; set; }

    }
}
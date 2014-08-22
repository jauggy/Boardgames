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

        public Planet(int normal, int advanced)
        {
            NormalPopulationSquares = new PopulationSquare[normal];
            AdvancedPopulationSquares = new PopulationSquare[advanced];
        }

        public Planet() { }

        public Planet Copy()
        {
            var planet = new Planet();
            planet.NormalPopulationSquares = NormalPopulationSquares.Select(x => x.Copy()).ToArray();
            planet.AdvancedPopulationSquares = AdvancedPopulationSquares.Select(x => x.Copy()).ToArray();

            return planet;
        }

    }
}
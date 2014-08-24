using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Web;

namespace Eclipse.Models.Hexes
{
    public class HexFactory
    {
        public static Hex CreateHex(Point p)
        {
            return null;
        }

        public static Hex CreateCenterHex()
        {
            var hex = new Hex();
            hex.AddPinkPlanet(2, 1);
            hex.AddBrownPlanet(2, 1);
            hex.AddGrayPlanet(2);

            return hex;
        }

        public static void PopulateLevel1Hex(Hex hex)
        {
            hex.AddRandomPopSquare(RandomGenerator.GetInt(1, 2), false);
            hex.AddRandomPopSquare(RandomGenerator.GetInt(0, 2), true);
        }



    }
}
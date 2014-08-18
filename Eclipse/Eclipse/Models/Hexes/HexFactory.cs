using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Web;

namespace Eclipse.Models.Hexes
{
    public class HexFactory
    {
        public static void CreateHex(Point p)
        {
        }

        public static Hex CreateCenterHex()
        {
            var hex = new Hex();
            hex.AddPinkPlanet(2, 1);
            hex.AddBrownPlanet(2, 1);
            hex.AddGrayPlanet(2);

            return hex;
        }

        public static void CreateHex(Point p, int startingHexNumber)
        {
            //Starting players have a certain hex number
            var hex = new Hex();
            switch(startingHexNumber)
            {
                case 222:
                    hex.AddOrangePlanet(2, 1);
                    hex.AddPinkPlanet(2, 1);
                    break;
                case 224:
                    hex.AddOrangePlanet(1);
                    hex.AddPinkPlanet(2, 1);
                    hex.AddBrownPlanet(1, 1);
                    break;
                case 226:
                    hex.AddPinkPlanet(1);
                    hex.AddOrangePlanet(1);
                    hex.AddBrownPlanet(1);
                    break;
                case 228:
                    hex.AddOrangePlanet(1);
                    hex.AddPinkPlanet(1);
                    hex.AddBrownPlanet(1, 1);
                    break;
                case 230:
                    hex.AddBrownPlanet(1, 1);
                    hex.AddOrangePlanet(2, 1);
                    hex.AddPinkPlanet(1);
                    break;
                case 232:
                    hex.AddOrangePlanet(1, 1);
                    hex.AddPinkPlanet(1);
                    hex.AddBrownPlanet(2, 1);
                    break;
                default:
                    hex.AddOrangePlanet(2, 1);
                    hex.AddBrownPlanet(1);
                    hex.AddPinkPlanet(2, 1);
                    break;
            }
        }
    }
}
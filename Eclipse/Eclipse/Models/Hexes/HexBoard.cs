using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Web;

namespace Eclipse.Models.Hexes
{
    public class HexBoard
    {
        public List<Hex> Hexes { get; set; }
        public void Setup()
        {
            var startHex = new Hex();
            startHex.PointLocation = new Point(0, 0);
            startHex.RingLevel = 0;
            var firstRing = startHex.GetRing(1).ToHex();
            firstRing.ForEach(x => x.RingLevel = 1);

            var secondRing = startHex.GetRing(2).ToHex();
            secondRing.ForEach(x => x.RingLevel = 2);

            Hexes = new List<Hex>();
            Hexes.Add(startHex);
            Hexes.AddRange(firstRing);
            Hexes.AddRange(secondRing);



        }

        private void PopulateHex(Hex hex)
        {
            //Populates hex based on the level
        }

        private void PopulateCenterHex(Hex hex)
        {
        }


    }
}
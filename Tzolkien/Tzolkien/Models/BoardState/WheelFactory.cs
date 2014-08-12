using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Tzolkien.Models.BoardState
{
    public class WheelFactory
    {
        public Wheel CreatePalenqueWheel()
        {
            var wheel = new Wheel();
            wheel.Locations = new List<Location>();


            var location = new Location();
            location.LocationAction = (w) =>
            {
                var player = w.Owner;
            };

            return null;

        }
    }
}
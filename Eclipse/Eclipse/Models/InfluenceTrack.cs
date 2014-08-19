using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Eclipse.Models
{
    public class InfluenceTrack
    {
        private List<int> _upkeepNumbers;

        public InfluenceTrack()
        {
            _upkeepNumbers = new List<int>
            {
                0,0,-1,-2,-3,-5,-7,-10,-13,-17,-21,-25,-30
            };
        }
    }
}
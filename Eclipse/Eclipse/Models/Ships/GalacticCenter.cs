using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Eclipse.Models.Ships
{
    public class GalacticCenter:Ship
    {
        public GalacticCenter()
        {
            AddCannon(1);
            AddCannon(1);
            AddCannon(1);
            AddCannon(1);
            Computers += 1;
            IsAncient = true;
        }
    }
}
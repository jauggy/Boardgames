using Eclipse.Models.Hexes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Eclipse.Models.Unique
{
    public class HydranUnique : UniqueMethods
    {
        public override Hexes.Hex CreateStartingHex()
        {
            return HexFactory.CreateStartingHex(224);
        }
    }
}
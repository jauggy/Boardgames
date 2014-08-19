using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Eclipse.Models.Hexes;

namespace Eclipse.Models.Unique
{
    public class HumanUnique : UniqueMethods
    {
        public override Hex CreateStartingHex()
        {
            return HexFactory.CreateStartingHex(-1);
        }
    }
}
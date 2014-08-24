using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Web;
using Eclipse.Models.Hexes;

namespace Eclipse.Models
{
    public abstract class UniqueMethods
    {
        //CreateStartingHex
        public abstract void PopulateStartingHex(Hex hex);

        //DrawStartingReputation
        //Move
        //InitInfluenceDisks
        //InitShipBlueprints
        //InitTechnologies
        //GetTradeRate
        //InitStartingStorage

        public virtual int GetNumberMovableShips()
        {
            return 2;
        }
    }
}
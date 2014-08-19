using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Eclipse.Models.Hexes;
using Eclipse.Models.Unique;

namespace Eclipse.Models
{
    public class Player
    {
        public UniqueMethods UniqueMethods { get; set; }

        public Player(UniqueMethods uniqueMethods)
        {
            UniqueMethods = uniqueMethods;
        }

        public Player():this(UniqueHelper.GetNewRandomUnique())
        {
            
        }
    }
}
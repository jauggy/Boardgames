using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Tzolkien.Models.BoardState
{
    public class YaxLocation
    {
        /*
         * 1 wood
         * stone corn
         * gold 2 corn
         * skull
         * gold stone 2 corn
         * 
         * /
        public bool IsUnique { get; set; }
        public int Index { get; set; }

        public void PalenqueLocation(int index)
        {
            
            Index = index;
            IsUnique = index > 0 && index < 6;

            if (index > 1 && index < 6)
            {
                WoodTokens = BoardState.GetInstance().NumberOfPlayers;
                CornTokens = BoardState.GetInstance().NumberOfPlayers;

                if (index == 2)
                {
                    CornPayoff = 4;
                }
                else if (index == 3)
                {
                    CornPayoff = 5;
                    WoodPayoff = 2;
                }
                else if (index == 4)
                {
                    CornPayoff = 7;
                    WoodPayoff = 3;
                }
                else if (index == 6)
                {
                    CornPayoff = 9;
                    WoodPayoff = 4;
                }

            }
        }

        public List<PlayerAction> GetPossibleActions()
        {
            var result = new List<PlayerAction>();

        }

 
    }
}
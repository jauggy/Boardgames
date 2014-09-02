using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Eclipse.Models.Combat
{
    public class CombatResultTotal
    {
        public int Wins { get; set; }
        public int Losses { get; set; }
        public int Draws { get; set; }

        public void AddResult(CombatResult res)
        {
            if (res == CombatResult.Win)
                Wins++;
            else if (res == CombatResult.Lose)
                Losses++;
            else
                Draws++;
        }
    }
}
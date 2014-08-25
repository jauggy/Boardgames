using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Eclipse.Models
{
    public class PlayerBoard
    {
        //Storage Track; storage markers
        //Technology track - military, grid, nano
        //Reputation track
        //Population cubes in the money, science, materials track
        //influce disks on action spaces and influence track
        public int InfluenceDisks { get; private set; }
        public int MoneyPopulationCubes { get; private set; }
        public int SciencePopulationCubes { get; private set; }
        public int MaterialsPopulationCubes { get; private set; }
        public PlayerBoard()
        {
            InfluenceDisks = 13;
        }

        public int GetUpkeep()
        {
            var list = new List<int> { -30, -25, -21, -17, -13, -10, -7, -5, -3, -2, -1, 0, 0 };
            return list[InfluenceDisks];
        }

        public void RemoveInfluenceDisk()
        {
            InfluenceDisks--;
        }
    }
}
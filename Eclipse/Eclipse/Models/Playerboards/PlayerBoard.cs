using Eclipse.Models.Playerboards;
using Eclipse.Models.Tech;
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
        public List<Technology> Technologies { get; private set; }
        public ShipBlueprint InterceptorBlueprint { get; set; }
        public ShipBlueprint CruiserBlueprint { get; set; }
        public ShipBlueprint DreadnoughtBlueprint { get; set; }
        public ShipBlueprint StarbaseBlueprint { get; set; }
        public PlayerBoard()
        {
            InfluenceDisks = 13;
            Technologies = new List<Technology>();
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

        public void AddStartingTechs(List<String> list)
        {
            foreach(var name in list)
            {
                var tech =GameState.GetInstance().SupplyBoard.GetTechnologyWithoutRemove(name);
                Technologies.Add(tech);
            }
            
        }

        public int GetProductionLevel(int numPopCubes)
        {
            var list = new List<int> { 28, 24, 21, 18, 15, 12, 10, 8, 6, 4, 3, 2 };
            return list[numPopCubes];
        }

        public void RemovePopulationCube(PopulationType type)
        {

        }
    }
}
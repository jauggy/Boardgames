﻿using Eclipse.Models.Hexes;
using Eclipse.Models.Playerboards;
using Eclipse.Models.Tech;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Script.Serialization;

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
        public int MoneyStorage { get; set; }
        public int MaterialsStorage { get; set; }
        public int ScienceStorage { get; set; }

        public TechnologySegment[] TechnologySegments { get{return   GetTechSegments(); } }

        

        public PlayerBoard()
        {
            InfluenceDisks = 13;
            Technologies = new List<Technology>();
            MoneyPopulationCubes = 12;
            SciencePopulationCubes = 12;
            MaterialsPopulationCubes = 12;
        }

        public TechnologySegment[] GetTechSegments()       
        {
            var list = new List<TechnologySegment>();
            list.Add(CreateTechnologySegment(TechnologyType.Military));
            list.Add(CreateTechnologySegment(TechnologyType.Grid));
            list.Add(CreateTechnologySegment(TechnologyType.Nano));
            return list.ToArray();
        }

        private TechnologySegment CreateTechnologySegment(TechnologyType type)
        {
            var segment1 = new TechnologySegment(type.ToString(), Technologies.Where(x => x.Type == type).ToList());
            var footer = new List<String>();
            footer.Add("Discount: " + GetDiscount(type));
            footer.Add("Next discount:  " + GetDiscountNext(type));
            footer.Add("VP: " + GetVP(type));
            footer.Add("Next VP: " + GetVPNext(type));

            segment1.Footer = footer;
            return segment1;
        }

        public int GetDiscount(TechnologyType type)
        {
            return 0;
        }

        public int GetDiscountNext(TechnologyType type)
        {
            return 0;
        }

        public int GetVP(TechnologyType type)
        {
            return 0;
        }

        public int GetVPNext(TechnologyType type)
        {
            return 0;
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

        public void RemovePop(PopulationType type)
        {
            AdjustPop(type, -1);
        }

        private void AdjustPop(PopulationType type, int adjust)
        {
            if (type == PopulationType.Materials)
                MaterialsPopulationCubes += adjust;
            else if (type == PopulationType.Money)
                MoneyPopulationCubes += adjust;
            else if (type == PopulationType.Science)
                SciencePopulationCubes += adjust;
            else
                throw new NotImplementedException();
        }
    }
}
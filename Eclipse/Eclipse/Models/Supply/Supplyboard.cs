using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Eclipse.Models.Tech;
using Eclipse.Models.Playerboards;

namespace Eclipse.Models.Supply
{
    public class SupplyBoard
    {
        public List<Technology> AllTechnologies { get; set; }
        public List<Technology> AvailableTechnologies { get; set; }
       // public List<Technology> FutureTechnologies { get; set; }
        public TechnologySegment[] TechnologySegments { get { return GetTechSegments(); } }


        public SupplyBoard()
        {
            var factory = new TechnologyFactory();
            AllTechnologies = factory.GetAllTechs();
           
            AvailableTechnologies = new List<Technology>();
            AddRandomTechToSupplyBoard(GetStartingNumberTech());
        }

        public SupplyBoard GetInstance()
        {
            return GameState.GetInstance().SupplyBoard;
        }

        public Technology GetTechnologyWithoutRemove(String techName)
        {
            var result =  AllTechnologies.FirstOrDefault(x => x.Name.Equals(techName, StringComparison.InvariantCultureIgnoreCase));
            if (result == null)
                throw new Exception("Couldn't find tech with name: " + techName);

            return result;
        }

        public void AddRandomTechToSupplyBoard(int num)
        {
            for(int i =0;i<num;i++)
            {
                var index = RandomGenerator.GetInt(0, AllTechnologies.Count - 1);
                var randTech = AllTechnologies[index];
                var existing = AvailableTechnologies.FirstOrDefault(x => x.Name == randTech.Name);
                if(existing==null)
                {
                    randTech.SupplyCount = 1;
                    AvailableTechnologies.Add(randTech);
                }
                else
                {
                    existing.SupplyCount++;
                }
            }
        }

        private int GetStartingNumberTech()
        {
            var list = new List<int> { 0, 0, 12, 14, 16, 18, 20 };
            var i = GameState.GetInstance().NumberPlayers;
            return list[i];
        }

        private int GetCleanupPhaseNumberTech()
        {
            var list = new List<int> { 0, 0, 4, 6, 7, 8, 9 };
            var i = GameState.GetInstance().NumberPlayers;
            return list[i];
        }

        private TechnologySegment[] GetTechSegments()
        {
            var list = new TechnologySegment[3];
            list[0] = CreateTechSegment(TechnologyType.Military);
            list[1] = CreateTechSegment(TechnologyType.Grid);
            list[2] = CreateTechSegment(TechnologyType.Nano);
            return list;
        }

        public TechnologySegment CreateTechSegment(TechnologyType type)
        {
            var techs = AvailableTechnologies.Where(x => x.Type == type).ToList();
            var segment = new TechnologySegment(type.ToString(), techs);
            return segment;
        }
    }
}
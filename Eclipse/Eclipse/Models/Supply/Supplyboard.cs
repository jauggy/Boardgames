using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Eclipse.Models.Tech;

namespace Eclipse.Models.Supply
{
    public class SupplyBoard
    {
        public List<Technology> AllTechnologies { get; set; }
        public List<Technology> AvailableTechnologies { get; set; }
        public List<Technology> FutureTechnologies { get; set; }

        public SupplyBoard()
        {
            var factory = new TechnologyFactory();
            AllTechnologies = factory.GetAllTechs();
            FutureTechnologies = AllTechnologies.Copy();

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
                var index =  RandomGenerator.GetInt(0, FutureTechnologies.Count - 1);
                AvailableTechnologies.Add(FutureTechnologies[index]);
                FutureTechnologies.RemoveAt(index);

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
    }
}
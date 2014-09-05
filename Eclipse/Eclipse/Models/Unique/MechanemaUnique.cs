using Eclipse.Models.Hexes;
using Eclipse.Models.Playerboards;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Eclipse.Models.Unique
{
    public class MechanemaUnique : UniqueMethods
    {

        public override void PopulateStartingHex(Hex hex)
        {
            hex.AddBrownPlanet(1, 1);
            hex.AddOrangePlanet(2, 1);
            hex.AddPinkPlanet(1);
        }

        public override int GetNumberBuilds()
        {
            return 3;
        }

        protected override List<string> GetStartingTechnolyNames()
        {
            return new List<String> { "Positron Computer" };
        }

        public override ShipBlueprint CreateInterceptorBlueprint()
        {
            var print =  base.CreateInterceptorBlueprint();
            print.MaterialCost = 2;
            return print;
        }

        public override ShipBlueprint CreateCruiserBlueprint()
        {
            var print = base.CreateCruiserBlueprint();
            print.MaterialCost = 4;
            return print;
        }

        public override ShipBlueprint CreateDreadnoughtBlueprint()
        {
            var print =  base.CreateDreadnoughtBlueprint();
            print.MaterialCost = 7;
            return print;
        }

        public override ShipBlueprint CreateStarbaseBlueprint()
        {
            
            var print =  base.CreateStarbaseBlueprint();
            print.MaterialCost = 2;
            return print;
        }

        protected override void SetupStorage(PlayerBoard board)
        {
            board.MoneyStorage = 3;
            board.ScienceStorage = 3;
            board.MaterialsStorage = 3;
        }
    }
}
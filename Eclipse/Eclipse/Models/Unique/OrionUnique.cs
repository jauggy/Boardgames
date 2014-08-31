using Eclipse.Models.Hexes;
using Eclipse.Models.Tech;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Eclipse.Models.Unique
{
    public class OrionUnique:UniqueMethods
    {

        public override void PopulateStartingHex(Hex hex)
        {
            hex.AddOrangePlanet(1, 1);
            hex.AddPinkPlanet(1);
            hex.AddBrownPlanet(2, 1);
        }

        protected override List<string> GetStartingTechnolyNames()
        {
            return new List<String> { "Neutron Bombs", "Gauss Shield" };
        }

        public override Playerboards.ShipBlueprint CreateInterceptorBlueprint()
        {
            var print = base.CreateInterceptorBlueprint();
            print.AddShipPart(BasicShipPart.GetGaussShield());
            print.SetBonus(0, 1, 3);
            return print;
        }

        public override Playerboards.ShipBlueprint CreateCruiserBlueprint()
        {
            var print =  base.CreateCruiserBlueprint();
            print.SetBonus(0, 2, 2);
           
            print.AddShipPart(BasicShipPart.GetGaussShield());
         
            return print;
        }

        public override Playerboards.ShipBlueprint CreateDreadnoughtBlueprint()
        {
            var print =  base.CreateDreadnoughtBlueprint();
            print.AddShipPart(BasicShipPart.GetGaussShield());
            print.SetBonus(0, 3, 1);

            return print;
        }

        public override Playerboards.ShipBlueprint CreateStarbaseBlueprint()
        {
            var print = base.CreateStarbaseBlueprint();
            print.SetBonus(0, 0, 5);
       
            print.AddShipPart(BasicShipPart.GetGaussShield());
            return print;
        }

        protected override void SetupStorage(PlayerBoard board)
        {
            board.MoneyStorage = board.ScienceStorage = 3;
            board.MaterialsStorage = 5;
        }
    }
}
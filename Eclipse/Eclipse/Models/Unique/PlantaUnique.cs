using Eclipse.Models.Hexes;
using Eclipse.Models.Playerboards;
using Eclipse.Models.Ships;
using Eclipse.Models.Tech;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Eclipse.Models.Unique
{
    public class PlantaUnique : UniqueMethods
    {

        public override void PopulateStartingHex(Hex hex)
        {
            hex.AddPinkPlanet(1);
            hex.AddOrangePlanet(1);
            hex.AddBrownPlanet(1);
        }


        protected override List<string> GetStartingTechnolyNames()
        {
            return new List<string> { "Starbase" };
        }

        public override ShipBlueprint CreateInterceptorBlueprint()
        {
            var print = new ShipBlueprint(3, 3);
            print.AddShipPart(BasicShipPart.GetIonCannon());
            print.AddShipPart(BasicShipPart.GetNuclearDrive());
            print.AddShipPart(BasicShipPart.GetNuclearSource());
            print.AddShipPart(new BonusShipPart(1, 2, 0));
            return print;
        }

        public override ShipBlueprint CreateCruiserBlueprint()
        {
            var print = this.CreateInterceptorBlueprint();
            print.Size = 5;
            print.MaterialCost = 5;
            print.AddShipPart(BasicShipPart.GetHull());
            return print;
        }

        public override ShipBlueprint CreateDreadnoughtBlueprint()
        {
            var print = this.CreateCruiserBlueprint();
            print.Size = 7;
            print.MaterialCost = 8;
            print.AddShipPart(BasicShipPart.GetHull());
            print.AddShipPart(BasicShipPart.GetIonCannon());
            return print;
        }

        public override ShipBlueprint CreateStarbaseBlueprint()
        {
            var print = new ShipBlueprint(4, 3);
            print.AddShipPart(BasicShipPart.GetHull());
            print.AddShipPart(BasicShipPart.GetHull());
            print.SetBonus(1, 5, 2);
            print.AddShipPart(BasicShipPart.GetElectronComputer());
            return print;
        }

        protected override void SetupStorage(PlayerBoard board)
        {
            board.MoneyStorage = board.ScienceStorage = board.MaterialsStorage = 4;
        }
    }
}
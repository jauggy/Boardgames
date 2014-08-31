using Eclipse.Models.Hexes;
using Eclipse.Models.Ships;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Eclipse.Models.Unique
{
    public class EridaniUnique : UniqueMethods
    {

        public override void PopulateStartingHex(Hex hex)
        {
            hex.AddOrangePlanet(2, 1);
            hex.AddPinkPlanet(2, 1);
        }



        protected override List<string> GetStartingTechnolyNames()
        {
            return new List<String> { "Gauss Shield", "Fusion Drive", "Plasma Cannon" };
        }

        public override Playerboards.ShipBlueprint CreateDreadnoughtBlueprint()
        {
            var print  = base.CreateDreadnoughtBlueprint();
            print.SetBonus(0, 1, 0);
            return print;
        }

        protected override void SetupStorage(PlayerBoard board)
        {
            board.MoneyStorage = 26;
            board.ScienceStorage = 2;
            board.MaterialsStorage = 4;
        }
    }
}
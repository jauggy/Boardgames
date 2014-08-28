using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Web;
using Eclipse.Models.Hexes;
using Eclipse.Models.Tech;
using Eclipse.Models.Playerboards;

namespace Eclipse.Models
{
    public abstract class UniqueMethods
    {
        //CreateStartingHex
        public abstract void PopulateStartingHex(Hex hex);

        //DrawStartingReputation
        //Move
        //InitInfluenceDisks
        //InitShipBlueprints
        //InitTechnologies
        //GetTradeRate
        //InitStartingStorage

        public virtual int GetNumberMovableShips()
        {
            return 2;
        }
        public virtual void SetupPlayerboard(PlayerBoard board)
        {
            board.AddStartingTechs(GetStartingTechnolyNames());
        }

        protected abstract List<String> GetStartingTechnolyNames();
   

        public String GetName()
        {
            return this.GetType().Name.Replace("Unique", "");
        }

        public virtual ShipBlueprint CreateInterceptorBlueprint()
        {
            var print = new ShipBlueprint();
            print.AddShipPart(BasicShipPart.GetIonCannon());
            print.AddShipPart(BasicShipPart.GetNuclearDrive());
            print.AddShipPart(BasicShipPart.GetNuclearSource());
            print.Size = 4;
            print.MaterialCost = 3;
            print.InitiativeBonus = 2;
            return print;
        }

        public virtual ShipBlueprint CreateCruiserBlueprint()
        {
            var print = CreateInterceptorBlueprint();
            print.Size = 6;
            print.AddShipPart(BasicShipPart.GetHull());
            print.AddShipPart(BasicShipPart.GetElectronComputer());
            print.MaterialCost = 5;
            print.InitiativeBonus = 1;
            return print;
            
        }

        public virtual ShipBlueprint CreateDreadnoughtBlueprint()
        {
            var print = CreateCruiserBlueprint();
            print.Size = 8;
            
            print.MaterialCost = 8;
            print.AddShipPart(BasicShipPart.GetIonCannon());
            print.AddShipPart(BasicShipPart.GetHull());
            return print;
        }

        public virtual ShipBlueprint CreateStarbaseBlueprint()
        {
            var print = new ShipBlueprint();
            print.InitiativeBonus = 4;
            print.EnergySourceBonus = 3;
            print.Size = 4;
            print.MaterialCost = 4;
            print.AddShipPart(BasicShipPart.GetHull());
            print.AddShipPart(BasicShipPart.GetHull());
            print.AddShipPart(BasicShipPart.GetElectronComputer());
            print.AddShipPart(BasicShipPart.GetIonCannon());
            return print;
        }
    }
}
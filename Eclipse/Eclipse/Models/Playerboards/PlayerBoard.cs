using Eclipse.Models.Hexes;
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
        public List<Technology> Technologies { get; private set; }
        public List<ShipPart> AncientShipParts { get; set; }
        public String Log { get; private set; }
        public ShipBlueprint InterceptorBlueprint { get; set; }
        public ShipBlueprint CruiserBlueprint { get; set; }
        public ShipBlueprint DreadnoughtBlueprint { get; set; }
        public ShipBlueprint StarbaseBlueprint { get; set; }
        public int MoneyStorage { get { return Storage[PopulationType.Money]; } set { Storage[PopulationType.Money] = value; } }
        public int MaterialsStorage { get { return Storage[PopulationType.Materials]; } set { Storage[PopulationType.Materials] = value; } }
        public int ScienceStorage { get { return Storage[PopulationType.Science]; } set { Storage[PopulationType.Science] = value; } }
        [ScriptIgnore]
        public Dictionary<PopulationType, int> Storage { get; set; }
        [ScriptIgnore]
        public Dictionary<PopulationType, int> PopulationsCubes { get; set; }
        public TechnologySegment[] TechnologySegments { get{return   GetTechSegments(); } }
        public ResourceSegment[] ResourceSegments { get { return GetResourceSegments(); } }


        private List<int> _techDiscounts = new List<int> { 0, 1, 2, 3, 4, 6, 8 };
        private List<int> _upkeepCosts = new List<int> { 30, 25, 21, 17, 13, 10, 7, 5, 3, 2, 1, 0, 0 };
        public PlayerBoard()
        {
            AncientShipParts = new List<ShipPart>();
            InfluenceDisks = 13;
            Technologies = new List<Technology>();
            PopulationsCubes = new Dictionary<PopulationType, int>();
            Storage = new Dictionary<PopulationType, int>();
            Storage[PopulationType.Money] = 0;
            Storage[PopulationType.Science] = 0;
            Storage[PopulationType.Materials] = 0;
            PopulationsCubes.Add(PopulationType.Money, 12);
            PopulationsCubes.Add(PopulationType.Science, 12);
            PopulationsCubes.Add(PopulationType.Materials, 12);
        }

        public TechnologySegment[] GetTechSegments()       
        {
            var list = new List<TechnologySegment>();
            list.Add(CreateTechnologySegment(TechnologyType.Military));
            list.Add(CreateTechnologySegment(TechnologyType.Grid));
            list.Add(CreateTechnologySegment(TechnologyType.Nano));
            return list.ToArray();
        }

        private ResourceSegment[] GetResourceSegments()
        {
            ResourceSegment[] list = new ResourceSegment[3];
            list[0] = new ResourceSegment(PopulationType.Money, this);
            list[1] = new ResourceSegment(PopulationType.Science, this);
            list[2] = new ResourceSegment(PopulationType.Materials, this);

            return list;
        }

        private TechnologySegment CreateTechnologySegment(TechnologyType type)
        {
            var segment1 = new TechnologySegment(type.ToString(), Technologies.Where(x => x.Type == type).ToList());
            var footer = new List<String>();
            footer.Add("Discount: " + GetDiscount(type));
            footer.Add("Next:  " + GetDiscountNext(type));
            footer.Add("VP: " + GetVP(type));
            footer.Add("Next VP: " + GetVPNext(type));

            segment1.Footer = footer.ToArray();
            return segment1;
        }

        public int GetDiscount(TechnologyType type)
        {
            var numTechs = Technologies.Where(x => x.Type == type).Count();
            if (numTechs >= _techDiscounts.Count) 
                numTechs = _techDiscounts.Count - 1;
            return _techDiscounts[numTechs];
        }

        public int GetDiscountNext(TechnologyType type)
        {
            var numTechs = Technologies.Where(x => x.Type == type).Count()+1;
            if (numTechs >= _techDiscounts.Count)
                return _techDiscounts[_techDiscounts.Count - 1];
            return _techDiscounts[numTechs];
        }

        public int GetVP(TechnologyType type)
        {
            return 0;
        }

        public int GetVPNext(TechnologyType type)
        {
            return 0;
        }

        public int GetNextUpkeep()
        {
            return _upkeepCosts[Math.Min(_upkeepCosts.Count - 1, InfluenceDisks-1)];
        }

        public int GetUpkeep()
        {
            return _upkeepCosts[Math.Min(_upkeepCosts.Count-1,InfluenceDisks)];
        }

        public void AddInfluenceDisk()
        {
            AdjustInfluenceDisks(1);
        }

        public void RemoveInfluenceDisk()
        {
            AdjustInfluenceDisks(-1);
           
        }

        public void AdjustInfluenceDisks(int adjust)
        {
            LogNetProductionBefore(PopulationType.Money);
            InfluenceDisks = InfluenceDisks + adjust;
            LogNetProductionAfter(PopulationType.Money);
        }

        private int _beforeLog;
        private void LogNetProductionBefore(PopulationType popType)
        {
            _beforeLog = GetProduction(popType);
                if(popType==PopulationType.Money)
                    _beforeLog -= GetUpkeep();
        }

        private void LogNetProductionAfter(PopulationType popType)
        {
            var after = GetProduction(PopulationType.Money);
            if(popType==PopulationType.Money)
                after -= GetUpkeep();
            AddLogLine("Net " + popType+ " Production: " + SignedNumber(_beforeLog, after));
        }

        private void AddLogLine(String msg)
        {
            Log = msg + "</br>" + Log;
        }

        public void AddPopulationCube(PopulationType type)
        {
            if (type == PopulationType.Unknown)
                throw new NotImplementedException();
            PopulationsCubes[type]++;
        }

        public void AddStartingTechs(List<String> list)
        {
            foreach(var name in list)
            {
                var tech =GameState.GetInstance().SupplyBoard.GetTechnologyWithoutRemove(name);
                Technologies.Add(tech);
            }
            
        }

        public int GetProduction(PopulationType type)
        {

                return GetProductionLevel(PopulationsCubes[type]);

        }

        public int GetNextProduction(PopulationType type)
        {
            return GetProductionLevel(PopulationsCubes[type]-1);
        }

        public int GetNetProduction(PopulationType t)
        {
            if (t == PopulationType.Money)
            {
                return GetProduction(PopulationType.Money) - GetUpkeep();
            }
            else
                return GetProduction(t);
        }

        private int GetProductionLevel(int numPopCubes)
        {
            var list = new List<int> { 28, 24, 21, 18, 15, 12, 10, 8, 6, 4, 3, 2 };
            if (numPopCubes >= list.Count)
                return 0;
            return list[numPopCubes];
        }

        public void RemovePop(PopulationType type)
        {
            AdjustPop(type, -1);
        }

        private void AdjustPop(PopulationType type, int adjust)
        {
            LogNetProductionBefore(type);
            PopulationsCubes[type] += adjust;
            LogNetProductionAfter(type);
        }

        public void ResetLog()
        {
            var before = GetProduction(PopulationType.Money)- GetUpkeep();

            var after = GetProduction(PopulationType.Money) - GetNextUpkeep();

            Log = "After the next action, net Money production: " + SignedNumber(before, after);
            AddLogLine("Money Storage: " + (GetStorage(PopulationType.Money)));
        }

        public String SignedNumber(int before, int after)
        {
            return after +" ("+SignedNumber(after - before) + ")";
        }

        private String SignedNumber(int num)
        {
            if(num >=0)
            {
                return "+" + num;
            }
            else
            {
                return "" + num;
            }
        }

        public int GetStorage(PopulationType type)
        {
            return Storage[type];
        }

        public void AdjustStorage(PopulationType type, int adjustment)
        {
            var before = Storage[type];
            Storage[type] += adjustment;
            var after = Storage[type];
            AddLogLine(type.ToString() + " Storage: " + SignedNumber(before, after));
        }

        public void SetStorage(PopulationType type, int value)
        {
            Storage[type] = value;
        }
        public ShipBlueprint GetBlueprint(String name)
        {
            if (name == "Interceptor")
                return InterceptorBlueprint;
            else if (name == "Cruiser")
                return CruiserBlueprint;
            else if (name == "Dreadnought")
                return DreadnoughtBlueprint;
            else if (name == "Starbase")
                return StarbaseBlueprint;
            else throw new NotImplementedException();
       }

        public List<ShipPart> GetAvailableShipParts()
        {
            var result = new List<ShipPart>();
            var list =  Technologies.Where(x => x.ShipPart != null).Select(x => x.ShipPart).Distinct().ToList();
            result.AddRange(list);
            result.AddRange(BasicShipPart.GetAllBasics());
            result.AddRange(AncientShipParts);
            return result;
        }

        public String GetCurrentProductionDescription(PopulationType type)
        {
           return  String.Format("Net {0} Production: {1}", type.ToString(), GetNetProduction(type));
        }

        public String GetCurrentStorageDescription(PopulationType type)
        {
            return String.Format("{0} Storage: {1}", type, GetStorage(type));
        }

    }
}
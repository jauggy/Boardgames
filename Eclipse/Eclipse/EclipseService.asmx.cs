using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Web;
using System.Web.Services;
using Eclipse.Models.Hexes;
using Eclipse.Models;
using Eclipse.Models.UI;
using Eclipse.Models.Supply;

namespace Eclipse
{
    /// <summary>
    /// Summary description for WebService1
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    [System.Web.Script.Services.ScriptService]
    public class WebService1 : System.Web.Services.WebService
    {

        [WebMethod(true)]
        public Size GetCanvasDimensions()
        {
            return CanvasHelper.GetCanvasDimensions();
        }

        [WebMethod(true)]
        public void Init()
        {
            HexBoard.GetInstance().Setup();
        }

        [WebMethod(true)]
        public HexBoard GetHexBoard()
        {
            return HexBoard.GetInstance();
        }

        [WebMethod(true)]
        public Hex GetNearestHex(double x, double y)
        {
            return HexBoard.GetInstance().GetNearestHexbyCanvasLocation((int)x, (int)y);
        }

        [WebMethod(true)]
        public CanvasConstants GetCanvasConstants()
        {
            return new CanvasConstants();
        }

        public List<Point> GetNeighbourHexPoints()
        {
            throw new NotImplementedException();
        }

        [WebMethod(true)]
        public List<Hex> GetExploreFromHexes()
        {
            return HexBoard.GetInstance().GetExploreFromHexes();
        }

         [WebMethod(true)]
        public List<Hex> GetExploreToHexes(int x, int y)
        {
            var hex = HexBoard.GetInstance().FindHex(new Point(x, y));
            return HexBoard.GetInstance().GetExploreToHexes(hex);
        }

        [WebMethod(true)]
        public List<DropDownMenu> GetExploreToMenus(int x, int y)
         {
             var hexes = GetExploreToHexes(x, y);
             var nextUpkeep = GameState.GetInstance().CurrentPlayer.PlayerBoard.GetNextUpkeep();
             var upkeep = GameState.GetInstance().CurrentPlayer.PlayerBoard.GetUpkeep();
             var msg = string.Format("Add influence +{0} upkeep = {1}",nextUpkeep-upkeep,nextUpkeep);
             var list = new List<DropDownMenu>();
            foreach(var hex in hexes)
            {
                var menu = new DropDownMenu();
                menu.Heading = "Explore to";
                menu.Hex = hex;
                menu.MenuItems = new String[] { msg, "No influence" };
                list.Add(menu);
            }

            return list;
         }

         [WebMethod(true)]
        public Hex ExploreTo(int x, int y)
         {
             GameState.GetInstance().HasDoneMainAction = true;
             var result =  HexBoard.GetInstance().ExploreTo(new Point(x, y));
             if (!result.IsVisible)
                 throw new NotImplementedException();

             return result;
         }

         [WebMethod(true)]
        public Hex Rotate(int x, int y)
         {
             var hex =  HexBoard.GetInstance().FindHex(new Point(x, y));
             hex.Rotate(null);
             return hex;
         }

      /*  [WebMethod(true)]
        public AfterPopulateUI AddPopulationToHex(int x, int y, string popType)
         {
             var pType =(PopulationType) Enum.Parse(typeof(PopulationType), popType);
             var hex = HexBoard.GetInstance().FindHex(new Point(x, y));
             HexBoard.GetInstance().AddPopulationToSelectedHex(pType, hex);

             var ui = new AfterPopulateUI();
             ui.Hex = hex;
             ui.PopulateMenus = GetPopulateMenus();
             ui.MainNavbarUI = new MainNavbarUI();
             return ui;
         }*/

        [WebMethod(true)]
            public List<TempMenu> GetPopulateMenus()
        {
            var result = new List<TempMenu>();
            var hexes = HexBoard.GetInstance().Hexes;
            foreach(var hex in hexes)
            {
                var types = hex.GetPopulatableTypes(false);
                if (types.Count > 0)
                {
                    result.Add(new TempMenu(types, hex));
                }
            }

            return result;
        }
        [WebMethod(true)]
        public PlayerBoardUI GetPlayerBoardUI()
        {
            return new PlayerBoardUI();
        }

        [WebMethod(true)]
        public SupplyBoard GetSupplyBoard()
        {
            return GameState.GetInstance().SupplyBoard;
        }

        /// <summary>
        /// Show hexes that you have an influence disk on
        /// </summary>
        /// <returns></returns>
        [WebMethod(true)]
        public List<TempMenu> GetInfluenceFromMenus()
        {
            var currentPlayer = GameState.GetInstance().CurrentPlayer;
            var hexes =  HexBoard.GetInstance().Hexes.Where(x => x.Controller==(currentPlayer));
            var result = new List<TempMenu>();
            foreach (var hex in hexes)
            {
                result.Add(new TempMenu(new List<String> { "Influence from" }, hex));
            }

            return result;
        }

        [WebMethod(true)]
        public List<TempMenu> GetInfluenceToMenus()
        {
            var currentPlayer = GameState.GetInstance().CurrentPlayer;
            var hexesAll = HexBoard.GetInstance().Hexes.ToList();
            var hexes = hexesAll.Where(x => x.IsInfluenceToPossible(currentPlayer)).ToList();
            var result = new List<TempMenu>();
            foreach (var hex in hexes)
            {
                result.Add(new TempMenu(new List<String> { "Influence to" }, hex));
            }

            return result;
        }

        [WebMethod(true)]
        public MainNavbarUI GetMainNavbarUI()
        {
            return new MainNavbarUI();
        }

         [WebMethod(true)]
        public AddInfluenceUI GetAddInfluenceUI()
        {
            return new AddInfluenceUI();
        }

         [WebMethod(true)]
         public AddInfluenceUI AddInfluenceToLastHex()
         {
             var hex = HexBoard.GetInstance().LastSelectedHex;
             hex.AddInfluence();
             return new AddInfluenceUI();
         }

        [WebMethod(true)]
        public Hex InfluenceFromHexToPlayerBoard(int x, int y)
         {
             return HexBoard.GetInstance().FindHex(x, y).RemoveInfluence();
         }

        [WebMethod(true)]
        public void Research(String techname)
        {
            GameState.GetInstance().HasDoneMainAction = true;
        }

        [WebMethod(true)]
        public MainNavbarUI NextPlayer()
        {
            GameState.GetInstance().NextPlayer();
            return new MainNavbarUI();
        }
           [WebMethod(true)]
        public UpgradeUI GetUpgradeUI(String shipType)
        {
            var ug = new UpgradeUI(shipType);
            GameState.GetInstance().UpgradeUI = ug;
            return ug;
        }

        [WebMethod(true)]
        public UpgradeUI SwapUpgradeParts(int workshopId, int availableId)
        {
            var ug = GameState.GetInstance().UpgradeUI;
            ug.Swap(workshopId, availableId);
            return ug;
        
        
        }

         [WebMethod(true)]
        public void Upgrade()
        {
            var ug = GameState.GetInstance().UpgradeUI;
            ug.ExecuteUpgrade();
            GameState.GetInstance().HasDoneMainAction = true;
        }

        [WebMethod(true)]
        public AfterPopulateUI PopulateUnknown(String popType)
         {
             throw new NotImplementedException();
           //  GameState.GetInstance().HexBoard.LastSelectedHex.AddPopulation(popType);
         }

        [WebMethod(true)]
        public AddPopUI GetAddPopUI(int x , int y)
        {

            return new AddPopUI(x, y);
        }

        /// <summary>
        /// arg will be comma seperated string like Money, Advanced Money, Unknown Money Advanced...
        /// </summary>
        /// <param name="arg"></param>
         [WebMethod(true)]
        public Hex AddInfluenceAndPopulate(IEnumerable<String> args)
         {
             var lastHex = HexBoard.GetInstance().LastSelectedHex;
             lastHex.AddInfluence();
             lastHex.PopulateByString(args);

             return lastHex;
         }

        [WebMethod(true)]
        public CombatSimUI GetCombatSimUI()
         {
             GameState.GetInstance().CombatSimUI =  new CombatSimUI("Ancient");
             return GameState.GetInstance().CombatSimUI;
         }

          [WebMethod(true)]
        public CombatSimUI SetCombatSimDefender(String name)
        {
            GameState.GetInstance().CombatSimUI.ChangeEnemy(name);
            return GameState.GetInstance().CombatSimUI;
        }

          [WebMethod(true)]
          public CombatSimUI Simulate(IEnumerable<String> attacker, IEnumerable<String> defender, int numberOfSimulations)
          {
              var model = GameState.GetInstance().CombatSimUI;
              model.Simulate(attacker, defender, numberOfSimulations);
              return model;
          }

        [WebMethod(true)]
        public List<TempMenu> GetBuildMenus()
          {
              GameState.GetInstance().HasDoneMainAction = true;
            var currentPlayer = GameState.GetInstance().CurrentPlayer;
            var hexes = HexBoard.GetInstance().Hexes.Where(x => x.Controller == currentPlayer);
            return hexes.Select(x => new TempMenu(new List<String> { "Build" }, x)).ToList();
          }
    }
}

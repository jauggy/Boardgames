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
             return HexBoard.GetInstance().ExploreTo(new Point(x, y));
         }

         [WebMethod(true)]
        public Hex Rotate(int x, int y)
         {
             var hex =  HexBoard.GetInstance().FindHex(new Point(x, y));
             hex.Rotate(null);
             return hex;
         }

        [WebMethod(true)]
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
         }

        [WebMethod(true)]
            public List<TempMenu> GetPopulateMenus()
        {
            var result = new List<TempMenu>();
            var hexes = HexBoard.GetInstance().Hexes;
            foreach(var hex in hexes)
            {
                var types = hex.GetPopulatableTypes();
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
        public Hex AddInfluenceToLastHex()
         {
             var hex = HexBoard.GetInstance().LastSelectedHex;
             hex.AddInfluence();
             return hex;
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
        }
    }
}

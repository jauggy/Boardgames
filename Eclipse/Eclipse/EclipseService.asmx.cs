using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Web;
using System.Web.Services;
using Eclipse.Models.Hexes;
using Eclipse.Models;
using Eclipse.Models.UI;

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
        public Hex ExploreTo(int x, int y)
         {
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
        public Hex AddPopulationToHex(int x, int y, string popType)
         {
             var pType =(PopulationType) Enum.Parse(typeof(PopulationType), popType);
             var hex = HexBoard.GetInstance().FindHex(new Point(x, y));
             HexBoard.GetInstance().AddPopulationToSelectedHex(pType, hex);
             return hex;
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

    }
}

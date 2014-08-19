using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Web;
using System.Web.Services;
using Eclipse.Models.Hexes;

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

        [WebMethod(true)]//use true to use session variables
        public Hex HelloWorld()
        {
            return new Hex();
        }

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

    }
}

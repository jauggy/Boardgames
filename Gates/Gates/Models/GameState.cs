using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GatesOfLoyang.Models
{
    public class GameState
    {

        public static GameState Instance
        {
            get { return GetInstance(); }
        }

        public static GameState GetInstance()
        {
            if (HttpContext.Current.Session["GameState"] == null)
            {
                var gs = new GameState();
                HttpContext.Current.Session["GameState"] = gs;
               
            }

            return (GameState)HttpContext.Current.Session["GameState"];
        }

        private GameState()
        {
            
        }
    }
}
using Eclipse.Models.Hexes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Eclipse.Models.UI
{
    public class AfterPopulateUI
    {
        public MainNavbarUI MainNavbarUI { get; set; }
        public List<TempMenu> PopulateMenus { get; set; }
        public Hex Hex { get; set; }
        public String Log { get; set; }

        public AfterPopulateUI()
        {
            Log = GameState.GetInstance().CurrentPlayer.PlayerBoard.Log;
        }
    }
}
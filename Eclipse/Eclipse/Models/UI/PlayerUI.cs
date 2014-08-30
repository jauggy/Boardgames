using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Eclipse.Models.UI
{
    public class PlayerUI
    {
        public PlayerBoard PlayerBoard { get; set; }
        public String Name { get; set; }

        public PlayerUI(Player player)
        {
            Name = player.Name;
            PlayerBoard = player.PlayerBoard;
        }

    }
}
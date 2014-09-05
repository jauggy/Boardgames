using Eclipse.Models.Hexes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Eclipse.Models.UI
{
    public class MoveUI
    {
        public Hex[] Hexes { get; set; }//hexes to redraw
      //  public TempMenu[] MoveFromMenus { get; set; }

        public MoveUI()
        {
            Hexes = new Hex[2];
            var board = HexBoard.GetInstance();
            Hexes[0] = board.LastMoveFromHex;
            Hexes[1] = board.LastSelectedHex;

         //   MoveFromMenus = board.GetMoveFromHexes().Select(x => new TempMenu("Move from", x)).ToArray();
        }
    }
}
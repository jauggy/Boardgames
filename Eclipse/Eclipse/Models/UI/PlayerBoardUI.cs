using Eclipse.Models.Tech;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Eclipse.Models.UI
{
    public class PlayerBoardUI
    {
        public List<Player> Players { get { return GameState.GetInstance().Players; } }
        public Player CurrentPlayer { get { return GameState.GetInstance().CurrentPlayer; } }

        public List<TechnologySection> TechnologySections { get; set; }

        public void PopulateSections()
        {
            TechnologySections = new List<TechnologySection>();
            foreach(var p in Players)
            {
                var board = p.PlayerBoard;
                AddSection(TechnologyType.Military, board, p);
                AddSection(TechnologyType.Grid, board, p);
                AddSection(TechnologyType.Nano, board, p);

            }
        }

        private void AddSection(TechnologyType type, PlayerBoard board, Player player)
        {
            var sec = new TechnologySection(type.ToString(), player);
            sec.Footer = GetFooter(TechnologyType.Military, board);
            sec.Technologies = board.GetTechnologies(type);
            this.TechnologySections.Add(sec);
        }

        private List<String> GetFooter(TechnologyType type, PlayerBoard board)
        {
            var list = new List<String>();
            list.Add("Discount: " + board.GetDiscount(type));
            list.Add("Next discount: " + board.GetDiscountNext(type));
            list.Add("VP: " + board.GetDiscount(type));
            list.Add("Next VP: " + board.GetDiscount(type));
            return list;
        } 
    }
}
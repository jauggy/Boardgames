using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Tzolkien.Models.BoardState
{
    public class BoardState
    {
        public PalenqueWheel PalenqueWheel { get; set; }
        public int NumberOfPlayers { get; set; }

        public static BoardState GetInstance()
        {
            return null;
        }
    }
}
﻿using Eclipse.Models.Tech;
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

     
    }
}
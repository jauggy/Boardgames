using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Web;

namespace Eclipse.Models
{
    public enum PopulationType
    {
        Money, 
        Science, 
        Materials, 
        Unknown
    }

    public class PopulationSquare
    {
        public bool IsOccupied { get { return Owner != null; } }
        public Player Owner { get; set; }
        public PopulationType Type { get; set; }
        public bool IsAdvanced { get; set; }
        public Point CanvasLocation { get; set; }
        public String Color { get { return GetColor(); } }

        public PopulationSquare() { }
        public PopulationSquare(PopulationType type)
        {
            Type = type;
        }

        public PopulationSquare Copy()
        {
            return null;
        }

        public String GetColor()
        {
            if (Type == PopulationType.Materials)
            {
                return "#663300";
            }
            else if(Type == PopulationType.Science)
            {
                return "#CC66FF";
            }
            else if(Type == PopulationType.Money)
            {
                return "#FF6600";
            }
            else
            {
                return "#909090";
            }
        }

        public String GetDescription()
        {
            var msg = "";
            if (IsAdvanced)
                msg += "Advanced ";
            msg += Type.ToString();
            return msg;
        }

        public bool IsTechSufficient(Player player)
        {
            if (!IsAdvanced)
                return true;
            else
            {
                return player.HasAdvancedTechnology(this.Type);
            }
        }

    }
}
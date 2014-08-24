using Eclipse.Models.Hexes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Eclipse.Models
{
    public class CanvasConstants
    {
        public double ComponentSize { get; set; }
        public double CanvasWidth { get; set; }
        public double CanvasHeight { get; set; }

        public CanvasConstants()
        {
            ComponentSize = CanvasHelper.GetComponentSize();
            var size = CanvasHelper.GetCanvasDimensions();
            CanvasWidth = size.Width;
            CanvasHeight = size.Height;
        }
    }
}
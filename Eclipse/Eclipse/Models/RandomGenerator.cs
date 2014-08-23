using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Eclipse.Models
{
    public class RandomGenerator
    {
      
        public static Random GetRandom()
        {
            if (HttpContext.Current.Session["Random"] == null)
            {
                HttpContext.Current.Session["Random"] = new Random();
            }

            return (Random)HttpContext.Current.Session["Random"];
        }
    }
}
using Eclipse.Models.Tech;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Eclipse.Models
{
    public static class ListExtensions
    {
        public static List<int> Copy(this List<int> list)
        {
            var result = new List<int> (list );
            return result;
        }

        public static List<Technology> Copy(this List<Technology> list)
        {
            var result = new List<Technology>(list);
            return result;
        }
    }
}
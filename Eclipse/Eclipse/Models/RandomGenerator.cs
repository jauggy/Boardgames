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

        public static double GetDouble(double minimum, double maximum)
        {
            Random random = GetRandom();
            return random.NextDouble() * (maximum - minimum) + minimum;
        }

        public static int GetInt(double minInclusive, double maxInclusive)
        {
              Random rnd = GetRandom();
            return rnd.Next(Convert.ToInt32(minInclusive), Convert.ToInt32(maxInclusive + 1));
        }

        public static double GetAngle()
        {
            return GetDouble(0, 2 * Math.PI);
        }

        /// <summary>
        /// Returns a random index
        /// </summary>
        /// <param name="distr">index i contains the number of times this count appears</param>
        /// <returns></returns>
        public static int GetInt(List<int> distr)
        {
            var sum = distr.Sum();
            var rand = GetInt(1, sum);
            var count = 0;
            for(int i = 0; i< distr.Count; i++)
            {
                count += distr[i];
                if (rand <= count)
                    return i;
            }

            throw new NotImplementedException();
        }
    }
}
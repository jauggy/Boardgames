using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Eclipse.Models
{
    public class PopulationTrack
    {
        public List<int> _productionNumbers;
        private int _index;

        public PopulationTrack()
        {
            _productionNumbers = new List<int>
            {
                2,3,4,6,8,10,12,15,18,21,24,28
            };
        }
        public int GetProductionAmount()
        {
            return _productionNumbers[_index];
        }

       public void AddPopulationToHex()
        {

        }
    }
}
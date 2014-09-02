using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Eclipse.Models.Ships
{
    public class ShipGroup
    {
        private List<Ship> _ships = new List<Ship>();
        public ShipGroup(IEnumerable<Ship> ships)
        {
            _ships = ships.ToList();
        }

        public ShipGroup() { }

        public void AddShip(Ship ship)
        {
            _ships.Add(ship);
        }

        public List<DamageDice> GetCannonDamageDice()
        {
            return _ships.Where(x=>!x.IsDestroyed).Select(x => x.GetCannonDice()).SelectMany(x => x).ToList();
        }

        public List<DamageDice> GetMissileDamageDice()
        {
            return _ships.Where(x=>!x.IsDestroyed).Select(x => x.GetMissileDice()).SelectMany(x => x).ToList();
        }


        public bool IsAttacker { get { return _ships[0].IsAttacker; } }
    }
}
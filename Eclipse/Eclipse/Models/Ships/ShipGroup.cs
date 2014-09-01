using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Eclipse.Models.Ships
{
    public class ShipGroup
    {
        private List<Ship> _ships;
        public ShipGroup(IEnumerable<Ship> ships)
        {
            _ships = ships.ToList();
        }

        public ShipGroup() { }

        public void AddShip(Ship ship)
        {
            _ships.Add(ship);
        }

        public List<DamageDice> GetCannonDamage()
        {
            return _ships.Select(x => x.GetCannonDice()).SelectMany(x => x).ToList();
        }

        public List<DamageDice> GetMissileDamage()
        {
            return _ships.Select(x => x.GetMissileDice()).SelectMany(x => x).ToList();
        }
    }
}
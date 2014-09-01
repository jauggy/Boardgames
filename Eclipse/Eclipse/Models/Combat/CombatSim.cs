using Eclipse.Models.Ships;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Eclipse.Models.Combat
{
    public class CombatSim
    {
        public void Simulate(IEnumerable<Ship> attackers, IEnumerable<Ship> defenders)
        {
            attackers.ToList().ForEach(x => x.IsAttacker = true);
            defenders.ToList().ForEach(x => x.IsAttacker = false);

            var list = GetShipsOrdered(attackers, defenders);
            var groups = FormShipGroups(list);

            foreach(var group in groups)
            {
                group.GetMissileDamage();
            }
            //Fire missiles
            //Initative order //defender if tied
            //Hit >= 6. Add computers subtract sheidls
            //1 damage destroys ship add 1 for each hull
        }

        private List<Ship> GetShipsOrdered(IEnumerable<Ship> attackers, IEnumerable<Ship> defenders)
        {
            //var list = new List<Ship> { attacker, defender };
            var combined = new List<Ship>();
            combined.AddRange(attackers);
            combined.AddRange(defenders);

            return combined.OrderBy(x => x.Initiative).ThenBy(x => x.IsAttacker).ToList();
         
        }

        public List<ShipGroup> FormShipGroups(List<Ship> shipsOrdered)
        {
            Ship lastShip = null;
            var result = new List<ShipGroup>();
            foreach (var s in shipsOrdered)
            {
                if (lastShip==null || s.Name!= lastShip.Name || s.Owner!=lastShip.Owner)
                {
                    result.Add(new ShipGroup());
                    

                }
                result[result.Count - 1].AddShip(s);
                lastShip = s;
                
            }

            return result;
        }

        public void AssignDamage(IEnumerable<Ship> targets, List<DamageDice> dice)
        {
            var ordered = targets.OrderBy(x => x.Hull).ThenByDescending(x => x.Size).ToList();

            foreach(var ship in ordered)
            {

            }
        }
    }
}
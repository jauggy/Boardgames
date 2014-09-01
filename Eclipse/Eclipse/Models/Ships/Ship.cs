using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Web;
using Eclipse.Models.Hexes;
using Eclipse.Models.Playerboards;
using Eclipse.Models.Ships;

namespace Eclipse.Models
{
    public class Ship:ShipBlueprint
    {
        public bool IsAncient { get; set; }
        public Player Owner { get; set; }
        public int ID { get; set; }
        public bool IsAttacker { get; set; }
        public String Code { get; set; }
        public Point CanvasLocation { get; set; }
        public int AssignedDamage { get; set; }
        public bool IsDestroyed { get { return AssignedDamage > Hull; } }
        public Ship()
        {

        }

        public List<DamageDice> GetMissileDice()
        {
            return MissileDamage.Select(x => new DamageDice(x, this.Computer)).ToList();
        }

        public List<DamageDice> GetCannonDice()
        {
            return CannonDamage.Select(x => new DamageDice(x, this.Computer)).ToList();
        }

        public bool AssignDamage(DamageDice dice)
        {
            if(dice.Value==6 || dice.AdjustedValue - this.Shield>= 6)
            {
                AssignedDamage++;
                return true;
            }
            else
                return false;
        }


    }
}
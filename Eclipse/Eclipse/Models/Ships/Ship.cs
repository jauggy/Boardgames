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
    public class Ship
    {
        public String Name { get; set; }
        public int Movement { get { return _blueprint.Movement; } }
        public int Size { get { return _blueprint.Size; } }
        public List<int> MissileDamage { get { return _blueprint.MissileDamage; } }
        public List<int> CannonDamage { get { return _blueprint.CannonDamage; } }
        public int Hull { get { return _blueprint.Hull; } }
        public int Shield { get { return _blueprint.Shield; } }
        public int Initiative { get { return _blueprint.Initiative; } }
        public int Computer { get { return _blueprint.Computer; } }
        public bool IsAncient { get; set; }
        public Player Owner { get; set; }
        public int ID { get; set; }
        public bool IsAttacker { get; set; }
        public String Code { get; set; }
        public Point CanvasLocation { get; set; }
        public int AssignedDamage { get; set; }
        public bool IsDestroyed { get { return Health <= 0; } }
        public int Health { get { return Hull - AssignedDamage + 1; } } //Alive if 1 or more
        public String Description { get { return ShipHelper.GetDescription(this); } }
        //We use a func so when the player board changes its print, the ships get updated automatically
        protected Func<ShipBlueprint> _getBlueprintFunc;
        protected ShipBlueprint _blueprint { get { return _getBlueprintFunc();}  }
        public Ship()
        {

        }

        private Ship(Func<ShipBlueprint> getPrint)
        {
            //EnergyRequirement = print.EnergyRequirement;
            _getBlueprintFunc = getPrint;
            Name = _blueprint.Name;
        }

        public Ship(Func<ShipBlueprint> getPrint, Player owner, String code)
            : this(getPrint)
        {
            Owner = owner;
            Code = code;

        }

        public List<DamageDice> GetMissileDice()
        {
            return MissileDamage.Select(x => new DamageDice(x, this.Computer)).ToList();
        }

        public List<DamageDice> GetCannonDice()
        {
            return CannonDamage.Select(x => new DamageDice(x, this.Computer)).ToList();
        }

        public List<DamageDice> AssignDamage(IEnumerable<DamageDice> dice)
        {
            foreach(var d in dice)
            {
                AssignDamage(d);
            }

            return dice.Where(x => !x.IsUsed).ToList();
        }

        public bool AssignDamage(DamageDice dice)
        {
            if(Health>0 && IsHit(dice))
            {
                dice.IsUsed = true;
                AssignedDamage++;
                return true;
            }
            else
                return false;
        }

        private bool IsHit(DamageDice dice)
        {
            if (dice.Value == 1)
                return false;
            else if (dice.Value == 6 || dice.AdjustedValue - this.Shield >= 6)
                return true;
            else
                return false;
        }

        public List<DamageDice> AssignToKill(IEnumerable<DamageDice> dice)
        {
            var potentialDamage = dice.Count(x => IsHit(x));
            if (potentialDamage >= Health)
            {
                return AssignDamage(dice);
            }
            else
                return dice.ToList();
        }

        public void Reset()
        {
            AssignedDamage = 0;
        }


    }
}
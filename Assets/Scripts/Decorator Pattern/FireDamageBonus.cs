using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts.Decorator_Pattern
{
    internal class FireDamageBonus : DamageBonus
    {
        private int fireDamage = 3;

        protected new WeaponItem _item;

        public FireDamageBonus(WeaponItem item) : base(item)
        {
            this._item = item;
        }

        // The flat bonus application method
        public override int DealDamage()
        {
            Debug.Log("\nThe flat bonus was " + fireDamage);

            return fireDamage + this._item.DealDamage();
        }
    }
}

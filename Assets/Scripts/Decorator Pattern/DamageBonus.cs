using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts.Decorator_Pattern
{
    public abstract class DamageBonus : WeaponItem
    {
        protected WeaponItem _item;

        public DamageBonus(WeaponItem item)
        {
            this._item = item;
        }
    }
}

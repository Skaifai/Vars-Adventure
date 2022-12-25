using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts.Decorator_Pattern
{
    internal class WoodenSword : WeaponItem
    {
        private int weaponDamage;

        public override int DealDamage()
        {
            weaponDamage = UnityEngine.Random.Range(3, 6);

            return weaponDamage;
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts.Decorator_Pattern
{
    internal class IronSword : WeaponItem
    {
        // Damage of the Irons Sword weapon
        private int weaponDamage;

        public override int DealDamage()
        {
            weaponDamage = UnityEngine.Random.Range(5, 10);

            Debug.Log("");

            return weaponDamage;
        }
    }
}

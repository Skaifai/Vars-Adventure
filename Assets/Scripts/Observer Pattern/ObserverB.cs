using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts
{
    /// <summary>
    /// A concrete observer object. Has a very specific conditional event it wants to react to.
    /// </summary>
    internal class ObserverB : MonoBehaviour, IObserver
    {
        public void Check(ISubject subject)
        {
            if (!((SomeSubject)subject).ZoneEntered) Debug.Log("Player has exited the zone!");
        }
    }
}

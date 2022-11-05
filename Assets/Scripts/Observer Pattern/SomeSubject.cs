using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts
{
    /// <summary>
    /// Concrete subject class. For now I don't know what it will represent in the game.
    /// </summary>
    /// 
    /// Maybe some type of state machine for the NPC behaviour?
    /// Maybe the command queue?
    ///
    internal class SomeSubject:MonoBehaviour, ISubject
    {
        #region Fields

        // List of observers. Can be edited at runtime
        private List<IObserver> _observers = new List<IObserver>();

        // The state that checks if the player entered a certain zone (collider)
        public bool ZoneEntered { get; set; } = false;

        // First state
        public bool FiveSecsPassed { get; set; } = false;

        // Second state
        public bool TenSecsPassed { get; set; } = false;
        #endregion

        #region Interface methods
        // Subscription method
        public void Subscribe(IObserver observer)
        {
            this._observers.Add(observer);
        }

        // Unsubscription method
        public void Unsub(IObserver observer)
        {
            this._observers.Remove(observer);
        }

        // Alert all subscribed observers
        public void Alert()
        {
            foreach (var observer in _observers)
            {
                observer.Check(this);
            }
        }
        #endregion

        #region MonoBehavior methods
        private void OnTriggerEnter2D(Collider2D collision)
        {
            if(collision.gameObject.CompareTag("Player"))
            {
                ZoneEntered = true;
                DoSomething();
            }
        }

        private void OnTriggerExit2D(Collider2D collision)
        {
            if(collision.gameObject.CompareTag("Player"))
            {
                ZoneEntered = false;
                DoSomething();
            }    
        }
        #endregion

        #region Business logic

        // This method alerts all subscribed observers
        public void DoSomething()
        {
            this.Alert();
        }
        #endregion
    }
}

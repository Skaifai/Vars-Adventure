using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// The subject interface. This subject can interact with different observers. 
/// It can sub, unsub and alert every subed observer.
/// </summary>

internal interface ISubject
{
    // Sub method signature. Just adds the specified observer object to a list
    public void Subscribe(IObserver observer);

    // Unsub method signature. Just removes the specified observer object from a list
    public void Unsub(IObserver observer);

    // Alert method signature. For each observer in a list it stores, will trigger their check method
    public void Alert();
}

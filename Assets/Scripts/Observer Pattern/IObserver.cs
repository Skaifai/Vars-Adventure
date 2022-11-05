using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// The observer interface. It can check (update) 
/// the state of the subject it is observing.
/// </summary>

internal interface IObserver
{
    public void Check(ISubject subject);
}

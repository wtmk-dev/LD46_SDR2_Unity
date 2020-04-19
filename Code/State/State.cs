using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class State : ScriptableObject
{
    public Action<State> OnStateChange;
    public State NextState;

    public void StateChange()
    {
        if(OnStateChange != null && NextState != null)
        {
            OnStateChange(NextState);
        }
    }
}

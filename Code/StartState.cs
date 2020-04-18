using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartState : IState
{
    private State state;

    public StartState(State state)
    {
        this.state = state;
    }

    public void OnStateEnter()
    {

    }

    public void OnStateUpdate()
    {

    }

    public void OnStateExit()
    {

    }
}

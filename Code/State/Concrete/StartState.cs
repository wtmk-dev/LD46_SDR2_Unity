using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StartState : IState
{
    public Action<State> OnStateChange;
    private State state;
    private Player player;
    private Button startButton;

    public StartState(State state, Player player, Button startButton)
    {
        this.state = state;
        this.player = player;
        this.startButton = startButton;

        startButton.onClick.AddListener(NewGame);
    }

    public void OnStateEnter()
    {
        
        Debug.Log("OnStateEnter: " + state);
    }

    public void OnStateUpdate()
    {
        Debug.Log("OnStateUpdate: " + state);
    }

    public void OnStateExit()
    {
        startButton.gameObject.SetActive(false);
        Debug.Log("OnStateExit: " + state);
    }

    public void StateChange()
    {
        if (OnStateChange != null)
        {
            OnStateChange(state.NextState);
        }
    }

    private void NewGame()
    {
        player.InitNewGame();
        Debug.Log("New Game");

        //Debug :: 
        state.StateChange();
    }
}

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
    private SoundManager soundManager;

    public StartState(State state, Player player, Button startButton)
    {
        this.state = state;
        this.player = player;
        this.startButton = startButton;
        //this.soundManager = soundManager;

        startButton.onClick.AddListener(StartGame);
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

    private void StartGame()
    {
        Debug.Log("New Game");
        player.InitNewGame();

        state.StateChange();
    }
}

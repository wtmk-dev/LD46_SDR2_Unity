using System; 
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageState : IState
{
    public Action<State> OnStateChange;
    private State state;
    private Player player;
    private Stage stage;
    private StageView view;
    private PlayerController playerController;
    private GameObject goPlayer;
    
    public StageState(State state, Player player, Stage stage, StageView view)
    {
        this.state = state;
        this.player = player;
        this.stage = stage;
        this.view = view;
    }

    public void StateChange() 
    { 
        if(OnStateChange != null)
        {
            OnStateChange(state.NextState);
        }
    }
    public void OnStateEnter() 
    {
        Debug.Log("OnStateEnter: " + state);

        if(goPlayer == null)
        {
            goPlayer = stage.Init(player,view);
            goPlayer.transform.position = player.startingPos;
        }

        if(playerController != null)
        {
            playerController = goPlayer.GetComponent<PlayerController>();
        }

        view.SetHopeMeterActive();
        view.SetSpellSlotActive();
        view.SetHudActive();
        view.SetScoreText("<slide>" +player.Score);
        view.SetNarratorText("Ready?");

        stage.StartLevel(3f); 
    }

    public void OnStateUpdate() 
    {
        view.UpdateHopeMeter(player.Hope);
    }

    public void OnStateExit() { }

    
}

using System; 
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StageState : IState
{
    public Action<State> OnStateChange;
    private State state;
    private Player player;
    private Stage stage;
    private StageView view;
    private PlayerController playerController;
    private GameObject goPlayer;
    private List<MobModel> mobs;
    private BadsModel bads;
    private List<Button> spells;

    public StageState(State state, Player player, Stage stage, StageView view, List<MobModel> mobs, BadsModel bads, List<Button> spells)
    {
        this.mobs = new List<MobModel>();

        this.state = state;
        this.player = player;
        this.stage = stage;
        this.view = view;
        this.mobs = mobs;
        this.bads = bads;
        this.spells = spells;

        
        
        spells[0].onClick.AddListener(() => Fire(0));
        spells[1].onClick.AddListener(() => Fire(1));
        spells[2].onClick.AddListener(() => Fire(2));
        spells[3].onClick.AddListener(() => Fire(3));
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

        Register();

        if(goPlayer == null)
        {
            goPlayer = stage.Init(player,view);
            goPlayer.transform.position = player.startingPos;
        }

        if(playerController == null)
        {
            playerController = goPlayer.GetComponent<PlayerController>();
            playerController.Init(player);
        }

        view.SetHopeMeterActive();
        view.SetSpellSlotActive();
        view.SetHudActive();
        view.SetScoreText("<slide>" +player.Score);
        view.SetNarratorText("Ready?");

        stage.StartLevel(4f); 
    }

    public void OnStateUpdate() 
    {
        GameOver();
        view.UpdateHopeMeter(player.Hope);
    }

    public void OnStateExit() 
    {
        view.Hide();
        Unregister();
        stage.Reset();
    }

    private void Register()
    {
       foreach(MobModel mob in mobs)
        {
            mob.OnMobScored += OnMobScored;
            mob.OnMobKiled  += OnMobKilled;
        }

        bads.OnScored += OnBadsScored;
    }

    private void Unregister()
    {
        foreach (MobModel mob in mobs)
        {
            mob.OnMobScored -= OnMobScored;
        }

        bads.OnScored -= OnBadsScored;
    }

    private void OnMobScored(float mobScored)
    {
        player.Despair += mobScored;
        stage.MoveDesiparMete(player.Despair, 1.2f);
    }

    private void OnMobKilled(int pts)
    {
        player.Score += pts * player.Level * 1000;
        player.Despair += 0.007f;
        view.SetScoreText("<rainb>" + player.Score);
    }

    private void OnBadsScored(bool bads)
    {
        player.Hope += 7;
    }

    private void Fire(int loc)
    {
        playerController.Fire(loc);
    }

    private void GameOver()
    {
        //Debug.Log(player.Despair);

        if(player.Despair >= 4.5f)
        {
            player.isWinner = true;
            state.StateChange();
        }

        if(player.Despair <= -2.8f)
        {
            player.isWinner = false;
            state.StateChange();
        }
    }

    
}

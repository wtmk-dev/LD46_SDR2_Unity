using System; 
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

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
    private AudioSource audioSource;
    private AudioClip clip;


    public StageState(State state, Player player, Stage stage, StageView view, List<MobModel> mobs, BadsModel bads, List<Button> spells, AudioSource audioSource, AudioClip clip)
    {
        this.mobs = new List<MobModel>();

        this.state = state;
        this.player = player;
        this.stage = stage;
        this.view = view;
        this.mobs = mobs;
        this.bads = bads;
        this.spells = spells;
        this.audioSource = audioSource;
        this.clip = clip;
        
        //spells[0].onClick.AddListener(() => Fire(0));
        //spells[1].onClick.AddListener(() => Fire(1));
        //spells[2].onClick.AddListener(() => Fire(2));
        //spells[3].onClick.AddListener(() => Fire(3));
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

        audioSource.clip = clip;
        audioSource.loop = true;
        audioSource.Play();
        audioSource.DOFade(0.3f, 0.8f);

        Register();

        var saveData = PlayerPrefs.HasKey("score");

        if(!saveData)
        {
            int score = 0;
            PlayerPrefs.SetInt("score", score);
            PlayerPrefs.Save();
        }

        if(goPlayer == null)
        {
           goPlayer = stage.Init(player,view);
        }

        if(playerController == null)
        {
            playerController = goPlayer.GetComponent<PlayerController>();
            playerController.Init(player, stage.GetSpellSpawnPoints());
        }

        view.SetHopeMeterActive();
        view.SetSpellSlotActive();
        view.SetHudActive();
        view.SetScoreText("<slide>" + player.Score);
        view.SetNarratorText("Ready?");

        stage.StartLevel(4f); 
    }

    public void OnStateUpdate() 
    {
        Debug.Log("OnStateUpdate " + state);
        BindKeys();
        GameOver();
        view.UpdateHopeMeter((int)player.Hope);
        view.UpdateScore(player.Score);
    }

    public void OnStateExit() 
    {
        Debug.Log("OnStateExit: " + state);
        view.Hide();
        stage.Reset();

        Unregister();
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

    private void BindKeys()
    {
        if(Input.GetKeyDown(KeyCode.A))
        {
            Fire(0);
        }else if (Input.GetKeyDown(KeyCode.S))
        {
            Fire(1);
        }else if (Input.GetKeyDown(KeyCode.D))
        {
            Fire(2);
        }else if (Input.GetKeyDown(KeyCode.F))
        {
            Fire(3);
        }else if(Input.GetKeyDown(KeyCode.Space))
        {
            Debug.Log("Space");
            RechargeMana();
        }
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

    private void RechargeMana()
    {
        Debug.Log(player.Score);
        if(player.Score > 0)
        {
            player.ManaTap();
        }
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

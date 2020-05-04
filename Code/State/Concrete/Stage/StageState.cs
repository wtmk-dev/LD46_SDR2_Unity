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
    private ShopView shopView;
    private PlayerController playerController;
    private GameObject goPlayer;
    private List<MobModel> mobs;
    private BadsModel bads;
    private List<Button> spells;
    private AudioSource audioSource;
    private AudioClip clip;
    private Dictionary<string, Button> btnDict;

    private bool isShopping = false;


    public StageState(State state, Player player, Stage stage, StageView view, ShopView shopView, List<MobModel> mobs, BadsModel bads, List<Button> spells, AudioSource audioSource, AudioClip clip)
    {
        this.mobs = new List<MobModel>();

        this.state = state;
        this.player = player;
        this.stage = stage;
        this.view = view;
        this.shopView = shopView;
        this.mobs = mobs;
        this.bads = bads;
        this.spells = spells;
        this.audioSource = audioSource;
        this.clip = clip;

        isShopping = false;
        btnDict = new Dictionary<string, Button>();

        btnDict.Add("ok",shopView.GetButton("ok"));
        btnDict["ok"].onClick.AddListener(() => StartShopping());

        btnDict.Add("no", shopView.GetButton("no"));
        btnDict["no"].onClick.AddListener(() => StopShopping());

        btnDict.Add("p", shopView.GetButton("p"));
        btnDict["p"].onClick.AddListener(() => PowerUp());

        btnDict.Add("d", shopView.GetButton("d"));
        btnDict["d"].onClick.AddListener(() => DamageUp());

        btnDict.Add("c", shopView.GetButton("c"));
        btnDict["c"].onClick.AddListener(() => ChargeUp());

        btnDict.Add("s", shopView.GetButton("s"));
        btnDict["s"].onClick.AddListener(() => SpeedUp());

        shopView.Hide();
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
        audioSource.loop = false;
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
           goPlayer = stage.Init(player,view,shopView);
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
        Debug.Log("OnStateUpdate: " + state);

        BindKeys();
        GameOver();
        view.UpdateHopeMeter((int)player.Hope);
        view.UpdateScore(player.Score);

        if(!audioSource.isPlaying)
        {
            if(!isShopping)
            {
                TransitionLevel();
            }
        }
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
        if(!isShopping)
        {
            if (Input.GetKeyDown(KeyCode.A))
            {
                Fire(0);
            }
            else if (Input.GetKeyDown(KeyCode.S))
            {
                Fire(1);
            }
            else if (Input.GetKeyDown(KeyCode.D))
            {
                Fire(2);
            }
            else if (Input.GetKeyDown(KeyCode.F))
            {
                Fire(3);
            }
            else if (Input.GetKeyDown(KeyCode.Space))
            {
                //Debug.Log("Space");
                RechargeMana(true);
            }
        }
     
        if(Input.GetKeyUp(KeyCode.Space))
        {
            RechargeMana(false);
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
        player.Score += pts * 1000;
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

    private void RechargeMana(bool isActive)
    {
        //Debug.Log(player.Score);
        if(isActive)
        {
            player.ManaTap();
        } else { }
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

    private void TransitionLevel()
    {
        isShopping = true;
        stage.PrepNextRound();
        view.PrepNextRound();
        shopView.Show();
    }

    private void StartShopping()
    {
        shopView.ShowText("You can spend your score to upgrade. When you upgrade your spell cost more tho so.. idk im bad at this kinda stuff.");
        shopView.StartShopping();
    }

    private void StopShopping()
    {

    }

    private void PowerUp()
    {
        if(player.Score >= player.Cost())
        {
            player.Score -= player.Cost();

            player.LevelUp();
            stage.LevelUp("p");
        }
        else { shopView.ShowText("not enought score <shake>player</shake> ... Kay?"); }
    }

    private void DamageUp()
    {
        Debug.Log("d");
        if (player.Score >= player.Cost())
        {
            player.Score -= player.Cost();

            player.LevelUp();
            stage.LevelUp("d");
           
        }else { shopView.ShowText("not enought score <shake>player</shake> ... Kay?"); }
    }

    private void ChargeUp()
    {
        Debug.Log("c");
        if (player.Score >= player.Cost())
        {
            player.Score -= player.Cost();

            player.LevelUp();
            stage.LevelUp("c");
        }
        else { shopView.ShowText("not enought score <shake>player</shake> ... Kay?"); }
    }

    private void SpeedUp()
    {
        Debug.Log("s");
        if (player.Score >= player.Cost())
        {
            player.Score -= player.Cost();

            player.LevelUp();
            stage.LevelUp("s");
        }
        else { shopView.ShowText("not enought score <shake>player</shake> ... Kay?"); }
    }


}

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class StartState : IState
{
    public Action<State> OnStateChange;
    private State state;
    private Player player;
    private Button startButton;
    private SoundManager soundManager;
    private StartView startView;
    private AudioSource audioSource;
    private AudioClip clip;
    private EffectSounds effectSounds;

    public StartState(State state, Player player, StartView startView, AudioSource audioSource, AudioClip clip, EffectSounds effectSounds)
    {
        this.state = state;
        this.player = player;
        this.startView = startView;
        this.audioSource = audioSource;
        this.clip = clip;
        this.effectSounds = effectSounds;
        //this.soundManager = soundManager;
    }

    public void OnStateEnter()
    {
        Debug.Log("OnStateEnter: " + state);
        startView.SetText("start", "<shake>Super Despair Shooter");
        startView.SetActive("story", false);
        startView.SetActive("pew", false);

        startView.GetButton("story").onClick.AddListener(InitModeStory);
        startView.GetButton("pew").onClick.AddListener(InitModePew);
        startView.GetButton("choice").onClick.AddListener(Uncover);

        audioSource.clip = clip;
        audioSource.loop = true;
        audioSource.Play();
        audioSource.DOFade(0.3f, 0.3f);
    }

    public void OnStateUpdate()
    {
        Debug.Log("OnStateUpdate: " + state);
    }

    public void OnStateExit()
    {
        Debug.Log("OnStateExit: " + state);

        //startView.SetText("start", "<shake>Super Despair Shooter");

        startView.SetActive("story", false);
        startView.SetActive("pew", false);
        startView.SetActive("title", false);
        startView.SetActive("st", false);
         
        startView.GetGameObject("bg").GetComponent<Image>().DOFade(0f, 3f).OnComplete( () => HideView() );
        startView.FadeObject("fg", 3f);

        audioSource.DOFade(0f, 0.3f);
    }

    public void StateChange()
    {
        if (OnStateChange != null)
        {
            OnStateChange(state.NextState);
        }
    }

    private void Uncover()
    {
        //Debug.Log("uncover");
        startView.GetButton("choice").gameObject.transform.DOMoveY(-150f,.936f);

        startView.SetActive("pew", true);

        Image pew = startView.GetButton("pew").gameObject.GetComponent<Image>();
        var color = pew.color;
        color.a = 0f;
        pew.color = color;
        pew.DOFade(1f, .336f);
    }

    private void HideView()
    {
        startView.SetActive("self", false);
    }

    private void InitModeStory()
    {
        Debug.Log("Mode: Story..."); 
        //player.InitNewGame();
        //state.StateChange();
    }

    private void InitModePew()
    {
        effectSounds.Play("start");
        Debug.Log("Mode: Endless...");
        player.InitNewGame();
        state.StateChange();
    }
}

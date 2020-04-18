using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class Main : MonoBehaviour
{
    public Action<State> OnStateChange;

    [SerializeField]
    private Image titleScreen, background;
    [SerializeField]
    private Button startButton;
    [SerializeField]
    private State init, start;
    private State currentState;
    private State previousState;

    private Dictionary<State, IState> stateHandeler;

    private void Awake()
    {
        DOTween.Init(true,true).SetCapacity(200, 10);
        startButton.onClick.AddListener(StartGame);

        InitStateHandeler();
    }

    void Update()
    {
        stateHandeler[currentState].OnStateUpdate();
    }


    private void StartGame()
    {
        titleScreen.DOFade(0, 3f);
    }

    private void InitStateHandeler()
    {
        currentState = init;
        stateHandeler = new Dictionary<State, IState>();

        stateHandeler.Add(init, new InitState(init, background) );
        stateHandeler.Add(start, new StartState(start) );

        stateHandeler[currentState].OnStateEnter();
    }
}

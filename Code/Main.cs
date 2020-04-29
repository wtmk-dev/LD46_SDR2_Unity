using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class Main : MonoBehaviour
{
    [SerializeField]
    private Image titleScreen, background;
    [SerializeField]
    private Button startButton;
    [SerializeField]
    private State init, start, stage, end;
    private State currentState;
    private State previousState;
    [SerializeField]
    private Player player;
    [SerializeField]
    private GameObject title, stagePrefab, stageUI, board, gameOverText, resetButton, endgameBg, goStartView, goShopView;
    private StartView startView;
    private Stage stageController;
    private StageView stageView;
    private Board boardController;
    private ShopView shopView;
    [SerializeField]
    private List<MobModel> mobs;
    [SerializeField]
    private BadsModel bads;
    [SerializeField]
    private List<Button> spellBtns;
    [SerializeField]
    private List<GameObject> highScoreUI;
    [SerializeField]
    private List<AudioClip> trackLists;
    private AudioSource audioSource;

    private Dictionary<State, IState> stateHandeler;

    void OnDisable()
    {
        UnregisterStateHandeler();
    }

    void Awake()
    {
        //allocate & fetch
        DOTween.Init(true,true).SetCapacity(200, 10);

        audioSource = GetComponent<AudioSource>();

        startView = goStartView.GetComponent<StartView>();
        startView.Init();

        GameObject stageClone = Instantiate(stagePrefab);
        stageController = stageClone.GetComponent<Stage>();
        stageView = stageUI.GetComponent<StageView>();
        stageView.Init();

        boardController = board.GetComponent<Board>();
        boardController.Init();

        shopView = goShopView.GetComponent<ShopView>();
        goShopView.SetActive(false); // remove

        //Init
        InitStateHandeler();
        RegisterStateHandeler();

        audioSource.clip = trackLists[0];
        audioSource.loop = true;

        currentState.StateChange();
        
    }

    void Start()
    {
        //currentState.StateChange();
       
    }

    void Update()
    {
        stateHandeler[currentState].OnStateUpdate();
    }

    private void InitStateHandeler()
    {
        currentState = init;
        stateHandeler = new Dictionary<State, IState>();

        stateHandeler.Add(init, new InitState(init, background) );
        stateHandeler.Add(start, new StartState(start,player,startView, audioSource, trackLists[0]) );
        stateHandeler.Add(stage, new StageState(stage, player, stageController, stageView,shopView, mobs, bads, spellBtns, audioSource, trackLists[1]));
        stateHandeler.Add(end,  new EndState(end,player, gameOverText, resetButton, endgameBg, highScoreUI) );

        stateHandeler[currentState].OnStateEnter();
    }

    private void RegisterStateHandeler()
    {
        foreach(KeyValuePair<State,IState> state in stateHandeler)
        {
            state.Key.OnStateChange += OnStateChange;
        }
    }

    private void UnregisterStateHandeler()
    {
        foreach (KeyValuePair<State, IState> state in stateHandeler)
        {
            state.Key.OnStateChange -= OnStateChange;
        }
    }

    private void OnStateChange(State state)
    {
        Debug.Log("State Change to: " + state + " from " + currentState);
        //Rework.. OnStateExit to retrun bool then check for compleation to move thought states
        stateHandeler[currentState].OnStateExit();

        previousState = currentState;

        //Rework.. same
        stateHandeler[currentState.NextState].OnStateEnter();

        currentState = previousState.NextState; 
    }

    
}

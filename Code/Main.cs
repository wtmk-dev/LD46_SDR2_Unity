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
    private GameObject title, stagePrefab, stageUI, board, gameOverText, resetButton, endgameBg;
    private Stage stageController;
    private StageView stageView;
    private Board boardController;
    [SerializeField]
    private List<MobModel> mobs;
    [SerializeField]
    private BadsModel bads;
    [SerializeField]
    private List<Button> spellBtns;
    [SerializeField]
    private List<GameObject> highScoreUI;

    private Dictionary<State, IState> stateHandeler;

    void OnDisable()
    {
        UnregisterStateHandeler();
    }

    void Awake()
    {
        //allocate & fetch
        DOTween.Init(true,true).SetCapacity(200, 10);

        GameObject stageClone = Instantiate(stagePrefab);
        stageController = stageClone.GetComponent<Stage>();
        stageView = stageUI.GetComponent<StageView>();
        stageView.Init();

        boardController = board.GetComponent<Board>();
        boardController.Init();

        startButton.onClick.AddListener(StartGame);

        //Init
        InitStateHandeler();
        RegisterStateHandeler();
    }

    void Update()
    {
        stateHandeler[currentState].OnStateUpdate();
    }


    private void StartGame()
    {
        titleScreen.DOFade(0, 1.8f);
        startButton.image.DOFade(.3f, 3f);
        title.SetActive(false);
        currentState.StateChange();
    }

    private void InitStateHandeler()
    {
        currentState = init;
        stateHandeler = new Dictionary<State, IState>();

        stateHandeler.Add(init, new InitState(init, background) );
        stateHandeler.Add(start, new StartState(start,player,startButton) );
        stateHandeler.Add(stage, new StageState(stage, player, stageController, stageView, mobs, bads, spellBtns));
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
        Debug.Log("State Change to: " + state + " from" + currentState);
        //Rework.. OnStateExit to retrun bool then check for compleation to move thought states
        stateHandeler[currentState].OnStateExit();

        previousState = currentState;

        //Rework.. same
        stateHandeler[currentState.NextState].OnStateEnter();

        currentState = previousState.NextState; 
    }

    
}

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using DG.Tweening;
using Febucci.UI;

public class EndState : IState
{
    public Action<State> OnStateChange;
    private State state;
    private Player player;
    private GameObject gameOverText, resetButton;
    private TextAnimatorPlayer gameOverTextAnim, highScoreTextAnim;
    private Button resetBtn;
    private GameObject background;
    private List<GameObject> highScoreUI;


    public EndState(State state, Player player, GameObject gameOverText, GameObject resetButton, GameObject background, List<GameObject> highScoreUI)
    {
        this.state = state;
        this.player = player;
        this.gameOverText = gameOverText;
        gameOverTextAnim = gameOverText.GetComponent<TextAnimatorPlayer>();
        gameOverText.SetActive(false);

        this.resetButton = resetButton;
        resetBtn = resetButton.GetComponent<Button>();
        resetBtn.onClick.AddListener(Reset);
        resetButton.SetActive(false);

        this.background = background;

        this.highScoreUI = highScoreUI;
        highScoreTextAnim = highScoreUI[0].GetComponent<TextAnimatorPlayer>();
        foreach (GameObject go in highScoreUI)
        {
            go.SetActive(false);
        }
    }

    public void OnStateEnter()
    {
        Debug.Log("OnStateEnter: " + state);
        if(PlayerPrefs.HasKey("Score"))
        {
            int score = PlayerPrefs.GetInt("Score");

            if(player.Score > score)
            {
                gameOverText.SetActive(true);
                gameOverTextAnim.ShowText("<rot>NEW HIGH SCORE!");

                PlayerPrefs.SetInt("Score", player.Score);
            }
        } else {
            PlayerPrefs.SetInt("Score", player.Score);
        }
        
        background.SetActive(true);
        gameOverText.SetActive(true);
        resetButton.SetActive(true);

        if(player.isWinner)
        {
            gameOverTextAnim.ShowText("<fade>w i n n e r");
        } else
        {
            gameOverTextAnim.ShowText("<shake>Game Over");
        }

        foreach (GameObject go in highScoreUI)
        {
            go.SetActive(true);
        }

        int hs = PlayerPrefs.GetInt("Score");

        highScoreTextAnim.ShowText("" + hs);


    }

    public void OnStateUpdate()
    {
        Debug.Log("OnStateUpdate: " + state);
    }

    public void OnStateExit()
    {
        Debug.Log("OnStateExit: " + state);
    }

    public void StateChange()
    {
        if (OnStateChange != null)
        {
            OnStateChange(state.NextState);
        }
    }

    private void Reset()
    {
        SceneManager.LoadScene(0);
    }
}

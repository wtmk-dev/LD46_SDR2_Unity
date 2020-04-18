using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class InitState : IState
{
    private State state;
    private Image camera;
    private System.Random rand;

    public InitState(State state, Image camera)
    {
        this.state = state;
        this.camera = camera;

        rand = new System.Random();
    }

    public void OnStateEnter()
    {
        Debug.Log("OnStateEnter: " + state);
        GlowScreen();
    }

    public void OnStateUpdate()
    {

    }

    public void OnStateExit()
    {

    }

    private void GlowScreen()
    {
        int roll = rand.Next(60, 120);
        camera.DOFade(0, 15f + roll);
    }
}

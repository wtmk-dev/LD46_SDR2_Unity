using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioSyncSpawn : AudioSyncer
{
    public Action<bool> OnBeatBroadcast;
    public Vector3 beathScale;
    public Vector3 resetScale;
    private float restScale;

    public AudioSyncSpawn(float bias, float timeStep, float timeToBeat, float restScale)
    {
        this.bias = bias; this.timeStep = timeStep; this.timeToBeat = timeToBeat; this.restScale = restScale;
    }

    public override void OnUpdate()
    {
        base.OnUpdate();

        if (isBeat) 
        { 
            return; 
        }
    }

    public override void OnBeat()
    {
        base.OnBeat();

        if (OnBeatBroadcast != null)
        {
            OnBeatBroadcast(isBeat);
        }
    }

    public bool IsOnBeat { get { return isBeat; } set { isBeat = value; } }
}

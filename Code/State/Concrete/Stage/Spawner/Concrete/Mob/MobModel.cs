using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class MobModel : ScriptableObject
{
    public Action<float> OnMobScored;
    public Action<int> OnMobKiled;
    public Sprite Sprite;
    public int Blood;
    public int Value;
    public float Speed;
    public float score;

    public void MobKilled()
    {
        if(OnMobKiled != null)
        {
            OnMobKiled(Value);
        }    
    }

    public void MobScored()
    {
        if (OnMobScored != null)
        {
            OnMobScored(score);
        }
    }

}

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class MobModel : ScriptableObject
{
    public Action<int> OnMobScored;
    public Sprite Sprite;
    public int Blood;
    public float Speed;
    public int score;

    public void MobScored()
    {
        if (OnMobScored != null)
        {
            OnMobScored(score);
        }
    }

}

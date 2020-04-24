using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class SpellModel : ScriptableObject
{
    public Action<MobModel> OnScore;
    public float Speed;
    public int damage = 1;

    public void Score(MobModel mob)
    {
        if(OnScore != null)
        {
            OnScore(mob);
        }
    }

    public void Init()
    {
        damage = 1;
    }

}

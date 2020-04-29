using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class SpellModel : ScriptableObject
{
    public Action<MobModel> OnScore;
    public Action<int> OnHit;

    public float speed;
    public int damage;
    public int power;

    public void Score(MobModel mob)
    {
        if(OnScore != null)
        {
            OnScore(mob);
        }
    }

    public void Hit(int dmg)
    {
        if(OnHit != null)
        {
            OnHit(dmg);
        }
    }

}

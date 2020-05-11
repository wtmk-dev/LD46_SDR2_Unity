using JetBrains.Annotations;
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

    public int speedLevel;
    public int damage;
    public int power;
    public int charge;

    public void NewSpell()
    {
        speed = 7f;
        damage = 1;
        power = 1;
        speedLevel = 1;
        charge = 1;
    }

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

    private void LevelSpeed()
    {
        float sp = speed - speedLevel;

        if(sp <= 1f)
        {
            sp = 1f;
        }

        speed = sp;
    }

}

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using Febucci.UI;

public class Mob : MonoBehaviour
{
    [SerializeField]
    private TextAnimatorPlayer dmgTextAnim;
    [SerializeField]
    private GameObject psPrefab, psgo;
    private ParticleSystem ps;
    private MobModel model;
    private SpriteRenderer spriteRenderer;
    private MobSpawner spawner;

    private int BLOOD;
    private int blood;

    private bool hasScored = false;

    private System.Random rand;

    private int currnetRound;

    public void Init(MobSpawner spawner)
    {
        rand = new System.Random();
        this.spawner = spawner;
        this.spriteRenderer = GetComponent<SpriteRenderer>();
        psgo = Instantiate(psPrefab);
        ps = psgo.GetComponent<ParticleSystem>();
    }

    public void SetActive(MobModel model, int round)
    {
        currnetRound = round;
        int bonus = rand.Next(0,round);
        this.model = model;
        spriteRenderer.sprite = model.Sprite;
        BLOOD = model.Blood + bonus;
        blood = BLOOD;
        hasScored = false;

        Move();
    }

    public void Damage(int damage)
    {
        blood -= damage;
        dmgTextAnim.ShowText("<fade>" + damage);
    }

    private void Kill()
    {
        if(blood < 0)
        {
            psgo.transform.position = gameObject.transform.position;
            ps.Play();
            int bonus = rand.Next(0, currnetRound);
            model.OnMobKiled(model.Value + bonus);
            spawner.Kill(gameObject, false);
        }
    }

    private void Score()
    {
        if(!hasScored)
        {
            if (gameObject.transform.position.y <= -3.88f)
            {
                hasScored = true;
                model.MobScored();
                spawner.Kill(gameObject,true);
            }
        }    
    }

    private void Move()
    {
        gameObject.transform.DOLocalMoveY(-4, model.Speed);
    }

    private void Update()
    {
        Score();
        Kill();
    }
}

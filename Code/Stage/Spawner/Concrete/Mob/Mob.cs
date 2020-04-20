using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using Febucci.UI;

public class Mob : MonoBehaviour
{
    [SerializeField]
    private TextAnimatorPlayer dmgTextAnim;
    private MobModel model;
    private SpriteRenderer spriteRenderer;
    private MobSpawner spawner;

    private int BLOOD;
    private int blood;

    private bool hasScored = false;

    public void Init(MobSpawner spawner)
    {
        this.spawner = spawner;
        this.spriteRenderer = GetComponent<SpriteRenderer>();
    }

    public void SetActive(MobModel model)
    {
        this.model = model;
        spriteRenderer.sprite = model.Sprite;
        BLOOD = model.Blood;
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
            model.OnMobKiled(model.Value);
            spawner.Kill(gameObject);
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
                spawner.Kill(gameObject);
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

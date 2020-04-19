using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Mob : MonoBehaviour
{
    private MobModel model;
    private SpriteRenderer spriteRenderer;

    private int BLOOD;
    private int blood;

    public void Init()
    {
        this.spriteRenderer = GetComponent<SpriteRenderer>();
    }

    public void SetActive(MobModel model)
    {
        this.model = model;
        spriteRenderer.sprite = model.Sprite;
        BLOOD = model.Blood;
        blood = BLOOD;

        Move();
    }

    private void Move()
    {
        gameObject.transform.DOLocalMoveY(-4, model.Speed);
    }
}

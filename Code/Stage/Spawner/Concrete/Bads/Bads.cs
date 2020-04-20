using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations;

public class Bads : MonoBehaviour
{
    private static System.Random random;

    [SerializeField]
    private List<Sprite> sprites;
    private Sprite sprite;
    private SpriteRenderer spriteRenderer;

    private BadsSpawner spawner;
    private BadsModel model;

    private Animator anim;

    public void Init(BadsModel model, BadsSpawner spawner)
    {
        if(random == null)
        {
            random = new System.Random();
        }

        spriteRenderer = GetComponent<SpriteRenderer>();
        this.spawner = spawner;
        this.model = model;

        anim = GetComponent<Animator>();
    }

    public void SetActive()
    {
        sprite = sprites[random.Next(sprites.Count)];
        spriteRenderer.sprite = sprite;
    }

    private void OnMouseDown()
    {
        model.Score();
        anim.SetBool("IsDead", true);
        spawner.Kill(gameObject);
    }
}

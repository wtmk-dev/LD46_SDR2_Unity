using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Spell : MonoBehaviour
{
    private static System.Random random;

    [SerializeField]
    private List<Sprite> sprites;
    private Sprite sprite;
    private SpriteRenderer spriteRenderer;

    private SpellSpawner spawner;
    private SpellModel model;

    public void Init(SpellModel model, SpellSpawner spawner)
    {
        if (random == null)
        {
            random = new System.Random();
        }

        spriteRenderer = GetComponent<SpriteRenderer>();
        this.spawner = spawner;
        this.model = model;
        model.Init();
    }

    public void SetActive()
    {
        sprite = sprites[random.Next(sprites.Count)];
        spriteRenderer.sprite = sprite;

        Move();
    }

    private void Move()
    {
        gameObject.transform.DOLocalMoveY(10, model.Speed);
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        Debug.Log(col);
        Mob mob = col.gameObject.GetComponent<Mob>();

        if(mob == null)
        {
            return;
        }

        mob.Damage(model.damage);
    }

    void Update()
    {
        Debug.Log(gameObject.transform.position.y);
        if(gameObject.transform.position.y >= 4f)
        {
            spawner.Kill(gameObject);
        }
    }
}

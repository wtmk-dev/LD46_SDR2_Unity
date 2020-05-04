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

    private float currentSpeed;
    private int currentDamage;
    private int currentPower;

    public void Init(SpellModel model, SpellSpawner spawner)
    {
        if (random == null)
        {
            random = new System.Random();
        }

        spriteRenderer = GetComponent<SpriteRenderer>();
        this.spawner = spawner;
        this.model = model;
        currentSpeed = model.speed;
        currentDamage = model.damage;
        currentPower = model.power;
    }

    public void SetActive()
    {
        sprite = sprites[random.Next(sprites.Count)];
        spriteRenderer.sprite = sprite;

        currentSpeed = model.speed;
        currentDamage = model.damage;
        currentPower = model.power;

        Move();
    }

    private void Move()
    {
        gameObject.transform.DOLocalMoveY(10, currentSpeed);
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        //Debug.Log(col);
        Mob mob = col.gameObject.GetComponent<Mob>();

        if(mob == null)
        {
            return;
        }

        mob.Damage(model.damage);

        currentPower--;
    }

    void Update()
    {
        //Debug.Log(gameObject.transform.position.y);
        if(gameObject.transform.position.y >= 4f)
        {
            model.Hit(currentDamage); 
            spawner.Kill(gameObject);
        }

        if(currentPower < 1)
        {
            spawner.Kill(gameObject);
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Spell : MonoBehaviour
{
    private static System.Random random;

    [SerializeField]
    private List<Sprite> sprites;
    [SerializeField]
    private GameObject damgePSPrefab;
    private GameObject goDamagePS;
    private ParticleSystem damagePS;
    private Sprite sprite;
    private SpriteRenderer spriteRenderer;

    private SpellSpawner spawner;
    private SpellModel model;

    private float currentSpeed;
    private int currentDamage;
    private int currentPower;
    private int currentCharge;

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
        currentCharge = model.charge;
        goDamagePS = Instantiate(damgePSPrefab);
        damagePS = goDamagePS.GetComponent<ParticleSystem>();
    }

    public void SetActive()
    {
        sprite = sprites[random.Next(sprites.Count)];
        spriteRenderer.sprite = sprite;

        currentSpeed = model.speed;
        currentDamage = model.damage;
        currentPower = model.power;
        currentCharge = model.charge;

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
        goDamagePS.transform.position = gameObject.transform.position;
        damagePS.Play();

        currentPower--;
    }

    void Update()
    {
        //Debug.Log(gameObject.transform.position.y);
        if(gameObject.transform.position.y >= 4f)
        {
            model.Hit(currentPower); 
            spawner.Kill(gameObject, true);
        }

        if(currentPower < 1)
        {
            spawner.Kill(gameObject, false);
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.Animations;

public class SpellSpawner : MonoBehaviour
{
    private static System.Random random;

    [SerializeField]
    private GameObject spellPrefab;
    [SerializeField]
    private SpellModel model;
    private Queue<GameObject> spellPool;
    private Queue<GameObject> spellQueue;
    [SerializeField]
    private List<GameObject> spawnPoints;
    private List<Animator> animators;
    private Dictionary<Vector3, Animator> animatorDict;
    private SpellSpawner spawner;
    
    private List<SummonTile> castTile;

    private PlayerController playerController;

    private void OnDisable()
    {
        if (castTile == null)
        {
            return;
        }

        foreach (SummonTile st in castTile)
        {
            UnregisterSummonTile(st);
        }
    }

    public void Allocate(int amount, List<GameObject> spawns)
    {
        spawner = GetComponent<SpellSpawner>();
        animators = new List<Animator>();
        animatorDict = new Dictionary<Vector3, Animator>();
        castTile = new List<SummonTile>();
        spellQueue = new Queue<GameObject>();

        playerController = GetComponent<PlayerController>();

        if (random == null)
        {
            random = new System.Random();
        }

        spellPool = new Queue<GameObject>();

        for (int count = 0; count < amount; count++)
        {
            GameObject clone = Instantiate(spellPrefab);
            spellPool.Enqueue(clone);
            Spell mob = clone.GetComponent<Spell>();
            mob.Init(model, spawner);
            //clone.transform.SetParent(gameObject.transform);
            clone.SetActive(false);
        }

        foreach(GameObject go in spawnPoints)
        {
            Animator anim = go.GetComponent<Animator>();
            animatorDict.Add(go.transform.position, anim);
            animators.Add(anim);
            SummonTile tile = anim.GetBehaviour<SummonTile>();
            castTile.Add(tile);
            RegisterSummonTile(tile);
        }
    }

    public void Spawn(int loc)
    {
        if(animators[loc].GetInteger("sp") > 0)
        {
            return;
        }


        GameObject clone = spellPool.Dequeue();
        clone.SetActive(true);
        clone.transform.position = spawnPoints[loc].transform.position;

        Vector3 move = new Vector3(spawnPoints[loc].transform.position.x, -3.46f, 0f);
        gameObject.transform.DOMove(move, 0.3f);

        animators[loc].SetInteger("sp", 1);

        spellQueue.Enqueue(clone);
        clone.SetActive(false);
    }

    public void Kill(GameObject clone)
    {
        //Debug.Log(clone);
        clone.SetActive(false);
        spellPool.Enqueue(clone);
    }

    private void OnSpawned(bool spawned)
    {
        GameObject clone = spellQueue.Dequeue();
        clone.SetActive(true);
        Spell mob = clone.GetComponent<Spell>();
        playerController.canCast = true;
        mob.SetActive();
    }

    private void RegisterSummonTile(SummonTile tile)
    {
        tile.OnAnimationComplete += OnSpawned;
    }

    private void UnregisterSummonTile(SummonTile tile)
    {
        tile.OnAnimationComplete -= OnSpawned;
    }
}

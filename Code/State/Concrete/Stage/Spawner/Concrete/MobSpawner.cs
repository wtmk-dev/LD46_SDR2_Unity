using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.Animations;

public class MobSpawner : MonoBehaviour
{
    private static System.Random random;

    [SerializeField]
    private GameObject mobPrefab, runner;
    [SerializeField]
    private List<MobModel> mobModels;
    private Queue<GameObject> mobPool;
    private Queue<GameObject> spawnQueue;
    private List<GameObject> spawned;

    [SerializeField]
    private List<GameObject> spawnPoints;
    private List<Animator> spawnPointAnimations;
    private List<SummonTile> summonTiles;
    [SerializeField]
    private List<GameObject> runnerSlidePoints;

    private MobSpawner spawner;
    private int currentRound;
    private Shake shake;

    private EffectSounds effectSounds;

    private void OnDisable()
    {
       if(summonTiles == null)
        {
            return;
        }

        foreach(SummonTile st in summonTiles)
        {
            UnregisterSummonTile(st);
        }
    }

    public void SetCameraAnimator(Animator cameraAnimator)
    {
        shake = GetComponent<Shake>();
        shake.Init(cameraAnimator);
    }

    public void SetEffectSounds(EffectSounds effectSounds)
    {
        this.effectSounds = effectSounds;
    }

    public void Allocate(int amount)
    {
        spawnPointAnimations = new List<Animator>();
        summonTiles = new List<SummonTile>();
        spawnQueue = new Queue<GameObject>();
        spawned = new List<GameObject>();

        spawner = GetComponent<MobSpawner>();

        if (random == null)
        {
            random = new System.Random();
        }

        mobPool = new Queue<GameObject>();

        for (int count = 0; count < amount; count++)
        {
            GameObject clone = Instantiate(mobPrefab);
            mobPool.Enqueue(clone);
            Mob mob = clone.GetComponent<Mob>();
            mob.Init(spawner);
            clone.transform.SetParent(gameObject.transform);
            clone.SetActive(false);
        }

        foreach(GameObject go in spawnPoints)
        {
            Animator anim = go.GetComponent<Animator>();
            spawnPointAnimations.Add(anim);
            SummonTile st = anim.GetBehaviour<SummonTile>();
            summonTiles.Add(st);
            RegisterSummonTile(st);
        }

        //Debug.Log(spawnPointAnimations.Count);
    }

    public void Spawn(int a, float timeToBeat, int round)
    {
        currentRound = round;
        for (int i = 0; i < a; i++)
        {
            GameObject clone = mobPool.Dequeue();
            clone.SetActive(true);
            
            var index = random.Next(spawnPoints.Count);

            clone.transform.position = spawnPoints[index].transform.position;

            runner.transform.DOMove(runnerSlidePoints[index].transform.position, timeToBeat);

            spawnPointAnimations[index].SetInteger("sp", 1);

            clone.SetActive(false);
            spawnQueue.Enqueue(clone);

            spawned.Add(clone);
        }
            
    }

    private void OnSpawned(bool isSpawn)
    {
        try
        {
            GameObject clone = spawnQueue.Dequeue();
            clone.SetActive(true);
            Mob mob = clone.GetComponent<Mob>();
            mob.SetActive(mobModels[random.Next(mobModels.Count)], currentRound);
        }
        catch { Debug.Log("Not spawn"); }
        
    }

    public void Kill(GameObject clone, bool isShake)
    {
        clone.SetActive(false);
        mobPool.Enqueue(clone);

        if(isShake)
        {
            effectSounds.Play("mobScored");
            shake.CameraShake();
        } else { effectSounds.Play("mobKilled");  }
    }

    public void KillAll()
    {
        foreach(GameObject go in spawnQueue)
        {
            go.SetActive(false);
        }

        spawnQueue.Clear();

        foreach (GameObject go in spawned)
        {
            go.SetActive(false);
        }

        spawned.Clear();
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

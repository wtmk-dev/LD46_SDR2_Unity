using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Stage : MonoBehaviour
{
    private Player player;
    private GameObject goPlayer;

    [SerializeField]
    private GameObject DesiparMeter, BadsSpawner, MobSpawner, palyerPrefab;
    private BadsSpawner badsSpawner;
    private MobSpawner mobSpawner;
    private StageView view;

    private float spawnTime = 10f;
    private bool isSpawning = false;
    private IEnumerator spawnTimer;

    private float startTime = 2f;
    private bool isStartTimeRunning = false;
    private IEnumerator startTimer;

    private float badsTime = 2f;
    private bool isbadsTimeRunning = false;
    private IEnumerator badsTimer;

    void Awake()
    {
        badsSpawner = BadsSpawner.GetComponent<BadsSpawner>();
        badsSpawner.Allocate(500);

        mobSpawner = MobSpawner.GetComponent<MobSpawner>();
        mobSpawner.Allocate(500);
    }

    public GameObject Init(Player player, StageView view)
    {
        this.view = view;
        this.player = player;

        if (goPlayer == null)
        {
            goPlayer = Instantiate(player.Prefab);
        }

        return goPlayer;
    }

    public void MoveDesiparMete(float to, float durration)
    {
        DesiparMeter.transform.DOMoveY(to, durration);
    }

    public void StartLevel(float time)
    {
        if (startTimer != null)
        {
            StopCoroutine(startTimer);
        }

        startTimer = LevelTimer(time);

        StartCoroutine(startTimer);
    }

    public void StartSpawning()
    {
        if (spawnTimer != null)
        {
            StopCoroutine(spawnTimer);
        }

        spawnTimer = SpawnTimer(player.GetSpawnTime());
        isSpawning = true;

        StartCoroutine(spawnTimer);
    }

    private void SpawnBads()
    {
        if (badsTimer != null)
        {
            StopCoroutine(badsTimer);
        }

        badsTimer = BadsTimer(player.GetBadsSpawnTimer());
        isbadsTimeRunning = true;

        StartCoroutine(badsTimer);
    }

    private IEnumerator SpawnTimer(float time)
    {
        while (isSpawning)
        {
            yield return new WaitForSeconds(time);
            mobSpawner.Spawn();
        }
    }

    private IEnumerator LevelTimer(float time)
    {
        yield return new WaitForSeconds(time);
        view.SetNarratorText("");
        StartSpawning();
        SpawnBads();
    }

    private IEnumerator BadsTimer(float time)
    {
        while (isbadsTimeRunning)
        {
            yield return new WaitForSeconds(time);
            badsSpawner.Spawn();
        }
    }
}

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
    private PlayerController playerController;
    [SerializeField]
    private SpellModel spellModel;
    [SerializeField]
    private Bark bark;

    private float spawnTime = 10f;
    private bool isSpawning = false;
    private IEnumerator spawnTimer;

    private float startTime = 2f;
    private bool isStartTimeRunning = false;
    private IEnumerator startTimer;

    private float badsTime = 2f;
    private bool isbadsTimeRunning = false;
    private IEnumerator badsTimer;

    private bool isStoryMode = false;

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
            playerController = goPlayer.GetComponent<PlayerController>();
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

        spawnTimer = SpawnTimer();
        isSpawning = true;

        StartCoroutine(spawnTimer);
    }

    private void SpawnBads()
    {
        if (badsTimer != null)
        {
            StopCoroutine(badsTimer);
        }

        badsTimer = BadsTimer();
        isbadsTimeRunning = true;

        StartCoroutine(badsTimer);
    }

    public void Reset()
    {
        StopAllCoroutines();
    }

    private IEnumerator SpawnTimer()
    {
        float time = 6f;
        float maxTime = 6f;
        while (isSpawning)
        {
            yield return new WaitForSeconds(time);

            view.SetNarratorText( bark.GetRandomBark() );
            time -= 0.3f;
            if(time < .7f && !isStoryMode)
            {
                time = maxTime;
                player.Level++;
                if(player.Level > 5)
                {
                    player.Level = 5;
                }
                view.SetNarratorText("<fade> Level UP: ... " + player.Level);
                spellModel.damage = player.Level;
            }
            mobSpawner.Spawn(player.Level);
        }
    }

    private IEnumerator LevelTimer(float time)
    {
        yield return new WaitForSeconds(time);
        view.SetNarratorText("");
        StartSpawning();
        SpawnBads();
        MoveDesiparMete(player.Despair, 2.7f);
        view.SetSpellSlotActive();
        playerController.canCast = true;
    }

    private IEnumerator BadsTimer()
    {
        float time = 3f;
        while (isbadsTimeRunning)
        {
            yield return new WaitForSeconds(time);
            time -= 0.03f;
            if(time < 1.3f)
            {
                time = 1.3f;
            }
            
            UpdateDespair();
            MoveDesiparMete(player.Despair, .3f);
            badsSpawner.Spawn(3); 
        }
    }

    private void UpdateDespair()
    {
        if (player.Hope > 100)
        {
            player.Despair += 0.03f;
        }

        if (player.Hope > 200)
        {
            player.Despair += 0.03f;
        }

        if (player.Hope > 300)
        {
            player.Despair += 0.05f;
        }

        if (player.Hope < 100)
        {
            player.Despair -= 0.03f;
        }

        if (player.Hope < 50)
        {
            player.Despair -= 0.02f;
        }

        if (player.Hope < 25)
        {
            player.Despair -= 0.01f;
        }
    }

    
}

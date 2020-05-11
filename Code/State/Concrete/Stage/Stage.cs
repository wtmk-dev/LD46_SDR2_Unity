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
    private ShopView shop;
    private PlayerController playerController;
    [SerializeField]
    private SpellModel spellModel;
    [SerializeField]
    private Bark bark;
    [SerializeField]
    private List<GameObject> spellSpawnPoints;
    private Animator cameraAnimator;

    private AudioSyncSpawn audioSync;

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
    private bool isActive = false;

    private int currentRound = 0;

    void Awake()
    {
        badsSpawner = BadsSpawner.GetComponent<BadsSpawner>();
        badsSpawner.Allocate(5);

        mobSpawner = MobSpawner.GetComponent<MobSpawner>();
        mobSpawner.Allocate(250);

        currentRound = 0;
    }

    private void Update()
    {
        if(isActive)
        {
            audioSync.OnUpdate();
        }
        
    }

    public void LevelUp(string key)
    {
        switch(key)
        {
            case "p":
                spellModel.power += 1;
                shop.PowerUpdate("Power: " + spellModel.power);
                break;
            case "d":
                spellModel.damage += 1;
                shop.DamageUpdate("Damage: " + spellModel.damage);
                break;
            case "s":
                spellModel.speedLevel += 1;
                shop.SpeedUpdate("Speed: " + spellModel.speedLevel);
                break;
            case "c":
                spellModel.charge += 1;
                shop.ChargeUpdate("Charge: " + spellModel.charge);
                break;
            default:
                break;
        }
    }

    public GameObject Init(Player player, StageView view, ShopView shop, Animator cameraAnimator, EffectSounds effectSounds)
    {
        spellModel.NewSpell();

        this.view = view;
        this.player = player;
        this.shop = shop;
        this.player.spell = spellModel;
        this.cameraAnimator = cameraAnimator;
        mobSpawner.SetCameraAnimator(cameraAnimator);
        mobSpawner.SetEffectSounds(effectSounds);

        spellModel.OnHit += ScoreHit;

        if( playerController == null )
        {
            playerController = palyerPrefab.GetComponent<PlayerController>();
        }

        audioSync = new AudioSyncSpawn(25f,1f,1.3f,2f);
        audioSync.OnBeatBroadcast += Spawn;

        return palyerPrefab;
    }

    private void ScoreHit(int pwr)
    {
        player.Despair += (float) pwr * 0.01f;
        Debug.Log(player.Despair);
        MoveDesiparMete(player.Despair, 0.3f);
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

    public void PrepNextRound()
    {
        currentRound++;
        StopAllCoroutines();
        mobSpawner.KillAll();
    }

    private void SpawnBads()
    {
        if (badsTimer != null)
        {
            StopCoroutine(badsTimer);
        }

        badsTimer = BadsTimer();
        isbadsTimeRunning = true;

        //StartCoroutine(badsTimer);
    }

    public void Reset()
    {
        StopAllCoroutines();
    }

    public List<GameObject> GetSpellSpawnPoints()
    {
        return spellSpawnPoints;
    }

    private IEnumerator SpawnTimer()
    {
        while(audioSync.IsOnBeat)
        {
            yield return new WaitForSeconds(audioSync.timeToBeat);
            mobSpawner.Spawn(1,audioSync.timeToBeat, currentRound);
        }
    }

    private IEnumerator LevelTimer(float time)
    {
        yield return new WaitForSeconds(time);
        view.SetNarratorText("");
        //StartSpawning();
        //SpawnBads();
        MoveDesiparMete(player.Despair, 2.7f);
        view.SetSpellSlotActive();
        playerController.canCast = true;
        isActive = true;
        //StartCoroutine("ScaleTimeToBeat");
    }

    private IEnumerator ScaleTimeToBeat()
    {
        while(audioSync.timeToBeat > 1.1f)
        {
            audioSync.timeToBeat -= 0.03f;
            //Debug.Log(audioSync.timeToBeat);
            yield return new WaitForSeconds(0.03f);
        }
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

    private void Spawn(bool isBeat)
    {
        if(isBeat)
        {
            StartSpawning();
        }
    }

    
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    private List<GameObject> spellSpawnPoints;
    private SpellSpawner spellSpawner;
    private Player player;

    public bool canCast = false;

    public void Init(Player player,List<GameObject> spellSpwanPoints)
    {
        this.spellSpawnPoints = spellSpwanPoints;
        this.player = player;
        spellSpawner = GetComponent<SpellSpawner>();
        spellSpawner.Allocate(250, spellSpawnPoints);
    }

    public void Fire(int loc)
    {
        if(canCast)
        {
            if(player.Hope <= 0)
            {
                return;
            }

            canCast = false;

            player.Hope -= 1;
            spellSpawner.Spawn(loc);
        }
    }
}

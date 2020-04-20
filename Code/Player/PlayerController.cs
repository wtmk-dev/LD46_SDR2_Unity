using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    [SerializeField]
    private List<GameObject> spellSpawnPoints;
    private SpellSpawner spellSpawner;
    private Player player;

    public bool canCast = false;

    public void Init(Player player)
    {
        this.player = player;
        spellSpawner = GetComponent<SpellSpawner>();
        spellSpawner.Allocate(500, spellSpawnPoints);
    }

    public void Fire(int loc)
    {
        if(canCast)
        {
            if(player.Hope <= 0)
            {
                return;
            }

            player.Hope -= 1;
            spellSpawner.Spawn(loc);
        }
    }
}

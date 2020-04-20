using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpellSpawner : MonoBehaviour
{
    private static System.Random random;

    [SerializeField]
    private GameObject spellPrefab;
    [SerializeField]
    private SpellModel model;
    private Queue<GameObject> spellPool;

    private List<Vector2> spawnPoints;

    private SpellSpawner spawner;

    public void Allocate(int amount, List<GameObject> spawns)
    {
        spawnPoints = new List<Vector2>();
        spawner = GetComponent<SpellSpawner>();

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
            clone.transform.SetParent(gameObject.transform);
            clone.SetActive(false);
        }

        foreach(GameObject go in spawns)
        {
            spawnPoints.Add(go.transform.position);
        }
    }

    public void Spawn(int loc)
    {
        GameObject clone = spellPool.Dequeue();
        clone.SetActive(true);
        Spell mob = clone.GetComponent<Spell>();
        clone.transform.position = spawnPoints[loc];

        mob.SetActive();
    }

    public void Kill(GameObject clone)
    {
        clone.SetActive(false);
        spellPool.Enqueue(clone);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MobSpawner : MonoBehaviour
{
    private static System.Random random;

    [SerializeField]
    private GameObject mobPrefab;
    [SerializeField]
    private List<MobModel> mobModels;
    private Queue<GameObject> mobPool;

    [SerializeField]
    private List<Vector2> spawnPoints;

    private MobSpawner spawner;
         
    public void Allocate(int amount)
    {
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
    }

    public void Spawn(int a)
    {
        for (int i = 0; i < a; i++)
        {
            GameObject clone = mobPool.Dequeue();
            clone.SetActive(true);
            Mob mob = clone.GetComponent<Mob>();
            clone.transform.position = spawnPoints[random.Next(spawnPoints.Count)];
            mob.SetActive(mobModels[random.Next(mobModels.Count)]);
        }
            
    }

    public void Kill(GameObject clone)
    {
        clone.SetActive(false);
        mobPool.Enqueue(clone);
    }
}

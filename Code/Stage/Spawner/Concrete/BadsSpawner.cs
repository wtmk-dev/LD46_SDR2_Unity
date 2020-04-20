using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BadsSpawner : MonoBehaviour
{
    [SerializeField]
    private GameObject badsPrefab;
    private Queue<GameObject> badsPool;
    [SerializeField]
    private BadsModel model;
    private BadsSpawner spawner;
    [SerializeField]
    private List<GameObject> spawnPoints;
    public void Allocate(int amount)
    {
        spawner = GetComponent<BadsSpawner>();
        badsPool = new Queue<GameObject>();
        for (int count = 0; count < amount; count++)
        {
            GameObject clone = Instantiate(badsPrefab);
            badsPool.Enqueue(clone);
            Bads bad = clone.GetComponent<Bads>();
            bad.Init(model,spawner);
            clone.transform.SetParent(gameObject.transform);
            clone.SetActive(false);
        }
    }

    public void Spawn(int a)
    {
        for(int i = 0; i < a; i++)
        {
            GameObject clone = badsPool.Dequeue();
            clone.transform.position = spawnPoints[i].transform.position;
            clone.SetActive(true);
            Bads bad = clone.GetComponent<Bads>();
            bad.SetActive();
        }
    }

    public void Kill(GameObject clone)
    {
        Rigidbody2D cloneRB = clone.GetComponent<Rigidbody2D>();
        cloneRB.velocity = Vector2.zero;
        clone.SetActive(false);
        badsPool.Enqueue(clone);
    }
}

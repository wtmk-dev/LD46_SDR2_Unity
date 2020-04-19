using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BadsSpawner : MonoBehaviour, ISpawner
{
    [SerializeField]
    private GameObject badsPrefab;
    private Queue<GameObject> badsPool;
    
    public void Allocate(int amount)
    {
        badsPool = new Queue<GameObject>();
        for (int count = 0; count < amount; count++)
        {
            GameObject clone = Instantiate(badsPrefab);
            badsPool.Enqueue(clone);
            Bads bad = clone.GetComponent<Bads>();
            bad.Init();
            clone.transform.SetParent(gameObject.transform);
            clone.SetActive(false);
        }
    }

    public void Spawn()
    {
        GameObject clone = badsPool.Dequeue();
        clone.SetActive(true);
        Bads bad = clone.GetComponent<Bads>();
        bad.SetActive();
    }
}

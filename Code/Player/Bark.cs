using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class Bark : ScriptableObject
{
    public List<string> barks;

    public string GetRandomBark()
    {
        return barks[UnityEngine.Random.Range(0, barks.Count)];
    }
}

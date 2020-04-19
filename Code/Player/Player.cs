using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class Player : ScriptableObject
{
    public GameObject Prefab;
    public int Score;
    public int Level;

    public float Despair;
    public float Hope;

    public Vector2 startingPos;

    
    public void InitNewGame()
    {
        Score = 0;
        Despair = 0;
        Hope = 27;
        Level = 1;
        startingPos = new Vector2(0.5f, -3.2f);
    }

    public float GetSpawnTime()
    {
        switch(Level)
        {
            case 1:
                return 10f;
            case 2:
                return 7f;
            case 3:
                return 3f;
            default:
                return 10f;
        }
    }

    public float GetBadsSpawnTimer()
    {
        if(Despair > 0)
        {
            return 2.1f;
        } else { return 5f; }
    }
}

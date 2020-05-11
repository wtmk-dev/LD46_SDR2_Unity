using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class Player : ScriptableObject
{
    public GameObject Prefab;
    public int Score;
    private int Level;
    private int Exp;

    public float Despair;
    public float Hope;

    public bool isWinner = false;

    [HideInInspector]
    public SpellModel spell;

    public void InitNewGame()
    {
        Score = 0;
        Despair = 0;
        Hope = 27;
        Level = 1;
        Exp = 0;
        isWinner = false;
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

    public float GetFade()
    {
        return Hope / 1000;
    }

    public void ManaTap()
    {
        Score = Score / 2;
        Hope = Hope + 27 * Level;
    }

    public int Cost()
    {
        return 9000 * Level;
    }

    public int GetLevel()
    {
        return Level;
    }

    public void LevelUp()
    {
        Exp++;

        if(Exp == 2)
        {
            Exp = 0;
            Level++;
        }
    }
}

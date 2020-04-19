using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ISpawner
{
    void Allocate(int amount);
    void Spawn();
}

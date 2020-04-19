using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreMob : MonoBehaviour
{
    private void Update()
    {
        if(gameObject.transform.position.y <= -3.88f)
        {
            gameObject.SetActive(false);
        }
    }
}

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class BadsModel : ScriptableObject
{
    public Action<bool> OnScored;

    public void Score()
    {
        if(OnScored != null)
        {
            OnScored(true);
        }
    }
}

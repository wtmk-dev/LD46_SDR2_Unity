using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shake : MonoBehaviour
{
    private Animator cameraAnimator;

    public void Init(Animator cameraAnimator)
    {
        this.cameraAnimator = cameraAnimator;
    }

    public void CameraShake()
    {
        cameraAnimator.SetTrigger("Shake");
    }

    public void ShakeUp()
    {
        cameraAnimator.SetTrigger("ShakeUp");
    }
}

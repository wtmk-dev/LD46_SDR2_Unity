using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioSyncer
{
    public float bias;
    public float timeStep;
    public float timeToBeat;
    public float restSmoothTime;

    private float previousAudioValue;
    private float currentAudioValue;
    private float timer;

    protected bool isBeat;

    public virtual void OnUpdate()
    {
        previousAudioValue = currentAudioValue;
        currentAudioValue = AudioSpectrum.spectrumValue;

        if(previousAudioValue > bias && currentAudioValue <= bias)
        {
            if(timer > timeStep)
            {
                OnBeat();
            }
        }

        if(previousAudioValue <= bias && currentAudioValue > bias)
        {
            if(timer > timeStep)
            {
                OnBeat();
            }
        }

        timer += Time.deltaTime;
    }

    public virtual void OnBeat()
    {
        Debug.Log("beat");
        timer = 0;
        isBeat = true;
    }
}

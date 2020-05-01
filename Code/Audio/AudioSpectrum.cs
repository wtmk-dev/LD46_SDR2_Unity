using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioSpectrum
{
    public static float spectrumValue { get; private set; } // change to action in the future
    private float[] audioSpectrum;

    public AudioSpectrum(int spectrumSize)
    {
        audioSpectrum = new float[spectrumSize];
    }

    public void AudioUpdate()
    {
        AudioListener.GetSpectrumData(audioSpectrum, 0, FFTWindow.Hamming);

        if(audioSpectrum != null && audioSpectrum.Length > 0)
        {
            spectrumValue = audioSpectrum[0] * 100;
        }
    }

}

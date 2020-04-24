using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager
{
    private List<AudioClip> tracks;
    private State startState;
    private AudioSource audioSource;

    public SoundManager(List<AudioClip> tracks, AudioSource audioSource, State startState) 
    {
        this.tracks = tracks; this.startState = startState; this.audioSource = audioSource;

        Debug.Log("Hey");
    }

    public void Play(int id)
    {
        Debug.Log(id);
        //audioSource.clip = tracks[id];
        //audioSource.loop = true;
        //audioSource.Play();

    }
}

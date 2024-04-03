using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloatingObject : MonoBehaviour
{
    private AudioSource source;
    [SerializeField] private AudioSource secondSource;

    private void Start()
    {
        source = GetComponent<AudioSource>();
    }

    private void Play()
    {
        source.Play();
        source.mute = false;
    }

    private void Stop()
    {
        source.Stop();
        source.mute = true;
    }

    private void PlaySecondSource()
    {
        secondSource.Play();
    }
}

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeVolumeAfterPlaying : MonoBehaviour
{
    [SerializeField] private AudioSource _source;
    [SerializeField] private AudioSource _sourceChangeVolume;
    [SerializeField] private float volumeAfterPlaying = 0.1f;
    private bool hasPlayed;

    private void Update()
    {
        if (!hasPlayed && _source.isPlaying) hasPlayed = true;
        if (hasPlayed && !_source.isPlaying)
        {
            _sourceChangeVolume.volume = volumeAfterPlaying;
            this.enabled = false;
        }
    }
}

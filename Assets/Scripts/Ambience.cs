using System;
using System.Collections;
using System.Collections.Generic;
using DigitalRuby.RainMaker;
using UnityEngine;

public class Ambience : MonoBehaviour
{
    [SerializeField] private AudioSource _source;
    [SerializeField] private GameObject _rainPrefab;
    [SerializeField] private float insideSoundVolume = 0.05f;
    [SerializeField] private AudioSource thunder;
    private float targetVolume;

    private float initialSoundVolume;
    private AudioSource _rain;
    private RainScript rain;
    

    private void Start()
    {
        rain = _rainPrefab.GetComponent<RainScript>();
        _rain = _rainPrefab.GetComponents<AudioSource>()[2];
        initialSoundVolume = _source.volume;
        targetVolume = initialSoundVolume;
    }

    public void LowerSoundAfterSeconds(float seconds)
    {
        StartCoroutine(lowerSoundAfterSec(seconds));
    }

    public void SetTargetVolume(float target)
    {
        targetVolume = target;
    }

    public void ResetSoundLevel()
    {
        targetVolume = initialSoundVolume;
    }

    private void Update()
    {
        
        _source.volume = Mathf.Lerp(_source.volume, targetVolume, Time.deltaTime);
        _rain.volume = Mathf.Lerp(_rain.volume, targetVolume, Time.deltaTime);
        if (_source.volume < 0.01f && targetVolume < 0.01f)
        {
            _source.volume = 0;
            rain.RainIntensity = 0;
        } else rain.RainIntensity = 1;

        thunder.volume = Mathf.Min(0.1f, _source.volume);
    }

    private IEnumerator lowerSoundAfterSec(float seconds)
    {
        yield return new WaitForSeconds(seconds);
        targetVolume = insideSoundVolume;
    }
}

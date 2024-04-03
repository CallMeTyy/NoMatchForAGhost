using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class LightSwitch : MonoBehaviour
{
    private bool hasTurnedOn = true;
    [SerializeField] private Transform flipSwitch;
    private Light[] lights;

    [SerializeField] private AudioClip switchOn;
    [SerializeField] private AudioClip switchOff;

    private AudioSource _source;

    private void Start()
    {
        lights = transform.parent.gameObject.GetComponentsInChildren<Light>(true);
        _source = GetComponent<AudioSource>();
    }

    public void ToggleLights()
    {
        hasTurnedOn = !hasTurnedOn;
        _source.clip = hasTurnedOn ? switchOn : switchOff;
        _source.Play();
        flipSwitch.localRotation = Quaternion.Euler(hasTurnedOn ? 5 : -5, 0,0);
        foreach (var l in lights)
        {
            l.enabled = hasTurnedOn;
        }
    }
}

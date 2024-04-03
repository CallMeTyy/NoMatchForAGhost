using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class Phone : MonoBehaviour
{
    private bool hasPlayedAudio;
    [SerializeField] private AudioSource _source;
    [SerializeField] private AudioSource _ring;

    [SerializeField] private AudioClip intro;
    [SerializeField] private AudioClip good;
    [SerializeField] private AudioClip bad;
    [SerializeField] private AudioClip none;

    private bool isRinging;

    [SerializeField] private GameObject screenText;
    [SerializeField] private GameObject screenTextJob;

    private void Start()
    {
        StartCoroutine(startAfterSeconds(10));
    }

    private IEnumerator startAfterSeconds(float seconds)
    {
        yield return new WaitForSeconds(seconds);
        _ring.Play();
        isRinging = true;
    }

    public void SwitchSceneIfPlayed(string scene)
    {
        if (hasPlayedAudio && _source.clip == intro) SceneTransitioner.singleton.SwitchScene(scene, 2);
    }

    public void StopGameIfEnding()
    {
        if (EndingStats.singleton != null && hasPlayedAudio)
        {
            print("Quitting game");
            Application.Quit();
        }
    }

    private IEnumerator enableScreenAfterSeconds(float seconds)
    {
        yield return new WaitForSeconds(seconds);
        screenTextJob.SetActive(true);
    }

    public void PlayAudioFile(SelectEnterEventArgs args)
    {
        if (hasPlayedAudio) return;
        if (!(args.interactorObject is VRHand)) return;
        if (!isRinging) return;

        _ring.Stop();


        if (EndingStats.singleton != null)
        {
            print(EndingStats.singleton.end);
            screenText.SetActive(true);
        }
        else StartCoroutine(enableScreenAfterSeconds(5));
        
        if (EndingStats.singleton == null) _source.clip = intro;
        else if (EndingStats.singleton.end == EndingStats.Ending.GOOD) _source.clip = good;
        else if (EndingStats.singleton.end == EndingStats.Ending.BAD) _source.clip = bad;
        else if (EndingStats.singleton.end == EndingStats.Ending.NONE) _source.clip = none;

        _source.Play();
        hasPlayedAudio = true;
    }
}

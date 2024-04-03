using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Thunder : MonoBehaviour
{
    [SerializeField] private float minTimeBeforeThunder = 1f;
    [SerializeField] private float maxTimeBeforeThunder = 10f;
    [SerializeField] private int maxFlashes = 3;
    [SerializeField] private float maxFlashDelay = 0.5f;
    [SerializeField] private float maxFlashTime = 0.1f;
    [SerializeField] private List<AudioClip> _audioClips;

    private bool isFlashing = false;
    private int flashCount;
    private int currentFlashCount;

    private float timer;
    private float timeBeforeAction;

    private Light _light;

    private List<AudioSource> availableSources;
    private AudioSource[] sources;

    // Start is called before the first frame update
    void Start()
    {
        availableSources = new List<AudioSource>();
        timeBeforeAction = Random.Range(minTimeBeforeThunder, maxTimeBeforeThunder);
        _light = GetComponent<Light>();
        sources = GetComponents<AudioSource>();
        for (int i = 0; i < sources.Length; i++)
        {
            sources[i].clip = _audioClips[Random.Range(0, _audioClips.Count)];
            sources[i].spatialBlend = 1;
            sources[i].rolloffMode = AudioRolloffMode.Linear;
            availableSources.Add(sources[i]);
        }
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
        if (timer > timeBeforeAction)
        {
            if (isFlashing)
            {
                if (flashCount < currentFlashCount)
                {
                    flashCount++;
                    timer = 0;
                    timeBeforeAction = Random.Range(0.1f, maxFlashDelay);
                    float flashTime = Random.Range(0.03f, Mathf.Min(maxFlashTime, timeBeforeAction - 0.01f));
                    StartCoroutine(thunderFlash(flashTime));
                }
                else
                {
                    isFlashing = false;
                    timeBeforeAction = Random.Range(minTimeBeforeThunder, maxTimeBeforeThunder);
                    timer = 0;
                }
            }
            else
            {
                isFlashing = true;
                flashCount = 0;
                currentFlashCount = Random.Range(1, maxFlashes + 1);
                timeBeforeAction = Random.Range(0.05f, maxFlashDelay);
            }
        }
    }

    private IEnumerator thunderFlash(float seconds)
    {
        _light.intensity = Random.Range(0.2f, 0.7f);
        PlayThunderSound();
        yield return new WaitForSeconds(seconds);
        _light.intensity = 0;
    }

    private void PlayThunderSound()
    {
        int index = Random.Range(0, availableSources.Count);
        availableSources[index].Play();
        StartCoroutine(AddSourceBackAfterPlaying(availableSources[index]));
        availableSources.Remove(availableSources[index]);
    }

    private IEnumerator AddSourceBackAfterPlaying(AudioSource source)
    {
        yield return new WaitForSeconds(2f);
        availableSources.Add(source);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(Volume))]
public class SceneTransitioner : MonoBehaviour
{
    public static SceneTransitioner singleton;
    private LiftGammaGain _lgg;
    
    
    private float timer;
    private float timeBeforeLoad;
    private bool isLoading;
    private bool isStarting;

    [SerializeField] private AudioSource car;

    // Start is called before the first frame update
    void Awake()
    {
        singleton = this;
        Volume volume = GetComponent<Volume>();
        if (volume.profile.TryGet(out LiftGammaGain lgg))
        {
            _lgg = lgg;
        }

        isStarting = true;
        _lgg.gain.Override(new Vector4(1f,1f,1f,0));
        StartCoroutine(resetStartAfterSeconds(1));
        timeBeforeLoad = 1;
        
    }

    public void SwitchScene(string sceneName)
    {
        car.Play();
        isLoading = true;
        timer = 0;
        timeBeforeLoad = 1;
        StartCoroutine(loadSceneAfterSeconds(sceneName, 1));
    }

    public void SwitchScene(string sceneName, float transitionTime = 1)
    {
        print("Switching scene to " + sceneName);
        car.Play();
        isLoading = true;
        timer = 0;
        timeBeforeLoad = transitionTime;
        StartCoroutine(loadSceneAfterSeconds(sceneName, transitionTime));
    }

    private IEnumerator loadSceneAfterSeconds(string sceneName, float seconds)
    {
        yield return new WaitForSeconds(seconds);
        SceneManager.LoadScene(sceneName);
    }
    
    private IEnumerator resetStartAfterSeconds(float seconds)
    {
        yield return new WaitForSeconds(seconds);
        isStarting = false;
    }

// Update is called once per frame
    void Update()
    {
        if (isLoading)
        {
            timer += Time.deltaTime;
            float map = -CoolMath.map(timer, 0, timeBeforeLoad, 0, 1);
            _lgg.gain.Override(new Vector4(1f,1f,1f,map));
        }

        if (isStarting)
        {
            timer += Time.deltaTime;
            float map = CoolMath.map(timer, 0, timeBeforeLoad, -1, 0);
            _lgg.gain.Override(new Vector4(1f,1f,1f,map));
        }
    }
}

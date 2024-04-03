using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;

public class FirePositionHandler : MonoBehaviour
{
    [SerializeField] private VisualEffect Fire;
    [SerializeField] private Transform targetPosition;
    [SerializeField] private Light light;
    [SerializeField] private float lightMin;
    [SerializeField] private float lightMax;
    [SerializeField] private float Speed = 1;
    [SerializeField] private float randomAmount = 0;
    [SerializeField] private bool setTargetPosition = true;
    [SerializeField] private bool slowTurnOn;
    [SerializeField] private AudioSource fireSound;
    [SerializeField] private bool setIntensityToMaxAtFirst = false;
    private float oldSpeed;
    private bool isFlickering;
    private float timer;
    private float oldIntensity;
    private float targetIntensity;
    private bool turningOff;
    private bool turnedOn;
    private float oldMax;
    private float partAmt = 1;
    private float startFireVolume;

    // Start is called before the first frame update
    void Start()
    {
        if (slowTurnOn) 
        {
            partAmt = 0;
            Fire.SetFloat("SpawnRate", 0);
        }
        timer = 0;
        if (!setIntensityToMaxAtFirst)
        {
            oldIntensity = 2;
            targetIntensity = 5; 
        }
        else
        {
            oldIntensity = light.intensity;
            targetIntensity = lightMax;
        }
        
        oldSpeed = Speed;
        if (fireSound != null) startFireVolume = fireSound.volume;
    }

    // Update is called once per frame
    void Update()
    {
        if (setTargetPosition)
        {
            Vector3 direction = transform.InverseTransformPoint(transform.position + Vector3.up);
            Fire.SetVector3("TargetPosition", direction);
        }

        if (slowTurnOn && !turnedOn)
        {
            partAmt = Mathf.Lerp(partAmt, 1, Time.deltaTime/2);
            Fire.SetFloat("SpawnRate", partAmt);
            if (partAmt >= 1)
            {
                partAmt = 1;
                turnedOn = true;
            }
        }

        if (turningOff)
        {
            lightMin = Mathf.Lerp(lightMin, 0, Time.deltaTime * 5);
            lightMax = Mathf.Lerp(lightMax, 0, Time.deltaTime * 5);
            float map = CoolMath.map(light.intensity, 0, oldMax, 0, 1);
            Fire.SetFloat("SpawnRate", map);
            if (fireSound != null) fireSound.volume = startFireVolume * map;
        }
        Flicker();
    }

    public void TurnOn()
    {
        
    }

    public void TurnOff()
    {
        turningOff = true;
        oldMax = lightMax;
    }
    
    void Flicker()
    {
        if (timer > Speed)
        {
            oldIntensity = light.intensity;
            targetIntensity = Random.Range(lightMin, lightMax);
            timer = 0;
            Speed = Random.Range(oldSpeed - randomAmount, oldSpeed + randomAmount);

        }
        timer += Time.deltaTime;
        light.intensity = CoolMath.map(timer, 0, Speed, oldIntensity, targetIntensity) * partAmt;
        
    }
   
}

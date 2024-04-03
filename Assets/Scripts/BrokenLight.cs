using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BrokenLight : MonoBehaviour
{
    private Light[] lights;
    [SerializeField] private float maxTimeForStayingOn = 1f;
    [SerializeField] private float maxTimeForStayingOff = 0.3f;


    private float timeBeforeAction;
    private float timer;
    private bool isOn = true;

    // Start is called before the first frame update
    void Start()
    {
        lights = GetComponentsInChildren<Light>();
        timeBeforeAction = Random.Range(0.1f, maxTimeForStayingOn);
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
        if (isOn)
        {
            if (timer > timeBeforeAction)
            {
                timeBeforeAction = Random.Range(0.1f, maxTimeForStayingOff);
                timer = 0;
                TurnLights(false);
            }
        }
        else
        {
            if (timer > timeBeforeAction)
            {
                timeBeforeAction = Random.Range(0.1f, maxTimeForStayingOn);
                timer = 0;
                TurnLights(true);
            }
        }
    }

    private void TurnLights(bool on)
    {
        foreach (var light in lights)
        {
            light.intensity = on ? light.intensity * 5 : light.intensity * 0.2f;
        }

        isOn = on;
    }
}

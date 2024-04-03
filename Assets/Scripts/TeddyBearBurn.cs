using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeddyBearBurn : MonoBehaviour
{
    [SerializeField] private Animator girl;
    [SerializeField] private Transform girlParent;
    [SerializeField] private Transform player;

    [SerializeField] private Material mat;
    [SerializeField] private Transform swing;
    [SerializeField] [ColorUsage(true,true)] private Color start;
    [SerializeField] [ColorUsage(true,true)]private Color end;
    [SerializeField] private float minDistanceBeforeChange;

    private float targetLerp;
    private float colorLerp;
    
    private bool isBurning;

    private void Start()
    {
        mat.SetColor("_FresnelColorFar", start);
        targetLerp = 0;
        colorLerp = 0;
    }

    public void BurnGirl()
    {
        isBurning = true;
        girl.SetTrigger("Bad");
        girlParent.transform.position = transform.position - girl.transform.localPosition;
    }
    
    private void Update()
    {
        if (isBurning)
        {
            girlParent.transform.LookAt(new Vector3(player.position.x, girlParent.position.y, player.position.z));
            girlParent.transform.position = transform.position;
        }

        float distance = Mathf.Min(minDistanceBeforeChange, Vector3.Distance(transform.position, swing.position));
        targetLerp = 1-CoolMath.map(distance, 0, minDistanceBeforeChange, 0, 1);
        colorLerp = Mathf.Lerp(colorLerp, targetLerp, Time.deltaTime);
        Color lerp = Color.Lerp(start, end, colorLerp);
        mat.SetColor("_FresnelColorFar", lerp);
    }
    
    
}

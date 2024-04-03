using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResetOrigin : MonoBehaviour
{
    private VRInput resetOrigin = null;
    
    private bool keptPressing = false;

    private void Start()
    {
        if (InputList.singleton.TryGetInput("ResetOrigin", out VRInput rInput))
        {
            resetOrigin = rInput;
        }
    }

    private void Update()
    {
        bool pressing = resetOrigin.GetIsPressed();
        if (pressing && !keptPressing) StartCoroutine(CheckIfStillPressedAfterSeconds(2));
        else if (keptPressing && !pressing) keptPressing = false;
    }

    private IEnumerator CheckIfStillPressedAfterSeconds(float seconds)
    {
        keptPressing = true;
        yield return new WaitForSeconds(seconds);
        if (keptPressing)
        {
            Transform head = InputList.singleton.head;
            transform.localPosition = new Vector3(-head.localPosition.x, 0, -head.localPosition.z);
            //head.localPosition = new Vector3(0, head.localPosition.y, 0);
        }
    }
}

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class DoorHandle : XRGrabInteractable
{
    [SerializeField] private Rigidbody door;
    
    protected override void OnDeactivated(DeactivateEventArgs args)
    {
        base.OnDeactivated(args);
        transform.localPosition = new Vector3(0, 0, 0);
        transform.localRotation = Quaternion.identity;
        door.velocity *= 0.5f;
        enabled = true;
    }

    private void Update()
    {
        if (Vector3.Distance(door.transform.position, transform.position) > 0.5f)
        {
           
        }
    }
}

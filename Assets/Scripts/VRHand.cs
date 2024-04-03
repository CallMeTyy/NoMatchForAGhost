using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.XR.Interaction.Toolkit;

public class VRHand : XRDirectInteractor
{
    public bool isHoldingSomething;

    public enum HandTypes
    {
        LEFT,
        RIGHT
    };

    public HandTypes hand;
    public Collider pusher;


    protected override void OnSelectEntering(SelectEnterEventArgs args)
    {
        base.OnSelectEntering(args);
        isHoldingSomething = true;
        pusher.enabled = false;
    }

    protected override void OnSelectExiting(SelectExitEventArgs args)
    {
        base.OnSelectExiting(args);
        isHoldingSomething = false;
        pusher.enabled = true;
    }
}

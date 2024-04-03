using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandAnimator : MonoBehaviour
{
    [SerializeField] private bool isLeftHand;

    private Animator _animator;

    private VRInput trigger, thumb, grip;
    private int TRIGGER, THUMB, GRIP, STRENGTH, PINCH;

    private float thumbValue;
    private float pinchValue;
    private float holdValue;
    private float gripStrength = 1;

    private bool isPinching = false;

    private VRHand vrhand;
    private void Start()
    {
        vrhand = GetComponentInParent<VRHand>();
        _animator = GetComponent<Animator>();
        
        string hand = isLeftHand ? "Left" : "Right";
        if (InputList.singleton.TryGetInput("Trigger"+hand, out VRInput tr)) trigger = tr;
        if (InputList.singleton.TryGetInput("Thumb"+hand, out VRInput th)) thumb = th;
        if (InputList.singleton.TryGetInput("Grip"+hand, out VRInput g)) grip = g;

        TRIGGER = Animator.StringToHash("Index");
        THUMB = Animator.StringToHash("Thumb");
        GRIP = Animator.StringToHash("Grip");
        STRENGTH = Animator.StringToHash("GrabStrength");
        PINCH = Animator.StringToHash("Pinch");
    }

    public void SetGripStrength(float strength, bool pinch = false)
    {
        gripStrength = strength;
        isPinching = pinch;
    }

    private void Update()
    {
        _animator.SetFloat(TRIGGER, trigger.GetFloatValue());
        thumbValue = Mathf.Lerp(thumbValue, thumb.GetIsPressed() ? 1 : 0, Time.deltaTime * 10);
        _animator.SetFloat(THUMB, thumbValue);
        _animator.SetFloat(GRIP, grip.GetFloatValue());
        if (!isPinching)
        {
            holdValue = Mathf.Lerp(holdValue, vrhand.isHoldingSomething ? 1 : 0, Time.deltaTime * 10);
            _animator.SetLayerWeight(4,holdValue);
            _animator.SetFloat(STRENGTH, gripStrength);
        }
        else
        {
            pinchValue = Mathf.Lerp(pinchValue, vrhand.isHoldingSomething ? gripStrength : 0, Time.deltaTime * 10);
            _animator.SetFloat(TRIGGER, pinchValue);
            _animator.SetFloat(THUMB, pinchValue/2);
            _animator.SetFloat(GRIP, grip.GetFloatValue());
        }
        
    }
}

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class SimplePickup : XRGrabInteractable
{
    [Header("Pickup Settings")] 
    [SerializeField] private bool pinch = false;
    [SerializeField] private float gripSize = 1;
    [SerializeField] private Transform leftHandOffset;
    [SerializeField] private Transform rightHandOffset;
    readonly InteractionLayerMask leftMask = 2;
    readonly InteractionLayerMask rightMask = 4;
    readonly InteractionLayerMask defaultMask = 1;

    [Header("Sounds")] 
    [SerializeField] private AudioClip dropSound;
    [SerializeField] private AudioClip pickupSound;
    [SerializeField] private AudioClip collideSound;

    private AudioSource _source;

    private bool playSoundFirstTime;

    private VRHand interactor;

    private Rigidbody rbody;

    private void Start()
    {
        rbody = GetComponent<Rigidbody>();
        if ((dropSound != null || pickupSound != null || collideSound != null))
        {
            if (TryGetComponent(out AudioSource source))
            {
                _source = source;
            }
            else
            {
                _source = gameObject.AddComponent<AudioSource>();
            }

            _source.playOnAwake = false;
            _source.spatialBlend = 1;
        }
    }

    public void ThrowAtPlayer()
    {
        Transform player = GameObject.FindWithTag("Player")?.transform;
        if (player != null)
        {
            rbody.AddForce((player.position + Vector3.up * 2) - transform.position * 30);
        }
    }

    public bool GetInteractor(out VRHand hand)
    {
        hand = interactor;
        return hand != null;
    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("Player")) return;

        if (collideSound != null && playSoundFirstTime)
        {
            _source.clip = collideSound;
            _source.Play();
        }
        playSoundFirstTime = true;
    }

    protected override void OnSelectEntered(SelectEnterEventArgs args)
    {
        base.OnSelectEntered(args);
        if (args.interactorObject is VRHand controller)
        {
            interactor = controller;
            int layerPickedUp = LayerMask.NameToLayer("InteractablePickedUp");
            gameObject.layer = layerPickedUp;
            if (controller.hand == VRHand.HandTypes.RIGHT)
            {
                if (rightHandOffset != null) attachTransform = rightHandOffset;
                interactionLayers = rightMask;
            }

            if (controller.hand == VRHand.HandTypes.LEFT)
            {
                if (leftHandOffset != null) attachTransform = leftHandOffset;
                interactionLayers = leftMask;
            }
            if (pickupSound != null)
            {
                _source.clip = pickupSound;
                _source.Play();
            }
            args.interactorObject.transform.gameObject.GetComponentInChildren<HandAnimator>().SetGripStrength(gripSize, pinch); 
        }
    }

    protected override void OnSelectExited(SelectExitEventArgs args)
    {
        base.OnSelectExited(args);
        interactor = null;
        attachTransform = null;
        int layerPickUp = LayerMask.NameToLayer("InteractablePickup");
        gameObject.layer = layerPickUp;
        interactionLayers = defaultMask;
        if (dropSound != null)
        {
            _source.clip = dropSound;
            _source.Play();
        }
    }
}

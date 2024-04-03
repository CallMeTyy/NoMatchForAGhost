using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.XR;
using UnityEngine.XR.Interaction.Toolkit;

public class MatchOpen : MonoBehaviour
{
    [SerializeField] private Transform tray;
    [SerializeField] private float swingSensitivity = 1f;
    [SerializeField] private int startMatchCount = 7;
    private InputDevice device;
    private bool isPickedUp;
    private bool isOpen;
    private bool canClose;
    private bool canOpen = true;

    [SerializeField] private TextMeshProUGUI text;
    [SerializeField] private XRGrabInteractable matchGrabber;
    [SerializeField] private Collider matchDropper;
    [SerializeField] private GameObject matchPrefab;
    private int matchCount;

    [SerializeField] private AudioClip openBox, closeBox;
    [SerializeField] private AudioSource boxSource;

    private void Start()
    {
        matchGrabber.enabled = false;
        matchDropper.enabled = false;
        matchCount = startMatchCount;
        text.text = matchCount.ToString();
    }

    public void AddMatch()
    {
        matchCount++;
        text.text = matchCount.ToString();
    }

    public void GrabMatch()
    {
        if (matchCount <= 0 || !isOpen) return;
        matchDropper.enabled = false;
        matchGrabber.enabled = false;
        GameObject match = Instantiate(matchPrefab, matchGrabber.transform.position, matchGrabber.transform.rotation);
        StartCoroutine(reactivateGrabMatchAfterClosing(0.5f));
        matchCount--;
        text.text = matchCount.ToString();
    }

    private IEnumerator reactivateGrabMatchAfterClosing(float seconds)
    {
        yield return new WaitForSeconds(seconds);
        if (isOpen) matchGrabber.enabled = true;
    }

    public void OnPickup(SelectEnterEventArgs args)
    {
        if (args.interactorObject is VRHand hand)
        {
            if (hand.hand == VRHand.HandTypes.LEFT) device = InputDevices.GetDeviceAtXRNode(XRNode.LeftHand);
            if (hand.hand == VRHand.HandTypes.RIGHT) device = InputDevices.GetDeviceAtXRNode(XRNode.RightHand);

            isPickedUp = true;
        }
    }

    public void OnDrop(SelectExitEventArgs args)
    {
        if (args.interactorObject is VRHand hand)
        {
            isOpen = false;
            isPickedUp = false;
            tray.localPosition = new Vector3(0, 0, 0);
            canOpen = true;
            canClose = false;
            matchGrabber.enabled = false;
            print("Dropped");
        }
    }

    private void Update()
    {
        if (isPickedUp)
        {
            device.TryGetFeatureValue(CommonUsages.deviceVelocity, out Vector3 velocity);
            Vector3 projection = Vector3.Project(velocity, transform.forward);
            float dot = Vector3.Dot(velocity, transform.forward);
            float forwardDot = Vector3.Dot(transform.forward, Vector3.up);
            bool correctSwing = Mathf.Sign(-dot) == Mathf.Sign(forwardDot);
            if (projection.magnitude > swingSensitivity)
            {
                if (correctSwing && !isOpen && canOpen)
                {
                    tray.localPosition = new Vector3(0, 0, Mathf.Sign(forwardDot) * -0.08f);
                    isOpen = true;
                    StartCoroutine(canCloseAfterSeconds(0.5f));
                    matchGrabber.enabled = true;
                    matchDropper.enabled = true;
                    if (openBox != null)
                    {
                        boxSource.clip = openBox;
                        boxSource.Play();
                    }
                    
                }

                if (!correctSwing && isOpen && canClose)
                {
                    tray.localPosition = new Vector3(0, 0, 0);
                    isOpen = false;
                    canOpen = false;
                    matchGrabber.enabled = false;
                    matchDropper.enabled = false;
                    if (closeBox != null)
                    {
                        boxSource.clip = closeBox;
                        boxSource.Play();
                    }
                }
            }
        }

        if (!canOpen)
        {
            StartCoroutine(canOpenAfterSeconds(0.5f));
        }
    }

    private IEnumerator canCloseAfterSeconds(float seconds)
    {
        canClose = false;
        yield return new WaitForSeconds(seconds);
        canClose = true;
    }
    
    private IEnumerator canOpenAfterSeconds(float seconds)
    {
        yield return new WaitForSeconds(seconds);
        canOpen = true;
    }
}

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;
using UnityEngine.XR.Interaction.Toolkit;

public class Match : MonoBehaviour
{
    private XRGrabInteractable _grabInteractable = null;
    private XRDirectInteractor _interactor = null;
    private InputDevice controller;
    public bool lit;
    private Vector3 fireOffset;

    [SerializeField] private GameObject fire;
    [SerializeField] private GameObject tip;
    [SerializeField] private GameObject burnedTip;

    [SerializeField] private Transform stick;
    [SerializeField] private Transform blackstick;
    [SerializeField] private Transform firePoint;

    [SerializeField] private float breakSpeed = 0.05f;
    
    private void Start()
    {
        
        _grabInteractable = GetComponent<XRGrabInteractable>();
        _grabInteractable.movementType = XRBaseInteractable.MovementType.VelocityTracking;
        _grabInteractable.selectEntered.AddListener(OnSelectEntered);
        _grabInteractable.selectExited.AddListener(OnSelectExit);

        fireOffset = fire.transform.localPosition;
    }

    private void OnSelectEntered(SelectEnterEventArgs args)
    {
        if (args.interactorObject is XRDirectInteractor directInteractor)
        {
            _interactor = directInteractor;

            if (directInteractor.name.Contains("Left")) controller = InputDevices.GetDeviceAtXRNode(XRNode.LeftHand);
            else controller = InputDevices.GetDeviceAtXRNode(XRNode.RightHand);
        }
    }

    private void Update()
    {
        if (lit)
        {
            Vector3 s = stick.localScale;
            Vector3 bs = blackstick.localScale;
            if (s.y < 0.25f) _grabInteractable.enabled = false;
            if (s.y <= 0.1f)
            {
                fire.SetActive(false);
                lit = false;
            }
            stick.localScale = Vector3.Lerp(s, new Vector3(s.x, 0f, s.z), Time.deltaTime * breakSpeed); 
            blackstick.localScale = Vector3.Lerp(bs, new Vector3(bs.x, 1f, bs.z), Time.deltaTime * breakSpeed);
            fire.transform.position = firePoint.position;
        }
    }

    private void OnSelectExit(SelectExitEventArgs args)
    {
        _interactor = null;
    }


    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("MatchBox") && !lit)
        {
            if (_interactor != null)
            {
                controller.TryGetFeatureValue(CommonUsages.deviceVelocity, out Vector3 velocity);
                if (velocity.magnitude > 1f)
                {
                    _interactor.xrController.SendHapticImpulse(0.7f, 0.3f);
                    lit = true;
                    fire.SetActive(true);
                    tip.SetActive(false);
                    burnedTip.SetActive(true);
                    tip.GetComponentInParent<AudioSource>().Play();
                    _grabInteractable.movementType = XRBaseInteractable.MovementType.Instantaneous;
                }
            }
        }
    }
}

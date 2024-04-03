using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class Crayon : XRGrabInteractable
{
    private DrawSurface _currentDrawSurface;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Writable"))
        {
            _currentDrawSurface = other.gameObject.GetComponent<DrawSurface>();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Writable"))
        {
            _currentDrawSurface = null;
        }
    }

    private void Update()
    {
        if (_currentDrawSurface != null)
        {
            _currentDrawSurface.Draw(transform.position);
        }
    }
}

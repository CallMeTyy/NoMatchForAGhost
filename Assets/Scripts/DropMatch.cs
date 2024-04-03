using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DropMatch : MonoBehaviour
{
    private bool isBurning;
    [SerializeField] private SimplePickup grab;
    
    
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            VRHand hand = other.GetComponentInParent<VRHand>();
            if (hand != null && grab.GetInteractor(out VRHand interactor))
            {
                if (hand.hand == interactor.hand)
                {
                    StartCoroutine(DropAfterSeconds(1));
                }
            }
        }

        if (other.gameObject.layer == LayerMask.NameToLayer("Burnable"))
        {
            other.gameObject.GetComponent<Burnable>()?.SetBurning(true);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player")) isBurning = false;
    }

    private IEnumerator DropAfterSeconds(float seconds)
    {
        isBurning = true;
        yield return new WaitForSeconds(seconds);
        if (isBurning) grab.enabled = false;
    }
}

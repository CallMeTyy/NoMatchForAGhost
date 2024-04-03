using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using Random = UnityEngine.Random;


public class GhostTrigger : MonoBehaviour
{
    [SerializeField] private UnityEvent defaultEvent = new UnityEvent();
    [SerializeField] private bool runEventsOnce = false;
    [SerializeField] private List<UnityEvent> events = new List<UnityEvent>();

    private bool isIn;

    

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && !isIn)
        {
            defaultEvent.Invoke();
            if (events.Count > 0)
            {
                int index = Random.Range(0, events.Count);
                events[index].Invoke();
                if (runEventsOnce) events.RemoveAt(index);
            }
            isIn = true;
        }
    }
    
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isIn = false;
        }
    }
}

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MatchBoxAddMatch : MonoBehaviour
{
    [SerializeField] private MatchOpen matchBox;
    
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Match"))
        {
            if (!other.gameObject.GetComponentInParent<Match>().lit)
            {
                matchBox.AddMatch();
                Destroy(other.gameObject);
            }
        }
    }
}

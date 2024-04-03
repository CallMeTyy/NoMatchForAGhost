using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathZone : MonoBehaviour
{
    [SerializeField] private Transform player;
    private Vector3 startPos;
    
    // Start is called before the first frame update
    void Start()
    {
        startPos = player.position;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.CompareTag("Player")) player.position = startPos;
    }
}

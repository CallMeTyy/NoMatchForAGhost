using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyDoor : MonoBehaviour
{
    [SerializeField] private AudioSource _openDoorSource;
    [SerializeField] private AudioSource _shutDoorSource;
    private bool openDoor;
    private bool unlockedDoor;
    private int targetRotation = 110;
    private float yRotation;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Key") && !openDoor)
        {
            openDoor = true;
            unlockedDoor = true;
            _openDoorSource.Play();
            targetRotation = 110;
            Destroy(other.transform.parent.gameObject);
        }
        
        if (other.gameObject.CompareTag("Player") && !openDoor && unlockedDoor)
        {
            openDoor = true;
            _openDoorSource.Play();
        }
    }

    public void GhostShutDoor()
    {
        _shutDoorSource.Play();
        openDoor = false;
        targetRotation = -110;
        transform.localRotation = Quaternion.Euler(0, 0, 0);
    }

    private void Update()
    {
        if (openDoor)
        {
            yRotation = Mathf.Lerp(yRotation, targetRotation, Time.deltaTime / 2);
            transform.localRotation = Quaternion.Euler(0, yRotation, 0);
        }
    }
}

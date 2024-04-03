using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    private bool hasEnteredHouse = false;

    [SerializeField] private AudioSource _source;

    [SerializeField] private AudioClip playerStepConcrete;
    [SerializeField] private AudioClip playerStepWood;

    [SerializeField] private LayerMask masksToCheck;

    [SerializeField] private Animator _animator;
    

    public void SetHouseEntered(bool hasEntered)
    {
        hasEnteredHouse = hasEntered;
    }

    private void PlayFootSound()
    {
        _source.clip = playerStepWood;
        _animator.speed = 1;
        RaycastHit hit;
        if (Physics.Raycast(transform.position, Vector3.down, out hit, 0.3f, masksToCheck))
        {
            if (hit.collider.CompareTag("Concrete")) _source.clip = playerStepConcrete;
            _source.Play();
        }
        
    }
}

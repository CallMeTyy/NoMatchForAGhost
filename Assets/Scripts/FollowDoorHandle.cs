using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class FollowDoorHandle : MonoBehaviour
{
    [SerializeField] private Transform target;
    private Rigidbody rbody;

    private void Start()
    {
        rbody = GetComponent<Rigidbody>();
    }

    void Update()
    {
        rbody.MovePosition(target.position);
    }
}

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

public class CameraCollision : MonoBehaviour
{
    private LiftGammaGain _liftGammaGain;
    [SerializeField] private Transform player;
    [SerializeField] private LayerMask collisionsToCheck;
    private bool isCollidingWithWall;

    // Start is called before the first frame update
    void Start()
    {
        Volume v = GameObject.FindObjectOfType<Volume>();
        if (v != null)
        {
            if (v.profile.TryGet(out LiftGammaGain g))
            {
                _liftGammaGain = g;
            }
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.isTrigger) return;
        if (isCollidingWithWall) return;
        Vector3 collisionPoint = other.ClosestPoint(transform.position);
        float distance = -Vector3.Distance(collisionPoint, transform.position);
        float map = -CoolMath.map(distance, -0.15f, 0, 0, 1);
        _liftGammaGain.gain.Override(new Vector4(1f,1f,1f,map));
    }

    private void FixedUpdate()
    {
        isCollidingWithWall = false;
        _liftGammaGain.gain.Override(new Vector4(1f,1f,1f,0));
        Vector3 dir = (player.position + Vector3.up * 2) - transform.position;
        float dist = Vector3.Distance(transform.position, (player.position + Vector3.up * 2));
        RaycastHit hit;
        if (Physics.Raycast(transform.position, dir, out hit, dist, collisionsToCheck, QueryTriggerInteraction.Ignore))
        {
            _liftGammaGain.gain.Override(new Vector4(1f,1f,1f,-1));
            isCollidingWithWall = true;
        }
    }
}

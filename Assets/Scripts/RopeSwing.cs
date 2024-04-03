using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RopeSwing : MonoBehaviour
{
    [SerializeField] private List<Transform> attachPoints;

    [SerializeField] private LineRenderer _rope;

    private void Start()
    {
        _rope.useWorldSpace = true;
        _rope.positionCount = attachPoints.Count;
    }

    private void FixedUpdate()
    {
        Vector3[] points = new Vector3[attachPoints.Count];
        for (int i = 0; i < points.Length; i++)
        {
            points[i] = attachPoints[i].position;
        }
        _rope.SetPositions(points);
    }
}

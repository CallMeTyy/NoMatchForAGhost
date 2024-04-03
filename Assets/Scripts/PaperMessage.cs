using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PaperMessage : MonoBehaviour
{
    [SerializeField] private Material afterBurnMaterial;
    [SerializeField] private MeshRenderer _meshRenderer;
    private Material paper;
    private bool fullyBurned = false;
    public float burnedValue;
    private bool isHoldingUnderFlame;

    private void Start()
    {
        afterBurnMaterial.color = new Color(1, 1, 1, 0);
        burnedValue = 0;
    }

    private void OnTriggerEnter(Collider other)
    {
        isHoldingUnderFlame = true;
    }

    private void OnTriggerExit(Collider other)
    {
        isHoldingUnderFlame = false;
    }

    private void Update()
    {
        if (isHoldingUnderFlame)
        {
            if (burnedValue < 1)
            {
                burnedValue += Time.deltaTime;
                burnedValue = Mathf.Min(burnedValue, 1);
            }
            
            
        }
        
        afterBurnMaterial.color = new Color(1, 1, 1, burnedValue);
    }
}

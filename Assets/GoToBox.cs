using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;

public class GoToBox : MonoBehaviour
{
    private VisualEffect effect;
    [SerializeField] private Transform boxPosition;
    void Start()
    {
        effect = this.GetComponent<VisualEffect>();
        
    }

    // Update is called once per frame
    void Update()
    {
        effect.SetVector3("BoxPosition", boxPosition.position);
    }
}

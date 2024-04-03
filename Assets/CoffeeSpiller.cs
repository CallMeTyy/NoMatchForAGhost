using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;

public class CoffeeSpiller : MonoBehaviour
{
    // Start is called before the first frame update
    private Vector3 direction;
    [SerializeField] private VisualEffect coffee;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        direction = transform.rotation * Vector3.up;
        if(direction.y < 0)
        {
            coffee.SetFloat("SpawnRate", 8);
        }
        else
        {
            coffee.SetFloat("SpawnRate", 0);
        }
    }
}

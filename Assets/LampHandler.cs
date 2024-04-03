using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LampHandler : MonoBehaviour
{
    [SerializeField] private GameObject[] lights;
    private bool turnOff;
    void Start()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            //turnOff = true;
            for (int i = 0; i < lights.Length; i++)
            {
                lights[i].SetActive(false);
            }
        }
    }
    private void Update()
    {
        if (turnOff)
        {
            for (int i = 0; i < lights.Length; i++)
            {
                lights[i].SetActive(false);
            }
        }
    }
}

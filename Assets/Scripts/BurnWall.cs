using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BurnWall : MonoBehaviour
{
    [SerializeField] private Burnable[] parts;

    public void CheckIfBurned()
    {
        int amt = 0;
        foreach (var burn in parts)
        {
            if (burn.isBurning) amt++;
        }

        if (amt > parts.Length / 2)
        {
            gameObject.SetActive(false);
        }
    }
}

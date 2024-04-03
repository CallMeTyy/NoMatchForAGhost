using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteAlways]
public class LockRotation : MonoBehaviour
{
    // Start is called before the first frame update
  
    // Update is called once per frame
    void Update()
    {
        transform.rotation = Quaternion.identity;
    }
}

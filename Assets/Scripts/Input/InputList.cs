using System;
using System.Collections.Generic;
using UnityEngine;

public class InputList : MonoBehaviour
{
    public static InputList singleton;
    
    //public Dictionary<string, VRInput> inputs = new Dictionary<string, VRInput>();
    [SerializeField] private List<VRInput> inputs;

    public Transform head;
    public Transform leftHand;
    public Transform rightHand;
    

    private void Awake()
    {
        singleton = this;
        foreach (VRInput input in inputs)
        {
            input.Initialize();
        }
    }

    public bool TryGetInput(string inputName, out VRInput input)
    {
        input = null;
        foreach (VRInput i in inputs)
        {
            if (i.name == inputName) input = i;
        }
        if (input == null) Debug.LogWarning("Specified input does not exist");
        return input != null;
    }
}

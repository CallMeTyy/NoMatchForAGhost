using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using Unity.VisualScripting.FullSerializer;
using UnityEngine;
using UnityEngine.InputSystem;

[Serializable]
public class VRInput
{
    public string name = "Input";
    public InputActionReference actionReference = null;

    private bool isPressed = false;

    private float floatValue = -1;
    private Vector2 vector2Value = Vector2.zero;

    public void Initialize()
    {
        actionReference.action.started += OnPressed;
        actionReference.action.performed += OnChangeData;
        actionReference.action.canceled += OnRelease;
    }

    #region ReadMethods

    private void OnPressed(InputAction.CallbackContext context)
    {
        isPressed = true;
    }
    
    private void OnRelease(InputAction.CallbackContext context)
    {
        isPressed = false;
        vector2Value = Vector2.zero;
    }

    private void OnChangeData(InputAction.CallbackContext context)
    {
        if (context.valueType == typeof(Vector2)) vector2Value = context.ReadValue<Vector2>();
        else if (context.valueType == typeof(float)) floatValue = context.ReadValue<float>();
    }

    #endregion
    
    
    #region GetMethods

    public float GetFloatValue()
    {
        return floatValue;
    }
    
    public Vector2 GetVector2Value()
    {
        return vector2Value;
    }

    public bool GetIsPressed()
    {
        return isPressed;
    }
    
    #endregion
    
}

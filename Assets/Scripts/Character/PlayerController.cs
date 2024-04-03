using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class PlayerController : MonoBehaviour
{
    private CharacterController controller = null;

    private VRInput moveInput = null;
    private VRInput turnInput = null;
    
    private Vector2 stickInput;
    private Vector2 turnValue;

    private Transform headTransform;

    [SerializeField] private float speed;
    [SerializeField] private float gravity;
    [SerializeField] private Animator _animator;
    
    private Vector3 velocity;

    private bool hasTurned;

    private int WALKING;
    
    void Awake()
    {
        controller = GetComponent<CharacterController>();
        WALKING = Animator.StringToHash("isWalking");
    }

    private void Start()
    {
        headTransform = InputList.singleton.head;
        if (InputList.singleton.TryGetInput("Move", out VRInput minput))
        {
            moveInput = minput;
        }
        if (InputList.singleton.TryGetInput("Turn", out VRInput tinput))
        {
            turnInput = tinput;
        }
    }
    
    void Update()
    {
        //SetCenterOfCharacter();
        stickInput = moveInput.GetVector2Value();
        _animator.SetBool(WALKING, stickInput.magnitude > 0.1f);
        
        turnValue = turnInput.GetVector2Value();

        ApplyTurn();
    }

    private void SetCenterOfCharacter()
    {
        controller.center = new Vector3(headTransform.transform.localPosition.x, 0,
            headTransform.transform.localPosition.z);
    }

    private void FixedUpdate()
    {
        ApplyCameraOrientedMovement();
        ApplyGravity();
    }

    private void ApplyTurn()
    {
        if (Mathf.Abs(turnValue.x) > 0.3f)
        {
            if (!hasTurned)
            {
                transform.Rotate(0, 45 * Mathf.Sign(turnValue.x), 0);
                hasTurned = true;
            }
        } else hasTurned = false;
    }

    private void ApplyCameraOrientedMovement()
    {
        Vector3 forward = Vector3.ProjectOnPlane(headTransform.forward, Vector3.up);
        Vector3 right = Vector3.ProjectOnPlane(headTransform.right, Vector3.up);

        velocity = forward * stickInput.y + right * stickInput.x;
        if (velocity.magnitude > 1) velocity = velocity.normalized;
        
        controller.Move(speed * Time.fixedDeltaTime * velocity);
    }

    private void ApplyGravity()
    {
        controller.Move(gravity * Time.fixedDeltaTime * Vector3.down);
    }
}

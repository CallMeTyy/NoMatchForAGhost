using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class SwingSeat : XRSocketInteractor
{
    [SerializeField] private HingeJoint joint;
    [SerializeField] private Animator ghost;

    private JointSpring oldSpringValues;
    
    protected override void OnSelectEntered(SelectEnterEventArgs args)
    {
        base.OnSelectEntered(args);
        foreach (Collider c in args.interactableObject.colliders)
        {
            c.isTrigger = true;
        }

        oldSpringValues = joint.spring;
        joint.spring = new JointSpring();

        if (args.interactableObject.transform.gameObject.CompareTag("Teddy"))
        {
            
            ghost.SetTrigger("Ascend");
        }
    }

    protected override void OnSelectExited(SelectExitEventArgs args)
    {
        base.OnSelectExited(args);
        foreach (Collider c in args.interactableObject.colliders)
        {
            c.isTrigger = false;
        }

        joint.spring = oldSpringValues;
    }
}

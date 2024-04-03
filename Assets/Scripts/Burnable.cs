using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using Random = UnityEngine.Random;

public class Burnable : MonoBehaviour
{
    [SerializeField] private GameObject fire;
    [SerializeField] private float minFireHeight = 1;
    [SerializeField] private float maxFireHeight = 1.25f;

    [SerializeField] private UnityEvent OnBurn;
    private FirePositionHandler _fire;
    [HideInInspector] public bool isBurning;
    
    void Start()
    {
        _fire = GetComponentInChildren<FirePositionHandler>(true);
        Vector3 scale = fire.transform.localScale;
        scale.y = Random.Range(minFireHeight, maxFireHeight);
        fire.transform.localScale = scale;
        //SetBurning(true);
    }

    public void DestroyAfterSeconds(float seconds)
    {
        StartCoroutine(destroyParentAfterSeconds(seconds, true));
    }
    
    public void DestroySelfSeconds(float seconds)
    {
        StartCoroutine(destroyParentAfterSeconds(seconds, false));
    }
    
    public void AddBodyAfterSeconds(float seconds)
    {
        StartCoroutine(addRigidbodyAfterSeconds(seconds));
    }
    
    public void StopFireAfterSeconds(float seconds)
    {
        StartCoroutine(deafenAfterSeconds(seconds));
    }

    private IEnumerator deafenAfterSeconds(float seconds)
    {
        yield return new WaitForSeconds(seconds);
        _fire.TurnOff();
    }

    private IEnumerator destroyParentAfterSeconds(float seconds, bool destroyParent)
    {
        yield return new WaitForSeconds(seconds);
        if (destroyParent) Destroy(transform.parent.gameObject);
        else Destroy(gameObject);
    }
    
    private IEnumerator addRigidbodyAfterSeconds(float seconds)
    {
        yield return new WaitForSeconds(seconds);
        gameObject.layer = 1;
        Rigidbody rbody = gameObject.AddComponent<Rigidbody>();
        rbody.AddTorque(-50,0,0);
    }

    public void SetBurning(bool burn)
    {
        if (isBurning && burn) return;
        isBurning = burn;
        fire.SetActive(burn);
        OnBurn.Invoke();
    }
}

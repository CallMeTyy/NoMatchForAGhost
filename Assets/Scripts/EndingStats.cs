using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndingStats : MonoBehaviour
{
    public static EndingStats singleton;

    public enum Ending
    {
        NONE,
        GOOD,
        BAD
    };

    public Ending end;

    private void Awake()
    {
        singleton = this;
        DontDestroyOnLoad(gameObject);
    }

    public void SetEnding(Ending pEnd)
    {
        end = pEnd;
    }
}

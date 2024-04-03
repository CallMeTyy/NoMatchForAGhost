using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoxCounter : MonoBehaviour
{
    private int candleCount;
    [SerializeField] private int candleAmount;
    [SerializeField] private GameObject otherBox;
    [SerializeField] private GameObject boxPosition;
    [SerializeField] private GameObject newPosition;
    
    public void AddCandle()
    {
        print("Candle Count " + candleCount);
        candleCount++;
        if (candleCount >= candleAmount)
        {
            boxPosition.transform.position = newPosition.transform.position;
            otherBox.SetActive(true);
            Destroy(gameObject);
        }
    }
}

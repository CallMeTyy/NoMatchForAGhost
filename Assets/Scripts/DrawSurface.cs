using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrawSurface : MonoBehaviour
{
    [SerializeField] private Transform topLeftT, bottomRightT;
    private Vector2 topLeft, bottomRight;

    [SerializeField] private Texture2D _texture;

    private void Start()
    {
        topLeft = topLeftT.localPosition;
        bottomRight = bottomRightT.localPosition;
    }

    public void Draw(Vector3 position)
    {
        Vector2 localPosition = transform.InverseTransformPoint(position);
        print(localPosition.y);
        int x = Mathf.FloorToInt(CoolMath.map(localPosition.x, topLeft.x, bottomRight.x, 1, 2048));
        int y = Mathf.FloorToInt(CoolMath.map(localPosition.y, topLeft.y, bottomRight.y, 1, 1024));
        x = Mathf.Min(x, 2048);
        x = Mathf.Max(x, 1);
        y = Mathf.Min(y, 1024);
        y = Mathf.Max(y, 1);
        
        print($"localY: {localPosition.y} || Y: {y}");

        /*for (int px = x - 5; px <= x + 5; px++)
        {
            for (int py = y - 5; py <= y + 5; py++)
            {
                px = Mathf.Min(px, 2048);
                px = Mathf.Max(px, 0);
                py = Mathf.Min(py, 1024);
                py = Mathf.Max(py, 0);
                _texture.SetPixel(px,py,Color.red);
            }
        }*/
        _texture.SetPixel(x,y,Color.red);
        _texture.Apply(true);
    }
}

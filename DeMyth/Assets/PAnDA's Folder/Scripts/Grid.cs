using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using UnityEngine;

public class Grid 
{
    private int width;
    private int height;
    private float cellSize;

    private int[,] ArrayGrid;

    public Grid(int width, int height, float cellSize)
    {
        this.width = width;
        this.height = height;
        this.cellSize = cellSize;

        ArrayGrid = new int[width,height];

        VisualGrid();

    }

    private void VisualGrid()
    {
        for (int x=0; x<ArrayGrid.GetLength(0); x++)
        {
            for (int y = 0; y < ArrayGrid.GetLength(1); y++)
            {
                Debug.DrawLine(GetWorldPosition(x, y), GetWorldPosition(x, y + 1),UnityEngine.Color.red, Mathf.Infinity);
                Debug.DrawLine(GetWorldPosition(x, y), GetWorldPosition(x + 1, y), UnityEngine.Color.red, Mathf.Infinity);
            }
        }
        Debug.DrawLine(GetWorldPosition(0, height), GetWorldPosition(width,height), UnityEngine.Color.red, Mathf.Infinity);
        Debug.DrawLine(GetWorldPosition(width,0), GetWorldPosition(width, height), UnityEngine.Color.red, Mathf.Infinity);
    }

    private Vector3 GetWorldPosition(int x, int y)
    {
        return new Vector3(x, y, 0) * cellSize;
    }

    public int GetWidth()
    {
        return this.width;
    }
    public int GetHeight()
    {
        return this.height;
    }

}

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public Grid grid { get; private set; }
    [SerializeField] int widthSize, heightSize;
    public float cellSize = 1f;


    private void Awake()
    {
         grid = new Grid(widthSize, heightSize, cellSize);
    }


}

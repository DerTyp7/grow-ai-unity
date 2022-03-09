using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridInfo : MonoBehaviour
{
    public static GridInfo instance;

    public int gridWidth = 20;
    public int gridHeight = 20;
    public float cellSize = 1f;

    private void Awake()
    {
        instance = this;
    }
}

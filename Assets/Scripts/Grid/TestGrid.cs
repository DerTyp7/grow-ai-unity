using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestGrid : MonoBehaviour
{
    Grid grid;
    void Start()
    {
        grid = new Grid(100, 100, 1, new Vector3(0, 0));
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            grid.SetValue(Camera.main.ScreenToWorldPoint(Input.mousePosition), 44);
        }

        if (Input.GetMouseButtonDown(1))
        {
            Debug.Log(grid.GetValue(Camera.main.ScreenToWorldPoint(Input.mousePosition)));
        }
    }
}

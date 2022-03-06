using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AreaManager : MonoBehaviour
{
    [SerializeField] int areaSize = 16;

    [SerializeField] int width = 8;
    [SerializeField] int height = 8;

    [SerializeField] GameObject area;

    [SerializeField] List<Area> areas;

    private void Start()
    {
        GenerateGrid();
    }

    void GenerateGrid()
    {
        for(int x = 0; x < width; x++)
        {
            for(int y = 0; y < height; y++)
            {
                GameObject currentArea = Instantiate(area);
                currentArea.transform.position = new Vector3(16*x, 16*y, 1);
                areas.Add(currentArea.GetComponent<Area>());
            }
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireCube(Vector3Int.zero, new Vector3Int(areaSize, areaSize, 1));
    }
}

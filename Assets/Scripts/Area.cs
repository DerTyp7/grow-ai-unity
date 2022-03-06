using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Area : MonoBehaviour
{
    [SerializeField] bool accessable = false;
    [SerializeField] GameObject tile;

    List<Tile> tiles = new List<Tile>();

    int height = 16;
    int width = 16;

    void Start()
    {
        GetComponent<SpriteRenderer>().color = Random.ColorHSV(0f, 1f, 1f, 1f, 0.5f, 1f);
        //GenerateGrid();
    }

    void GenerateGrid()
    {
        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                GameObject currentTile= Instantiate(tile);
                
                currentTile.transform.position = new Vector3(x - width/2, y-height/2, 1);
                //currentTile.transform.SetParent(transform);
                tiles.Add(currentTile.GetComponent<Tile>());
            }
        }
    }
}

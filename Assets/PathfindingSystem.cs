using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathfindingSystem : MonoBehaviour
{
    public static PathfindingSystem instance { get; private set; }
    public Pathfinding pathfinding;
    int originX = 0;
    int originY = 0;

    void Start()
    {
        instance = this;
        int gridWidth = GridInfo.instance.gridWidth;
        int gridHeight = GridInfo.instance.gridHeight;
        float cellSize = GridInfo.instance.cellSize;

        pathfinding = new Pathfinding(gridWidth, gridHeight, cellSize);
    }

    void Update()
    {/*
        if (Input.GetMouseButtonDown(0))
        {
            Vector3 mouseWorldPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            pathfinding.GetGrid().GetXY(mouseWorldPosition, out int x, out int y);
            List<PathNode> path = pathfinding.FindPath(originX, originY, x, y);
            if (path != null)
            {
                float cellSize = pathfinding.GetGrid().GetCellSize();
                for (int i = 0; i < path.Count - 1; i++)
                {
                    Debug.DrawLine(new Vector3(path[i].x, path[i].y) * cellSize + Vector3.one * cellSize / 2, new Vector3(path[i + 1].x, path[i + 1].y) * cellSize + Vector3.one * cellSize / 2, Color.green, 5f);
                }
            }

            //characterPathfinding.SetTargetPosition(mouseWorldPosition);
        }

        if (Input.GetMouseButtonDown(1))
        {
            Vector3 mouseWorldPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            pathfinding.GetGrid().GetXY(mouseWorldPosition, out int x, out int y);
            pathfinding.GetNode(x, y).SetIsWalkable(!pathfinding.GetNode(x, y).isWalkable);
            originX = x;
            originY = y;
        }

        if (Input.GetMouseButtonDown(2))
        {
            Vector3 mouseWorldPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            pathfinding.GetGrid().GetXY(mouseWorldPosition, out int x, out int y);
            pathfinding.GetNode(x, y).SetIsWalkable(!pathfinding.GetNode(x, y).isWalkable);
        }*/
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PersonMovement : MonoBehaviour
{
    private int currentPathIndex;
    [SerializeField] private List<Vector3> pathVectorList = new List<Vector3>();
    private const float speed = 40f;

    private void Awake()
    {
    }

    private void Update()
    {

        if (Input.GetMouseButtonDown(0))
        {
            Pathfinding pathfinding = PathfindingSystem.instance.pathfinding;
            Vector3 mouseWorldPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Vector3 personPosition = GetPosition();

            pathfinding.GetGrid().GetXY(mouseWorldPosition, out int endX, out int endY);
            pathfinding.GetGrid().GetXY(personPosition, out int startX, out int startY);
            List<PathNode> path = pathfinding.FindPath(startX, startY, endX, endY);
            if (path != null)
            {
                float cellSize = pathfinding.GetGrid().GetCellSize();
                for (int i = 0; i < path.Count - 1; i++)
                {
                    Debug.DrawLine(new Vector3(path[i].x, path[i].y) * cellSize + Vector3.one * cellSize / 2, new Vector3(path[i + 1].x, path[i + 1].y) * cellSize + Vector3.one * cellSize / 2, Color.green, 5f);
                }
            }

            SetTarget(mouseWorldPosition);
        }
    }

    void HandleMovementList()
    {
       if(pathVectorList.Count > 0)
        {
            if(GetPosition() == pathVectorList[0])
            {

            }
        }
    }

    public Vector3 GetPosition()
    {
        return transform.position;
    }
    public void SetTarget(Vector3 targetTransform)
    {   
        pathVectorList = Pathfinding.Instance.FindPath(GetPosition(), targetTransform);
        Debug.Log(pathVectorList);
    }
}

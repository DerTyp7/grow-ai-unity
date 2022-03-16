using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PersonMovement : MonoBehaviour
{
    private int currentPathIndex;
    [SerializeField] private List<Vector3> pathVectorList = new List<Vector3>();
    private const float speed = 5f;
    Rigidbody2D rigidbody;

    private void Awake()
    {
        rigidbody = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        HandleMovementList();

        /*
        //Debug
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
        }*/
    }

    void HandleMovementList()
    {

       if(pathVectorList != null && pathVectorList.Count > 0)
        {
            // Move our position a step closer to the target.
            float step = speed * Time.deltaTime; // calculate distance to move
            transform.position = Vector3.MoveTowards(transform.position, pathVectorList[0], step);

            // Check if the position of the cube and sphere are approximately equal.
            if (Vector3.Distance(transform.position, pathVectorList[0]) < 0.001f)
            {
                pathVectorList.RemoveAt(0);
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
    }
}

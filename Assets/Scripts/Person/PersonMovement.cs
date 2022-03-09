using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PersonMovement : MonoBehaviour
{
    private int currentPathIndex;
    [SerializeField] private List<Vector3> pathVectorList;
    private const float speed = 40f;

    private void Awake()
    {
        //agent.avoidancePriority = Random.Range(1, 100);
    }

    private void Update()
    {
        HandleMovement();

        if (Input.GetMouseButton(0))
        {
            SetTarget(Camera.main.ScreenToWorldPoint(Input.mousePosition));
        }
    }

    void HandleMovement()
    {
        if (pathVectorList != null)
        {
            Vector3 targetPosition = pathVectorList[currentPathIndex];
            if (Vector3.Distance(transform.position, targetPosition) > 1f)
            {
                Vector3 moveDir = (targetPosition - transform.position).normalized;

                float distanceBefore = Vector3.Distance(transform.position, targetPosition);
                transform.position = transform.position + moveDir * speed * Time.deltaTime;
            }
            else
            {
                currentPathIndex++;
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

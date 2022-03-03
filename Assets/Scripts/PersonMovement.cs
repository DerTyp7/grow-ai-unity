using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PersonMovement : MonoBehaviour
{
    NavMeshAgent agent;

    private void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
        agent.updateRotation = false;
        agent.updateUpAxis = false;
        //agent.avoidancePriority = Random.Range(1, 100);
    }

    public void SetTarget(Transform target)
    {
        agent.SetDestination(target.position);
    }
}

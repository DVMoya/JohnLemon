using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class WaypointPatrol : MonoBehaviour
{
    public NavMeshAgent navMeshAgent;
    NavMeshPath navMeshPath;
    public Transform[] waypoints;
    public Transform originalPosition;

    int m_CurrentWaypointIndex;
    bool m_IsReachable;

    void Start()
    {
        navMeshPath = new NavMeshPath();
        m_IsReachable = false;
        navMeshAgent.SetDestination(originalPosition.position);
    }

    void Update()
    {
        //Calculate path from agent to target
        navMeshAgent.CalculatePath(waypoints[m_CurrentWaypointIndex].position, navMeshPath);

        //Check path's status
        if (navMeshPath.status == NavMeshPathStatus.PathComplete)
        {
            m_IsReachable = true;
        }
        else
        {
            m_IsReachable = false;
        }

        if (navMeshAgent.remainingDistance < navMeshAgent.stoppingDistance)
        {
            if (m_IsReachable)
            {
                m_CurrentWaypointIndex = (m_CurrentWaypointIndex + 1) % waypoints.Length;
                navMeshAgent.SetDestination(waypoints[m_CurrentWaypointIndex].position);
            }
            else
            {
                //When the target is not reachable, then the agent returns to it's originalPosition
                navMeshAgent.SetDestination(originalPosition.position);
            }

        }
    }
}

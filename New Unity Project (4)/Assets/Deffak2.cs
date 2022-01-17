using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Deffak2 : MonoBehaviour
{
    public Transform[] patrolPoints;
    public int nextPatrol = 0;

    private NavMeshAgent agent;

    enum EnemyState
    {
        PATROL, ATTACK, CHASE, NONE, BACK
    }

    EnemyState currState;

    public static bool chasing = false;

    // Start is called before the first frame update
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
    }

    // Update is called once per frame
    void Update()
    {
        // print(chasing);
        if (!chasing)
        {
            Patrol();
        }
    }

    void Patrol()
    {
        Vector3 curr = patrolPoints[nextPatrol].position;
        agent.SetDestination(curr);
        if (Vector3.Distance(transform.position, curr) <= 2f)
        {
            nextPatrol++;
            if (nextPatrol >= patrolPoints.Length)
            {
                nextPatrol = 0;
            }
        }
    }
}

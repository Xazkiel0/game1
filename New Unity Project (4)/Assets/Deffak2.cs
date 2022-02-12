using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Deffak2 : MonoBehaviour
{
    public List<Transform> patrolPoints = new List<Transform>();
    public int nextPatrol = 0;

    private NavMeshAgent agent;
    private Animator anim;

    public static bool chasing = false;

    // Start is called before the first frame update
    private void Awake()
    {
    }

    void findPatrolPoints()
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            Transform child = transform.GetChild(i);
            if (child.CompareTag("Patrol Point"))
            {
                print(child.name);
                patrolPoints.Add(child);
            }
        }
    }
    void Start()
    {
        findPatrolPoints();
        print(transform.name + patrolPoints.Count);
        agent = GetComponentInChildren<NavMeshAgent>();
        StartCoroutine(Patrol());
        anim = agent.GetComponent<Animator>();
    }


    // Update is called once per frame
    void Update()
    {
        if (!chasing)
        {
            Vector3 curr = patrolPoints[nextPatrol].position;
            agent.SetDestination(curr);
            if (reachPoint(1.5f))
                anim.SetFloat("Speed", 0);
            else
                anim.SetFloat("Speed", 1);
        }
    }

    bool reachPoint(float dist)
    {
        return Vector3.Distance(agent.transform.position, patrolPoints[nextPatrol].position) <= dist;
    }

    IEnumerator Patrol()
    {
        yield return new WaitUntil(() => reachPoint(2f));
        yield return new WaitForSeconds(1f);
        nextPatrol++;
        if (nextPatrol >= patrolPoints.Count)
        {
            nextPatrol = 0;
        }
        StartCoroutine(Patrol());
    }
}

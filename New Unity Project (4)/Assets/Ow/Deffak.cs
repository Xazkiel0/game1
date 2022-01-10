using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Deffak : MonoBehaviour
{
    public Transform target;
    public float findDistance = 10f;
    NavMeshAgent agent;
    Vector3 basePosition;

    public Transform[] patrolPoints;
    public int patrolIdx = 0;
    public bool callOnce = false;

    // Start is called before the first frame update
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        // basePosition = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        float tarDistance = Vector3.Distance(target.position, transform.position);

        if (tarDistance <= findDistance && !Deffak2.chasing)
        {
            agent.SetDestination(target.position);
            if (tarDistance <= agent.stoppingDistance)
                faceTarget();

        }
        else
        {
            if (Vector3.Distance(transform.position, patrolPoints[patrolIdx].position) <= 2f)
            {
                if (!callOnce)
                {
                    callOnce = true;
                    StartCoroutine(findAround());
                }

            }
            else
                agent.SetDestination(patrolPoints[patrolIdx].position);
            // if (Vector3.Distance(transform.position, basePosition) <= 2.6f)
            // {
            // }
            // else

        }
    }

    void faceTarget()
    {
        Vector3 direction = (target.position - transform.position).normalized;
        Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z));
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * 5f);
    }

    IEnumerator findAround()
    {
        yield return new WaitForSeconds(1f);
        if (patrolIdx + 1 < patrolPoints.Length)
            patrolIdx++;
        else
            patrolIdx = 0;

        callOnce = false;
    }


    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, findDistance);
    }
}

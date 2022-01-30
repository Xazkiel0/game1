using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class FieldOfView : MonoBehaviour
{
    public float viewRad = 5f;
    [Range(0, 170)]
    public float viewAngle;
    public LayerMask targetMask;
    public LayerMask obsMask;
    public Vector3 respPoint;

    public List<Transform> visibleTargets = new List<Transform>();
    NavMeshAgent agent;
    private void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        respPoint = transform.position;
        StartCoroutine(FindTargetsWithDelay(.2f));
    }

    private void Update()
    {
        if (visibleTargets.Count > 0)
        {
            agent.SetDestination(nearestTarget().position);
        }
        else
        {
            agent.ResetPath();
        }
    }

    IEnumerator FindTargetsWithDelay(float delay)
    {
        while (true)
        {
            yield return new WaitForSeconds(delay);
            FindVisibleTarget();
        }
    }

    void FindVisibleTarget()
    {
        visibleTargets.Clear();
        Collider[] targetsInViewRad = Physics.OverlapSphere(transform.position, viewRad, targetMask);
        for (int i = 0; i < targetsInViewRad.Length; i++)
        {
            Transform target = targetsInViewRad[i].transform;
            Vector3 dirToTarget = (target.position - transform.position).normalized;
            if (Vector3.Angle(transform.forward, dirToTarget) < viewAngle / 2)
            {
                float dstToTarget = Vector3.Distance(transform.position, target.position);
                if (!Physics.Raycast(transform.position, dirToTarget, dstToTarget, obsMask))
                {
                    if (!visibleTargets.Contains(target))
                    {
                        visibleTargets.Add(target);

                    }
                }
            }
        }
    }
    public Vector3 DirFromAngle(float angleInDegrees, bool angleIsGlobal)
    {
        if (!angleIsGlobal)
        {
            angleInDegrees += transform.eulerAngles.y;
        }
        return new Vector3(Mathf.Sin(angleInDegrees * Mathf.Deg2Rad), 0, Mathf.Cos(angleInDegrees * Mathf.Deg2Rad));
    }
    Transform nearestTarget()
    {
        Transform currClosest = visibleTargets[0];
        foreach (Transform target in visibleTargets)
            if (Vector3.Distance(transform.position, currClosest.position) < Vector3.Distance(transform.position, target.position))
                currClosest = target;

        return currClosest;
    }
}

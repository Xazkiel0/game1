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

    Animator anim;

    public List<Transform> visibleTargets = new List<Transform>();
    NavMeshAgent agent;
    private void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        StartCoroutine(FindTargetsWithDelay(.2f));
        anim = GetComponent<Animator>();
    }

    private void Update()
    {
        if (visibleTargets.Count <= 0)
        {
            anim.SetFloat("Speed", 0);
            agent.ResetPath();
            return;
        }
        agent.SetDestination(nearestTarget().position);
        anim.SetFloat("Speed", 1);
        if (agent.remainingDistance <= agent.stoppingDistance)
        {
            anim.SetFloat("Speed", 0);
            anim.SetTrigger("Attack");
            return;
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
        Collider[] targetsCloseRad = Physics.OverlapSphere(transform.position, viewRad, targetMask);
        Collider[] targetsInViewRad = Physics.OverlapSphere(transform.position, viewRad * 2, targetMask);

        if (visibleTargets.Count > 0)
            visibleTargets.Clear();

        if (targetsInViewRad.Length == 0)
            return;


        for (int i = 0; i < targetsInViewRad.Length; i++)
        {
            Transform target = targetsInViewRad[i].transform;
            Vector3 dirToTarget = (target.position - transform.position).normalized;
            if (Vector3.Angle(transform.forward, dirToTarget) < viewAngle / 2)
            {
                float dstToTarget = Vector3.Distance(transform.position, target.position);
                if (!Physics.Raycast(transform.position, dirToTarget, dstToTarget, obsMask))
                    visibleTargets.Add(target);
            }
        }
        for (int i = 0; i < targetsCloseRad.Length; i++)
        {
            Transform target = targetsCloseRad[i].transform;
            visibleTargets.Add(target);
        }
    }
    public Vector3 DirFromAngle(float angleInDegrees, bool angleIsGlobal)
    {
        if (!angleIsGlobal)
            angleInDegrees += transform.eulerAngles.y;

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

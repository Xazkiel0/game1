using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class FieldOfView : MonoBehaviour
{
    public float viewRad = 5f;
    [Range(0, 170)]
    public float viewAngle;
    [Range(5, 10)] public int turningMultiplier = 5;
    public LayerMask targetMask;
    public LayerMask obsMask;

    Animator anim;

    [HideInInspector] public Transform visibleTargets = null;
    NavMeshAgent agent;
    private void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        StartCoroutine(FindTargetsWithDelay(.2f));
        anim = GetComponent<Animator>();
    }

    private void Update()
    {
        if (visibleTargets == null)
        {
            anim.SetFloat("Speed", 0);
            agent.ResetPath();
            return;
        }
        agent.SetDestination(visibleTargets.position);
        anim.SetFloat("Speed", 1);
        if (agent.remainingDistance <= agent.stoppingDistance)
        {
            anim.SetFloat("Speed", 0);
            adyFaceTarget();
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

    void adyFaceTarget()
    {
        Vector3 dirFromTarget = (visibleTargets.position - transform.position).normalized;
        float dotTarget = Vector3.Dot(transform.forward, dirFromTarget);
        print(dotTarget);
        if (dotTarget <= 0.9f)
        {
            lookTarget();
            return;
        }
        if (dotTarget > .9f)
        {
            anim.SetTrigger("Attack");
        }
    }

    void lookTarget()
    {
        Vector3 lookPos = visibleTargets.position - transform.position;
        lookPos.y = 0;
        Quaternion rotation = Quaternion.LookRotation(lookPos);
        transform.rotation = Quaternion.Slerp(transform.rotation, rotation, Time.deltaTime * turningMultiplier);
    }

    void FindVisibleTarget()
    {
        visibleTargets = null;
        if (!Physics.CheckSphere(transform.position, viewRad * 2, obsMask))
            if (!Physics.CheckSphere(transform.position, viewRad, obsMask))
                return;

        Collider[] targetsCloseRad = Physics.OverlapSphere(transform.position, viewRad, targetMask);
        Collider[] targetsInViewRad = Physics.OverlapSphere(transform.position, viewRad * 2, targetMask);

        for (int i = 0; i < targetsInViewRad.Length; i++)
        {
            Transform target = targetsInViewRad[i].transform;
            Vector3 dirToTarget = (target.position - transform.position).normalized;
            if (Vector3.Angle(transform.forward, dirToTarget) < viewAngle / 2)
            {
                float dstToTarget = Vector3.Distance(transform.position, target.position);
                if (!Physics.Raycast(transform.position, dirToTarget, dstToTarget, obsMask))
                    visibleTargets = target;
            }
        }
        for (int i = 0; i < targetsCloseRad.Length; i++)
        {
            visibleTargets = targetsCloseRad[i].transform;
        }
    }
    public Vector3 DirFromAngle(float angleInDegrees, bool angleIsGlobal)
    {
        if (!angleIsGlobal)
            angleInDegrees += transform.eulerAngles.y;

        return new Vector3(Mathf.Sin(angleInDegrees * Mathf.Deg2Rad), 0, Mathf.Cos(angleInDegrees * Mathf.Deg2Rad));
    }
    // Transform nearestTarget()
    // {
    //     Transform currClosest = visibleTargets[0];
    //     foreach (Transform target in visibleTargets)
    //         if (Vector3.Distance(transform.position, currClosest.position) < Vector3.Distance(transform.position, target.position))
    //             currClosest = target;

    //     return currClosest;
    // }
}

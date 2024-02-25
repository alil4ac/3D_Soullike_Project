using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PathFinder : MonoBehaviour
{
    public GameObject target;
    NavMeshAgent agent;
    LineRenderer lr;

    void Start()
    {
        agent = this.GetComponent<NavMeshAgent>();

        lr = this.GetComponent<LineRenderer>();
        lr.startWidth = lr.endWidth = 0.1f;
        lr.material.color = Color.blue;
        lr.enabled = false;
    }

    public void makePath()
    {
        lr.enabled = true;
        StartCoroutine(makePathCoroutine());
    }

    void drawPath()
    {
        int length = agent.path.corners.Length;

        lr.positionCount = length;
        for (int i = 1; i < length; i++)
            lr.SetPosition(i, agent.path.corners[i]);
    }

    IEnumerator makePathCoroutine()
    {
        agent.SetDestination(target.transform.position);
        lr.SetPosition(0, this.transform.position);

        while (Vector3.Distance(this.transform.position, target.transform.position) > 0.1f)
        {
            lr.SetPosition(0, this.transform.position);

            drawPath();

            yield return null;
        }

        lr.enabled = false;
    }
}
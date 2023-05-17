using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GhostManager : MovingObject
{
    public Transform target;

    private void Start()
    {
        StartCoroutine(GhostMove());
    }

    IEnumerator GhostMove()
    {
        transform.LookAt(target);
        transform.position = Vector3.MoveTowards(transform.position, target.position, speed * walkCount);
        yield return new WaitForSeconds(1f);
    }
}
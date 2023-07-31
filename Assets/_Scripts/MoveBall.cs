using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveBall : MonoBehaviour
{
    public Transform[] waypoints;
    public float speed = 5f;
    public float stoppingDistance = 0.2f;

    private int currentWaypointIndex = 0;
    private void Start()
    {
        
    }
    void Update()
    {
        if (waypoints.Length == 0)
            return;
       
        Vector3 targetPosition = waypoints[currentWaypointIndex].position;
        Vector3 moveDirection = targetPosition - transform.position;
        float distanceToTarget = moveDirection.magnitude;
        if (distanceToTarget <= stoppingDistance)
        {
            currentWaypointIndex++;
            if (currentWaypointIndex >= waypoints.Length)
                currentWaypointIndex = waypoints.Length-1;
        }
        moveDirection.Normalize();
        transform.Translate(moveDirection * speed * Time.deltaTime);
    }
}

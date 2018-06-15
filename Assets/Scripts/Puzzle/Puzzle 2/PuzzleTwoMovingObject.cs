using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PuzzleTwoMovingObject : MonoBehaviour
{
    public Transform[] Waypoints;
    private int currentPoint;
    public float MovementSpeed = 2f;

    void Start()
    {
        // First point
        currentPoint = 0;

        // Starting Position
        transform.position = Waypoints[currentPoint].position;
    }

    void Update()
    {
        // If it reaches the waypoint
        if (transform.position == Waypoints[currentPoint].position)
        {
            currentPoint++;
        }

        // If it reaches the last waypoint, return to the beginning
        if (currentPoint >= Waypoints.Length)
        {
            currentPoint = 0;
        }

        // Movement of the object
        transform.position = Vector3.MoveTowards(transform.position, Waypoints[currentPoint].position, MovementSpeed * Time.deltaTime);

    }
}

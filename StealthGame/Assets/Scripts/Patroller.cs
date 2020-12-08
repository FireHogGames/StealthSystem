using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Patroller : AIController
{
    public GameObject patrolObject;
    public float idleLength;
    public float range;
    public bool doPatrol;

    int patrolIndex = 0;
    bool canCount;
    private List<Transform> patrolPath = new List<Transform>();

    // This is the same as start but runs after the AI is setup
    public override void Init()
    {
        base.Init();
        //-v- Run everything under the base -v-

        //Set the patrol point height to that of the AI controller (For better distance checking)
        Vector3 pos = patrolObject.transform.position;
        pos.y = transform.position.y;
        patrolObject.transform.position = pos;

        
        Transform[] patrolPoints = patrolObject.GetComponentsInChildren<Transform>(); // Find all the patrol points in the patrol holder
        for (int i = 0; i < patrolPoints.Length; i++) // Loop through all the found points
        {
            if(patrolPoints[i] != patrolObject.transform) // If the patrol point is not the point holder add it to the list of points
            {
                patrolPath.Add(patrolPoints[i]);
            }
        }

        transform.position = patrolPath[0].transform.position; // Set the AI to the first point

        StartCoroutine(Patrol()); // Start patrolling
    }

    public void StopPatrol()
    {
        // Stop the agent
        StopAgent();
    }

    private IEnumerator Patrol()
    {
        while (doPatrol)// Loop while doPatrol is true
        {
            if (Vector3.Distance(transform.position, patrolPath[patrolIndex].position) >= range)// If the AI is not close to its objective -> keep going
            {
                SetDestination(patrolPath[patrolIndex].position);
            }
            else if (Vector3.Distance(transform.position, patrolPath[patrolIndex].position) <= range) // If the AI is close or reached its objective its objective -> Stop for a few seconds then go to the next
            {
                if (patrolIndex != 0) // Check if the AI is a the first point or not
                {
                    yield return new WaitForSeconds(idleLength); // Wait for idleLength amound of seconds
                    patrolIndex++; // After idleLength amount of seconds -> Continue
                }
                else
                {
                    patrolIndex++; // Immidiatly continue
                }
            }

            if (patrolIndex == patrolPath.Count) // If the patroller is at the end of the path go back to zero
            {
                patrolIndex = 0;
            }

            yield return null;
        }
    }

    public void SetPatrolStatus(bool value)
    {
        doPatrol = value;
    }
}

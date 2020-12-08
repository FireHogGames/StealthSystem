using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AIController : MonoBehaviour
{
    NavMeshAgent agent;

    // Start is called before the first frame update
    void Start()
    {
        // Call the init function
        Init();
    }

    public void SetDestination(Vector3 location) // Set the destenation of the agent to a custom vector 3
    {
        agent.SetDestination(location);
    }

    // Stop the AI where it stands
    public void StopAgent()
    {
        SetDestination(transform.position);
    }

    public virtual void Init()
    {
        //Initialize the agent
        agent = GetComponent<NavMeshAgent>();
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AI_Navigation : MonoBehaviour
{
    private GameObject destination;
    private NavMeshAgent agent;
    private Animator animator;  // Reference to the Animator component

    void Start()
    {
        destination = GameObject.FindGameObjectWithTag("Destination");
        agent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();  // Get the Animator component

        agent.SetDestination(destination.transform.position);
    }

    void Update()
    {
        // Check if the AI is moving and update the animator parameter
        bool isWalking = !agent.pathPending && agent.remainingDistance > agent.stoppingDistance;
        animator.SetBool("Walking", isWalking);
    }
}


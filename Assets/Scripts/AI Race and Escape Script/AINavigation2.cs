using UnityEngine;
using UnityEngine.AI;

public class AINavigation2 : MonoBehaviour
{
    public Transform destination; // Tujuan navigasi
    private NavMeshAgent agent;   // Komponen NavMeshAgent

    private void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
        if (agent == null)
        {
            Debug.LogError($"{gameObject.name}: NavMeshAgent not found!");
        }
    }

    public void SetDestination(Transform target)
    {
        destination = target;
        if (agent != null && destination != null)
        {
            agent.enabled = true;
            agent.SetDestination(destination.position);
            Debug.Log($"{gameObject.name}: NavMesh Navigation started towards {destination.name}.");
        }
    }

    public void StopNavigation()
    {
        if (agent != null)
        {
            agent.enabled = false;
            Debug.Log($"{gameObject.name}: NavMesh Navigation stopped.");
        }
    }

    private void Update()
    {
        if (agent == null || !agent.enabled || destination == null) return;

        if (!agent.pathPending && agent.remainingDistance <= agent.stoppingDistance)
        {
            if (!agent.hasPath || agent.velocity.sqrMagnitude == 0f)
            {
                Debug.Log($"{gameObject.name}: Reached destination.");
            }
        }
    }
}

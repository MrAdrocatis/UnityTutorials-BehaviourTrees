using UnityEngine;
using UnityEngine.AI;

public class NavMeshController : MonoBehaviour
{
    private NavMeshAgent agent;
    private Transform target;

    private void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
        if (agent == null)
        {
            Debug.LogError($"{gameObject.name}: Missing NavMeshAgent component!");
        }
    }

    private void Start()
    {
        DisableNavigation(); // Matikan navigasi saat start
    }

    public void EnableNavigation()
    {
        if (agent == null)
        {
            Debug.LogError($"{gameObject.name}: NavMeshAgent not initialized!");
            return;
        }

        if (!agent.isOnNavMesh)
        {
            Debug.LogError($"{gameObject.name}: NavMeshAgent is not on a NavMesh!");
            return;
        }

        agent.enabled = true;

        if (target != null)
        {
            agent.SetDestination(target.position);
            Debug.Log($"{gameObject.name}: NavMesh navigation enabled towards {target.name}.");
        }
        else
        {
            Debug.LogWarning($"{gameObject.name}: No target assigned for NavMesh navigation!");
        }
    }

    public void DisableNavigation()
    {
        if (agent != null && agent.enabled)
        {
            agent.ResetPath();
            agent.enabled = false;
            Debug.Log($"{gameObject.name}: NavMesh navigation disabled.");
        }
    }

    public void SetTarget(Transform newTarget)
    {
        target = newTarget;

        if (agent != null && agent.enabled)
        {
            agent.SetDestination(target.position);
            Debug.Log($"{gameObject.name}: NavMesh target set to {target.name}.");
        }
    }
}

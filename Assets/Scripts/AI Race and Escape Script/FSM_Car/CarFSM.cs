using UnityEngine;

public class CarFSM : MonoBehaviour
{
    public enum State { Idle, Steering, Navigating, Finished }
    public State currentState = State.Idle;

    public Transform target; // Target navigasi
    private Rigidbody rb;
    private NavMeshController navMeshController;
    private SteeringController steeringController;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        navMeshController = GetComponent<NavMeshController>();
        steeringController = GetComponent<SteeringController>();

        if (rb == null || navMeshController == null || steeringController == null)
        {
            Debug.LogError($"{gameObject.name} requires Rigidbody, NavMeshController, and SteeringController components!");
        }
    }

    private void Start()
    {
        SwitchState(State.Idle);
    }

    public void SetTarget(Transform newTarget)
    {
        target = newTarget;

        if (currentState == State.Navigating && navMeshController != null)
        {
            navMeshController.SetTarget(target);
            Debug.Log($"{gameObject.name} target updated for NavMesh navigation.");
        }

        if (currentState == State.Steering && steeringController != null)
        {
            steeringController.SetTarget(target);
            Debug.Log($"{gameObject.name} target updated for Steering behavior.");
        }
    }

    public void SwitchState(State newState)
    {
        if (currentState == newState) return;

        DisableAllModes(); // Pastikan semua mode dimatikan sebelum mengganti state

        currentState = newState;

        switch (newState)
        {
            case State.Idle:
                Debug.Log($"{gameObject.name} switched to Idle state.");
                break;

            case State.Steering:
                EnableSteeringBehavior();
                Debug.Log($"{gameObject.name} switched to Steering behavior.");
                break;

            case State.Navigating:
                EnableNavMeshNavigation();
                Debug.Log($"{gameObject.name} switched to NavMesh navigation.");
                break;

            case State.Finished:
                DisableAllModes();
                Debug.Log($"{gameObject.name} switched to Finished state.");
                break;
        }
    }

    private void EnableSteeringBehavior()
    {
        if (rb != null)
        {
            rb.isKinematic = false; // Nonaktifkan mode kinematik untuk Steering
            rb.velocity = Vector3.zero;
            rb.angularVelocity = Vector3.zero;
        }

        if (navMeshController != null)
        {
            navMeshController.DisableNavigation();
        }

        if (steeringController != null)
        {
            steeringController.enabled = true;
        }
    }

    private void EnableNavMeshNavigation()
    {
        if (rb != null)
        {
            rb.isKinematic = true; // Aktifkan mode kinematik untuk NavMesh
            rb.velocity = Vector3.zero;
            rb.angularVelocity = Vector3.zero;
        }

        if (steeringController != null)
        {
            steeringController.enabled = false;
        }

        if (navMeshController != null)
        {
            navMeshController.EnableNavigation();
            navMeshController.SetTarget(target);
        }
    }

    private void DisableAllModes()
    {
        if (rb != null)
        {
            rb.velocity = Vector3.zero;
            rb.angularVelocity = Vector3.zero;
        }

        if (steeringController != null)
        {
            steeringController.enabled = false;
        }

        if (navMeshController != null)
        {
            navMeshController.DisableNavigation(); // Jangan panggil ResetPath jika NavMeshAgent tidak aktif
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (currentState != State.Finished && other.CompareTag("Finish"))
        {
            SwitchState(State.Finished);

            GameManager gameManager = FindObjectOfType<GameManager>();
            if (gameManager != null)
            {
                gameManager.CheckWinner(gameObject.name);
            }
        }
    }
}

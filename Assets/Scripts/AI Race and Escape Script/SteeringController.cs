using UnityEngine;

public class SteeringController : MonoBehaviour
{
    public Transform pathGroup;
    public float maxSteerAngle = 30f;
    public float maxMotorTorque = 150f;
    public float maxSpeed = 100f;
    public float slowDownSpeed = 50f;
    public float brakeForce = 300f;
    public float waypointThreshold = 5f;

    private Transform[] waypoints;
    private int currentWaypoint = 0;
    private Rigidbody rb;

    private Transform target;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        if (rb == null)
        {
            Debug.LogError($"{gameObject.name}: Rigidbody component is missing!");
        }
    }

    private void Start()
    {
        if (pathGroup != null)
        {
            GetWaypoints();
        }
    }

    private void FixedUpdate()
    {
        if (!enabled) return;

        if (rb != null && !rb.isKinematic)
        {
            Steer();
            AdjustSpeed();
            Drive();
            CheckWaypointDistance();
        }
        else
        {
            Debug.LogWarning($"{gameObject.name}: Rigidbody must not be kinematic for Steering behavior.");
        }
    }

    public void SetTarget(Transform newTarget)
    {
        target = newTarget;
        Debug.Log($"{gameObject.name}: Steering target set to {target?.name ?? "null"}.");
    }

    private void GetWaypoints()
    {
        Transform[] pathObjs = pathGroup.GetComponentsInChildren<Transform>();
        waypoints = new Transform[pathObjs.Length - 1];
        for (int i = 1; i < pathObjs.Length; i++)
        {
            waypoints[i - 1] = pathObjs[i];
        }
    }

    private void Steer()
    {
        if (waypoints == null || waypoints.Length == 0) return;

        Vector3 relativeVector = transform.InverseTransformPoint(waypoints[currentWaypoint].position);
        float steerAngle = (relativeVector.x / relativeVector.magnitude) * maxSteerAngle;
        Debug.Log($"{gameObject.name}: Steering to waypoint {currentWaypoint} with angle {steerAngle}");
    }

    private void AdjustSpeed() { /* Simplified for brevity */ }
    private void Drive() { /* Simplified for brevity */ }
    private void CheckWaypointDistance() { /* Simplified for brevity */ }
}

using UnityEngine;



public class AICarScript : MonoBehaviour
{
    public Transform pathGroup;          // Objek grup path
    public float maxSteerAngle = 30f;    // Sudut maksimal kemudi
    public float maxMotorTorque = 150f;  // Torsi maksimal motor
    public float maxSpeed = 100f;        // Kecepatan maksimal
    public float slowDownSpeed = 50f;    // Kecepatan saat belokan tajam
    public float brakeForce = 300f;      // Kekuatan rem
    public float waypointThreshold = 5f; // Jarak minimal ke waypoint
    public float steerSensitivity = 0.1f; // Sensitivitas kemudi

    public WheelCollider wheelFL;        // WheelCollider roda depan kiri
    public WheelCollider wheelFR;        // WheelCollider roda depan kanan
    public WheelCollider wheelRL;        // WheelCollider roda belakang kiri
    public WheelCollider wheelRR;        // WheelCollider roda belakang kanan

    private Transform[] waypoints;       // Array titik-titik jalur
    private int currentWaypoint = 0;     // Indeks waypoint saat ini
    private Rigidbody rb;                // Komponen Rigidbody

    private float targetSpeed;           // Kecepatan target yang dinamis
    private float currentSteerAngle;     // Sudut kemudi saat ini

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        GetWaypoints();                  // Ambil titik-titik jalur dari pathGroup
        targetSpeed = maxSpeed;          // Inisialisasi targetSpeed ke maxSpeed

    }

    void FixedUpdate()
    {
        Steer();                         // Mengatur kemudi
        AdjustSpeed();                   // Menyesuaikan kecepatan
        Drive();                         // Menggerakkan mobil
        CheckWaypointDistance();         // Cek jarak ke waypoint berikutnya
    }

    // Ambil titik-titik jalur dari grup pathGroup
    void GetWaypoints()
    {
        Transform[] pathObjs = pathGroup.GetComponentsInChildren<Transform>();
        waypoints = new Transform[pathObjs.Length - 1]; // Kecualikan parent-nya
        for (int i = 0; i < pathObjs.Length; i++)
        {
            if (pathObjs[i] != pathGroup)
            {
                waypoints[i - 1] = pathObjs[i];
            }
        }
    }

    // Mengatur kemudi roda depan secara bertahap
    void Steer()
    {
        // Hitung vektor relatif ke waypoint saat ini
        Vector3 relativeVector = transform.InverseTransformPoint(waypoints[currentWaypoint].position);
        float desiredSteerAngle = (relativeVector.x / relativeVector.magnitude) * maxSteerAngle;

        // Gunakan Lerp untuk membuat kemudi halus
        currentSteerAngle = Mathf.Lerp(currentSteerAngle, desiredSteerAngle, steerSensitivity * Time.fixedDeltaTime);
        wheelFL.steerAngle = currentSteerAngle;
        wheelFR.steerAngle = currentSteerAngle;
    }

    // Menyesuaikan kecepatan sesuai dengan belokan
    void AdjustSpeed()
    {
        // Hitung sudut antara mobil dan waypoint saat ini
        Vector3 relativeVector = transform.InverseTransformPoint(waypoints[currentWaypoint].position);
        float angleToWaypoint = Mathf.Abs(Mathf.Atan2(relativeVector.x, relativeVector.z) * Mathf.Rad2Deg);

        // Kurangi targetSpeed jika belokan tajam
        if (angleToWaypoint > 10f)
        {
            targetSpeed = Mathf.Lerp(targetSpeed, slowDownSpeed, 0.05f);
        }
        else
        {
            targetSpeed = Mathf.Lerp(targetSpeed, maxSpeed, 0.05f);
        }
    }

    // Menggerakkan mobil maju atau melambat
    void Drive()
    {
        float currentSpeed = rb.velocity.magnitude * 3.6f; // Konversi ke km/h

        if (currentSpeed < targetSpeed)
        {
            wheelRL.motorTorque = maxMotorTorque;
            wheelRR.motorTorque = maxMotorTorque;
            wheelRL.brakeTorque = 0;
            wheelRR.brakeTorque = 0;
        }
        else
        {
            wheelRL.motorTorque = 0;
            wheelRR.motorTorque = 0;
            wheelRL.brakeTorque = brakeForce;
            wheelRR.brakeTorque = brakeForce;
        }
    }

    // Pindah ke waypoint berikutnya jika jarak sudah cukup dekat
    void CheckWaypointDistance()
    {
        float distanceToWaypoint = Vector3.Distance(transform.position, waypoints[currentWaypoint].position);
        if (distanceToWaypoint < waypointThreshold)
        {
            currentWaypoint++;
            if (currentWaypoint >= waypoints.Length)
            {
                currentWaypoint = 0; // Kembali ke waypoint pertama jika sudah di akhir jalur
            }
        }
    }
}

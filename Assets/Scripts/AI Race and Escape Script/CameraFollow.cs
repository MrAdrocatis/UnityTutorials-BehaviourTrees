
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform target; // Mobil yang akan diikuti
    public Vector3 offset; // Posisi offset
    public float smoothSpeed = 0.125f; // Kecepatan mengikuti

    private void LateUpdate()
    {
        if (target == null)
        {
            Debug.LogError($"[{gameObject.name}] Target belum diassign untuk kamera!");
            return;
        }

        // Posisi yang diinginkan
        Vector3 desiredPosition = target.position + offset;
        Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);
        transform.position = smoothedPosition;

        // Kamera menghadap target
        transform.LookAt(target);
    }
}


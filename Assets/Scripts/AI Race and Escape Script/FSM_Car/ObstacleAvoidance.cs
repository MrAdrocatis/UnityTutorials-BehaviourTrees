using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleAvoidance : MonoBehaviour
{
    public float targetVelocity = 10.0f;
    public int numberOfRays = 17;
    public float angle = 90.0f;
    public float rayRange = 2.0f;

    // Update is called once per frame
    void Update()
    {
        var deltaPosition = Vector3.zero;

        for (int i = 0; i < numberOfRays; i++)
        {
            var rotation = this.transform.rotation;
            var rotationMod = Quaternion.AngleAxis((i / ((float)numberOfRays - 1)) * angle * 2 - angle, this.transform.up);
            var direction = rotation * rotationMod * Vector3.forward;

            var ray = new Ray(this.transform.position, direction);
            RaycastHit hitInfo;

            if (Physics.Raycast(ray, out hitInfo, rayRange))
            {
                deltaPosition -= (1.0f / numberOfRays) * targetVelocity * direction;
            }
            else
            {
                deltaPosition += (1.0f / numberOfRays) * targetVelocity * direction;
            }
        }

        this.transform.position += deltaPosition * Time.deltaTime;
    }

    // Tambahkan Gizmos untuk memvisualisasikan raycast
    private void OnDrawGizmos()
    {
        if (!Application.isPlaying) return;

        Gizmos.color = Color.red;

        for (int i = 0; i < numberOfRays; i++)
        {
            var rotation = this.transform.rotation;
            var rotationMod = Quaternion.AngleAxis((i / ((float)numberOfRays - 1)) * angle * 2 - angle, this.transform.up);
            var direction = rotation * rotationMod * Vector3.forward;

            var start = this.transform.position;
            var end = start + direction * rayRange;

            Gizmos.DrawLine(start, end);
            Gizmos.DrawSphere(end, 0.05f); // Tambahkan titik di ujung garis untuk memperjelas
        }
    }
}

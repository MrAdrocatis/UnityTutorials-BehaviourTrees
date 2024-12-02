using UnityEngine;

public class CarFSM : MonoBehaviour
{
    public enum State
    {
        Idle,
        Navigating,
        Finished
    }

    private State currentState;
    private AICarScript aiCarScript;  // Ganti dengan komponen yang sesuai jika perlu
    private AINavigation2 aiNavigation; // Ganti dengan komponen yang sesuai jika perlu

    private void Start()
    {
        aiCarScript = GetComponent<AICarScript>();  // Mengambil AICarScript
        aiNavigation = GetComponent<AINavigation2>(); // Mengambil AINavigation2

        if (aiCarScript == null || aiNavigation == null)
        {
            Debug.LogError("Missing necessary components.");
        }

        SwitchState(State.Idle); // Set state awal ke Idle
    }

    public void SwitchState(State newState)
    {
        currentState = newState;
        Debug.Log($"Switched to state: {currentState}");

        switch (currentState)
        {
            case State.Idle:
                // Aksi saat mobil dalam keadaan idle
                if (aiNavigation != null)
                {
                    aiNavigation.StopNavigation(); // Matikan navigasi jika diperlukan
                }
                break;

            case State.Navigating:
                // Aksi saat mobil sedang menavigasi
                if (aiNavigation != null)
                {
                    aiNavigation.SetDestination(aiNavigation.destination); // Atur tujuan navigasi
                }
                break;

            case State.Finished:
                // Aksi saat mobil selesai (misalnya mencapai finish line)
                if (aiCarScript != null)
                {
                    Debug.Log("Car reached the finish line.");
                }
                break;
        }
    }

    public void SetTarget(Transform target)
    {
        // Jika menggunakan AINavigation2 atau metode lain untuk mengatur tujuan
        if (aiNavigation != null)
        {
            aiNavigation.SetDestination(target);
        }
    }

    // Fungsi untuk menangani finish line
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Finish"))
        {
            GameManager gameManager = FindObjectOfType<GameManager>();
            if (gameManager != null)
            {
                gameManager.CheckWinner(gameObject.name); // Periksa pemenang jika mobil mencapai garis finish
            }
        }
    }
}

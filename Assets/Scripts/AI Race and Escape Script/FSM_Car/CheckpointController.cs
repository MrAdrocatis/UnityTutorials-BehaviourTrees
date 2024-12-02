using UnityEngine;

public class CheckpointController : MonoBehaviour
{
    [Header("Checkpoint Settings")]
    public bool switchToSteering; // Apakah beralih ke steering mode?
    public bool deactivateAfterTrigger = true; // Apakah checkpoint akan dinonaktifkan setelah dipicu?

    private void OnTriggerEnter(Collider other)
    {
        //if (!other.CompareTag("Body")) return;

        //var carFSM = other.GetComponentInParent<CarFSM>();
        //if (carFSM == null) return;

        // Switch mode sesuai checkpoint
        //if (gameObject.name == "Checkpoint1")
        //{
            //if (carFSM.currentState == CarFSM.State.Navigating)
            //{
            //    Debug.Log($"{carFSM.gameObject.name} already in NavMesh mode.");
            //    return;
            //}
            //.SwitchToNavigationMode();
            //Debug.Log($"{carFSM.gameObject.name} switched to NavMesh mode at {gameObject.name}.");
        //}
        //else if (gameObject.name == "Checkpoint2")
        //{
            //if (carFSM.currentState == CarFSM.State.Idle)
            //{
            //    Debug.Log($"{carFSM.gameObject.name} already in Steering mode.");
            //    return;
            //}
            //carFSM.SwitchToSteeringMode();
            //Debug.Log($"{carFSM.gameObject.name} switched to Steering mode at {gameObject.name}.");
        //}

        // Nonaktifkan checkpoint setelah dipicu
        gameObject.SetActive(false);
    }

}



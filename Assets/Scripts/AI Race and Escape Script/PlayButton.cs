using UnityEngine;

public class PlayButton : MonoBehaviour
{
    public CarFSM playerCarFSM;
    public CarFSM enemyCarFSM;

    public Camera playerCamera;
    public Camera enemyCamera;

    public GameObject panelStart;
    public GameObject carPositionUI;

    private void Start()
    {
        Time.timeScale = 0f;
        Debug.Log("PlayButton: Game paused. Waiting for Start.");
    }

    public void StartGame()
    {
        Time.timeScale = 1f;

        if (playerCarFSM != null) playerCarFSM.SwitchState(CarFSM.State.Navigating);
        if (enemyCarFSM != null) enemyCarFSM.SwitchState(CarFSM.State.Navigating);

        if (playerCamera != null) playerCamera.gameObject.SetActive(true);
        if (enemyCamera != null) enemyCamera.gameObject.SetActive(true);

        if (panelStart != null) panelStart.SetActive(false);
        if (carPositionUI != null) carPositionUI.SetActive(false);

        Debug.Log("PlayButton: Game started.");
    }
}

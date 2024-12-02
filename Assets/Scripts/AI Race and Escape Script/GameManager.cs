using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public CarFSM playerCarFSM;
    public CarFSM enemyCarFSM;
    public Transform commonTarget;

    public GameObject winUI;
    public GameObject loseUI;
    public GameObject gameOverPanel;

    private bool gameFinished = false;

    private void Start()
    {
        if (playerCarFSM != null)
        {
            playerCarFSM.SetTarget(commonTarget);
            Debug.Log("Player car's target set.");
        }

        if (enemyCarFSM != null)
        {
            enemyCarFSM.SetTarget(commonTarget);
            Debug.Log("Enemy car's target set.");
        }
    }

    public void SwitchToSteeringMode()
    {
        if (playerCarFSM != null)
        {
            playerCarFSM.SwitchState(CarFSM.State.Steering);
            Debug.Log("Player car switched to Steering mode.");
        }

        if (enemyCarFSM != null)
        {
            enemyCarFSM.SwitchState(CarFSM.State.Steering);
            Debug.Log("Enemy car switched to Steering mode.");
        }

        Debug.Log("Switched all cars to Steering Behavior.");
    }

    public void SwitchToNavMeshMode()
    {
        if (playerCarFSM != null)
        {
            playerCarFSM.SwitchState(CarFSM.State.Navigating);
            Debug.Log("Player car switched to NavMesh mode.");
        }

        if (enemyCarFSM != null)
        {
            enemyCarFSM.SwitchState(CarFSM.State.Navigating);
            Debug.Log("Enemy car switched to NavMesh mode.");
        }

        Debug.Log("Switched all cars to NavMesh Navigation.");
    }

    public void CheckWinner(string carName)
    {
        if (gameFinished) return;

        gameFinished = true;

        if (gameOverPanel != null)
        {
            gameOverPanel.SetActive(true);
            Debug.Log("Game Over panel activated.");
        }

        GameObject car = GameObject.Find(carName);
        if (car != null && car.CompareTag("PlayerCar"))
        {
            if (winUI != null) winUI.SetActive(true);
            Debug.Log($"{carName} (Player) won!");
        }
        else
        {
            if (loseUI != null) loseUI.SetActive(true);
            Debug.Log($"{carName} (Enemy) won!");
        }

        if (playerCarFSM != null) playerCarFSM.SwitchState(CarFSM.State.Finished);
        if (enemyCarFSM != null) enemyCarFSM.SwitchState(CarFSM.State.Finished);
    }

    public void PlayAgain()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        Debug.Log("Restarting game...");
    }

    public void BackToMainMenu()
    {
        SceneManager.LoadScene("MainMenu");
        Debug.Log("Returning to main menu...");
    }
}

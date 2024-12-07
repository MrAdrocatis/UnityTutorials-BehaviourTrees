using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public CarFSM playerCarFSM; // FSM for player car
    public CarFSM enemyCarFSM;  // FSM for enemy car
    public Transform commonTarget; // Target for navigation

    public GameObject winUI;        // Win UI
    public GameObject loseUI;       // Lose UI
    public GameObject gameOverPanel; // Game over panel

    private bool gameFinished = false; // Game finished status

    private void Start()
    {
        // Set Time.timeScale to 0 at start to prevent cars from moving immediately
        Time.timeScale = 0;

        // Set target navigation for all cars
        if (playerCarFSM != null)
        {
            // If your car has NavMesh or Steering, ensure the FSM initializes properly
            // playerCarFSM.SetTarget(commonTarget); // Removed transition AI usage
            Debug.Log("Player car's target set.");
        }

        if (enemyCarFSM != null)
        {
            // Same for enemy car
            // enemyCarFSM.SetTarget(commonTarget); // Removed transition AI usage
            Debug.Log("Enemy car's target set.");
        }
    }

    public void SwitchToNavMeshMode()
    {
        if (playerCarFSM != null) playerCarFSM.SwitchState(CarFSM.State.Navigating);
        if (enemyCarFSM != null) enemyCarFSM.SwitchState(CarFSM.State.Navigating);
        Debug.Log("Switched all cars to NavMesh Navigation.");
    }

    public void SwitchToSteeringMode()
    {
        if (playerCarFSM != null) playerCarFSM.SwitchState(CarFSM.State.Idle); // Assuming Player 2 uses Steering
        if (enemyCarFSM != null) enemyCarFSM.SwitchState(CarFSM.State.Idle); // Assuming Player 2 uses Steering
        Debug.Log("Switched all cars to Steering mode.");
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

    // Function to start the game, can be triggered by a button
    public void StartGame()
    {
        Time.timeScale = 1; // Resume game when start is pressed
        Debug.Log("Game started!");
    }

    public void FastForward()
    {
        Time.timeScale = 5; // Resume game when start is pressed
        Debug.Log("Fast!");
    }

    public void SuperFast()
    {
        Time.timeScale = 20; // Resume game when start is pressed
        Debug.Log("Fast!");
    }

    public void NormalSpeed()
    {
        Time.timeScale = 1; // Resume game when start is pressed
        Debug.Log("Slow!");
    }
}

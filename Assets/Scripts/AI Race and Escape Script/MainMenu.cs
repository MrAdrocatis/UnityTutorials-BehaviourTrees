using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public void PlayGame()
    {
        SceneManager.LoadScene("RaceCar"); // Ganti dengan nama scene gameplay
    }

    public void ExitGame()
    {
        Application.Quit();
        Debug.Log("Exit");
    }
}


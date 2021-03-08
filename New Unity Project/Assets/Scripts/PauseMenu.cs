using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public static bool GameIsPaused = false;

    public GameObject pauseMenuUI;
    public Button resumeButton, restartButton, menuButton, quitButton;

    void Update()
    {

        if (Input.GetKeyDown(KeyCode.Escape) || Input.GetKeyDown(KeyCode.P))
        {
            GameIsPaused = true;
            AudioListener.pause = true;
        }
        
        if (GameIsPaused)
        {
            Pause();
        }
    }
    public void Resume()
    {
        Cursor.lockState = CursorLockMode.Locked;
        pauseMenuUI.SetActive(false);
        AudioListener.pause = false;
        Time.timeScale = 1;
        GameIsPaused = false;
    }
    public void Pause()
    {
        Time.timeScale = 0;
        Cursor.lockState = CursorLockMode.None;
        pauseMenuUI.SetActive(true);
    }

    public void Restart()
    {
        Time.timeScale = 1;
        GameIsPaused = false;
        AudioListener.pause = false;
        pauseMenuUI.SetActive(false);
        SceneManager.LoadScene("Scene");
    }

    public void LoadMenu()
    {
        Time.timeScale = 0;
        GameIsPaused = false;
        AudioListener.pause = false;
        pauseMenuUI.SetActive(false);
        SceneManager.LoadScene("Scene");
    }
    public void QuitGame()
    {
        Application.Quit();
    }
}

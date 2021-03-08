using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameOverMenu : MonoBehaviour
{
    public GameObject gameOverCanvas, winCanvas;
    public Button restartButtonWin, menuButtonWin, quitButtonWin;
    public Button restartButtonLose, menuButtonLose, quitButtonLose;

    void Update()
    {
        if (Player.isDead)
        {
            Cursor.lockState = CursorLockMode.None;
            gameOverCanvas.SetActive(true);
        }
        
        if (Player.isWinner && !Player.isDead)
        {
            Cursor.lockState = CursorLockMode.None;
            winCanvas.SetActive(true);
        }
    }

    public void Restart()
    {
        Time.timeScale = 1;
        Player.isDead = false;
        Player.isWinner = false;
        gameOverCanvas.SetActive(false);
        winCanvas.SetActive(false);
        SceneManager.LoadScene("Scene");
    }

    public void MainMenu()
    {
        Time.timeScale = 0;
        Player.isDead = false;
        Player.isWinner = false;
        gameOverCanvas.SetActive(false);
        winCanvas.SetActive(false);
        SceneManager.LoadScene("Scene");
    }

    public void Quit()
    {
        Application.Quit();
    }
}

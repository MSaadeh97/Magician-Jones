using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuScript : MonoBehaviour
{
    public GameObject MenuCanvas, InfoCanvas;
    public Button playButton, controlsButton, aboutButton, quitButton, closeButton;
    public GameObject aboutText, controlsText;
    public bool menuActive;

    public RestartVariable reset;


    void Start()
    {
        DisplayMenu();
    }

    void Update()
    {
        if(menuActive)
        {
            Time.timeScale = 0;
            reset = FindObjectOfType<RestartVariable>();
        }

        if (reset.restart == true)
        {
            Play();
        }
    }

    public void Play()
    {
        Time.timeScale = 1;
        Close();
        menuActive = false;
        Cursor.lockState = CursorLockMode.Locked;
        MenuCanvas.SetActive(false);
        reset.restart = false;
    }

    public void DisplayMenu()
    {
        menuActive = true;
        MenuCanvas.SetActive(true);
        Cursor.lockState = CursorLockMode.None;
    }

    public void DisplayAbout()
    {
        InfoCanvas.SetActive(true);
        controlsText.SetActive(false);
        aboutText.SetActive(true);
    }


    public void DisplayControls()
    {
        InfoCanvas.SetActive(true);
        aboutText.SetActive(false);
        controlsText.SetActive(true);
    }

    public void Close()
    {
        InfoCanvas.SetActive(false);
        aboutText.SetActive(false);
        controlsText.SetActive(false);
    }

    public void Quit()
    {
        Application.Quit();
    }
}

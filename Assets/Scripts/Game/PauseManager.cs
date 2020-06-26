using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseManager : MonoBehaviour
{
    private bool isPaused;
    public GameObject pausePanel;
    public string startMenu;


    void Start()
    {
        isPaused = false;
    }


    void Update()
    {
        if (Input.GetButtonDown("pause"))
        {
            TogglePause();
        }
    }


    public void TogglePause()
    {
        isPaused = !isPaused;
        if (isPaused)
        {
            pausePanel.SetActive(true);
            Time.timeScale = 0f;
        }
        else
        {
            pausePanel.SetActive(false);
            Time.timeScale = 1f;
        }
    }


    public void QuitToMain()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(startMenu);
    }
}

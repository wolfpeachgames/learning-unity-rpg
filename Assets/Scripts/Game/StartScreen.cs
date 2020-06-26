using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartScreen : MonoBehaviour
{
    public string startingSceneForNewGame;


    public void NewGame()
    {
        SceneManager.LoadScene(startingSceneForNewGame);
    }


    public void Quit()
    {
        Application.Quit();
    }
}

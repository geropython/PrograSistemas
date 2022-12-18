using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneController : MonoBehaviour
{
    public GameObject panel;
    public void RestartScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void MainMenu()
    {
        SceneManager.LoadScene(0);
    }

    public void SecondTutorial()
    {
        //Escape
        Cursor.lockState = CursorLockMode.Locked;
        SceneManager.LoadScene(6);
    }

    public void FirstTutorial()
    {
        //Level1
        Cursor.lockState = CursorLockMode.Locked;
        SceneManager.LoadScene(5);
    }
    public void Level3()
    {
        SceneManager.LoadScene(3);
    }
    public void Level2()
    {
        SceneManager.LoadScene(2);
    }

    public void Level1()
    {
        SceneManager.LoadScene(1);
    }

    public void Controls()
    {
        panel.SetActive(true);
    }
    public void QuitControls()
    {
        panel.SetActive(false);
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}

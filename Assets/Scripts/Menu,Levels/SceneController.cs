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

    public void Continue()
    {
        //Escape
        Cursor.lockState = CursorLockMode.Locked;
        SceneManager.LoadScene(2);
    }

    public void StartGame()
    {
        //Level1
        Cursor.lockState = CursorLockMode.Locked;
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

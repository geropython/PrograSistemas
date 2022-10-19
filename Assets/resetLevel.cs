using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class resetLevel : MonoBehaviour
{ 
    public void ResetScene()
    {
        SceneManager.LoadScene("MainMenu");
    }
}

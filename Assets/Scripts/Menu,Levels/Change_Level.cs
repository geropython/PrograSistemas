using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Change_Level : MonoBehaviour
{
   public void ChangeLevel()
    {
        SceneManager.LoadScene("Escape");
    }
}

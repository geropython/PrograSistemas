using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class deathZone : MonoBehaviour
{
    [SerializeField] GameObject defeatCanvas;
    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.layer == 7)
        {
            defeatCanvas.SetActive(true);
            //Time.timeScale = 0f;
            Cursor.lockState = CursorLockMode.None;
        }
    }
}

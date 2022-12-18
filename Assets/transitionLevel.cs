using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class transitionLevel1 : MonoBehaviour
{
    //Tutorial Level1
        private void OnCollisionEnter(Collision collision)
        {
            if (collision.gameObject.tag == "Player")
            {
                SceneManager.LoadScene(1);
            }
        }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class tutorialSound3 : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {

            soundManager.PlaySound("tutorial3");
        }
    }
}

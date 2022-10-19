using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class tutorialSound1 : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        soundManager.PlaySound("tutorial1");
    }
}

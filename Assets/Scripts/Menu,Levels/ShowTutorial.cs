using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShowTutorial : MonoBehaviour
{
    [SerializeField] private GameObject _showMessage;
    [SerializeField] private GameObject _hiddeMessage = null;


    private void OnTriggerEnter(Collider other)
    {

        if (_hiddeMessage)
        {
        _hiddeMessage.SetActive(false);
        }
        _showMessage.SetActive(true);
        Debug.Log("Hola");
    }
}

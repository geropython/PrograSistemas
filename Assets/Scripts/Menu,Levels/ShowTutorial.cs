using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShowTutorial : MonoBehaviour
{
    [SerializeField] private GameObject _showMessage = null;
    [SerializeField] private GameObject _canvasTutorial = null;

    private void OnTriggerEnter(Collider other)
    {

        foreach (Transform t in _canvasTutorial.transform)
        {
            t.gameObject.SetActive(false);

        }

        if (_showMessage) _showMessage.SetActive(true);
    }
}

using UnityEngine;
using UnityEngine.UI;

public class ScapeLava : MonoBehaviour
{

    [SerializeField] private GameObject _lavaBar;
    [SerializeField] private Text _lavaText;

    private void OnTriggerEnter(Collider other)
    {
        _lavaText.text = "Lograste escapar de lava!";
        _lavaBar.gameObject.SetActive(false);
    }

    private void OnTriggerExit(Collider other)
    {
        _lavaText.gameObject.SetActive(false);
    }
}

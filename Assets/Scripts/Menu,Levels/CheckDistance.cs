using UnityEngine;
using UnityEngine.UI;

public class CheckDistance : MonoBehaviour
{
    [SerializeField] private Transform _checkPoint;
    [SerializeField] private Text _distanceText;

    public float _distance;

    private void Update()
    {
        _distance = (transform.position.y - _checkPoint.transform.position.y) - 1;

        _distanceText.text = "La lava se acerca" + _distance.ToString("F1");
    }

}

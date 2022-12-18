using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveLavePoint : MonoBehaviour
{
    [SerializeField] private Transform _distanceLaveY = null;
    [SerializeField] private Transform _distancePlayerX = null;

    private void Update()
    {

        Debug.Log("lava en Y " + _distanceLaveY.transform.position.y);
        Debug.Log("player en X " + _distancePlayerX.transform.position.x);
        Debug.Log(transform.position);

        Vector3 currentLavePoint = transform.position;
        Vector3 currentPlayerX = _distancePlayerX.transform.position;
        Vector3 currentLaveY = _distanceLaveY.transform.position;

        currentLavePoint += new Vector3 (currentPlayerX.x,0,0);

        currentLavePoint += new Vector3(currentLaveY.y, 0, 0);


    }


    //
    //    Debug.Log(_distanceLave.transform.position.x);
    //    Debug.Log(_distancePlayer.transform.position.x);

    //    lavePosition.x = _distanceLave.transform.position.x;
}

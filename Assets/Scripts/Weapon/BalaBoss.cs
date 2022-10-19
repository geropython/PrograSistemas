using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BalaBoss : MonoBehaviour
{
    // Start is called before the first frame update
    public float cronometro;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        cronometro += 1 * Time.deltaTime;

        if (cronometro > 3)
        {
            gameObject.SetActive(false);
            cronometro = 0;
        }
        transform.Translate(Vector3.forward * 15 * Time.deltaTime);
    }
}

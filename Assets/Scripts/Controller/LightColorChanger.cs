using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LightColorChanger : MonoBehaviour
{
    [SerializeField] private Material material;
    float duration = 1.0f;

    // Start is called before the first frame update
    void Start()
    {
        if (SceneManager.GetActiveScene().name == "Escape")

            material.SetColor("_Color", Color.red);

        else
            material.SetColor("_Color", Color.cyan);
    }

    // Update is called once per frame
    void Update()
    {
        if (SceneManager.GetActiveScene().name == "Escape")
        {
            float lerp = Mathf.PingPong(Time.time, duration) / duration;
            material.color = Color.Lerp(Color.red, Color.black, lerp);
            material.color = Color.Lerp(Color.black, Color.red, lerp);
        }
    }

}
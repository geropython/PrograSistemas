using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyGameObject : MonoBehaviour
{
    [SerializeField] float _maxLifetime;
    float _lifetime;

    void Start()
    {
        _lifetime = 0f;
    }

    // Update is called once per frame
    void Update()
    {
        _lifetime += Time.deltaTime;
        if (_lifetime >= _maxLifetime)
        {
            Destroy(this.gameObject);
        }
    }
}

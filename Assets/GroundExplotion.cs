using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundExplotion : MonoBehaviour
{
    public float delay = 5f;
    public float radius = 15f;
    public float force = 700f;
    float countdown;
    bool hasExploded = false;
    public GameObject explosionEffect;

    void Start()
    {
        countdown = delay;
    }

    // Update is called once per frame
    void Update()
    {
        countdown -= Time.deltaTime;
        if (countdown <= 0f && !hasExploded)
        {
            Explode();
            hasExploded = true;
        }
    }

    public void Explode()
    {
        Instantiate(explosionEffect, transform.position, transform.rotation);
       Collider[] colliders = Physics.OverlapSphere(transform.position, radius);
        foreach (Collider nearbyObject in colliders)
        {
            Rigidbody rb = nearbyObject.GetComponent<Rigidbody>();
            if (rb != null)
            {
                rb.AddExplosionForce(force, transform.position, radius);

            }
        }
        //hacer daño a los enemigos
    }
}
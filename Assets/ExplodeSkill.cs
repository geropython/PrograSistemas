using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplodeSkill : MonoBehaviour
{
    public float radiusExplotion = 5f;
    [SerializeField] public float force;


    void Start()
    {
       
    }

    void Update()
    {
        if (Input.GetKey(KeyCode.Alpha3))
        {
            Explode();
        }
    }

    private void Explode()
    {
        Debug.Log("Boom");
        //Instantiate(, transform.position, transform.rotation);mostrar effecto
        Collider[] colliders = Physics.OverlapSphere(transform.position, radiusExplotion);
        foreach (Collider nearbyObjects in colliders)
        {
            //Force y comprobamos rigidbodys de objetos
            Rigidbody rb = nearbyObjects.GetComponent<Rigidbody>();
            if (rb != null)
            {
                rb.AddExplosionForce(force, transform.position, radiusExplotion);
            }
            //Damage
        }
    }
}
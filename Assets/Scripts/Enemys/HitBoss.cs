using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitBoss : MonoBehaviour
{
    // Start is called before the first frame update
    public int damage;

    void OnTriggerEnter(Collider coll)
    {
       if (coll.CompareTag("Player"))
        {
            coll.GetComponent<Actor>().TakeDamage(damage);
        }
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

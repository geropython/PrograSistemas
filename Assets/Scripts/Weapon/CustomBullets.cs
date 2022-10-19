using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomBullets : MonoBehaviour
{
    //assignables
    public Rigidbody rb;
    public GameObject explosion;
    public LayerMask whatIsEnemy;

    //stats
    [Range(0f, 1f)]
    public float bounciness;
    public bool useGravity;

    //Damage
    public int explosionDamage;
    public float explosionRange;
    public float explosionForce;

    //Lifetime
    public int maxCollisions;
    public float maxLifetime;
    public bool explodeOnTouch = true;

    int collisions;
    PhysicMaterial physics_mat;
    public IDamageable damageable;


    private void Start()
    {
        Setup();
    }

    private void Update()
    {
        //when to boom
        if (collisions > maxCollisions) Explode();

        //lifetime
        maxLifetime -= Time.deltaTime;
        if (maxLifetime <= 0) Explode();
    }

    private void OnCollisionEnter(Collision collision)
    {
        //count collisions
        collisions++;

        //boom if malo + explodeOnTouch si
        if (explodeOnTouch || collision.collider.CompareTag("Player")) Explode();
    }

    private void Explode()
    {
        //boom
        if (explosion != null) Instantiate(explosion, transform.position, Quaternion.identity);

        //check enemies
        Collider[] enemies = Physics.OverlapSphere(transform.position, explosionRange, whatIsEnemy);
        for (int i = 0; i < enemies.Length; i++)
        {
            enemies[i].GetComponent<Actor>()?.TakeDamage(explosionDamage);
            if (enemies[i].GetComponent<Rigidbody>())
                enemies[i].GetComponent<Rigidbody>().AddExplosionForce(explosionForce, transform.position, explosionRange);
        }
            Invoke("Delay", 0.05f);

    }

    private void Delay()
    {
        Destroy(gameObject);
    }

    private void Setup()
    {
        //new physic mat
        physics_mat = new PhysicMaterial();
        physics_mat.bounciness = bounciness;
        physics_mat.frictionCombine = PhysicMaterialCombine.Minimum;
        physics_mat.bounceCombine = PhysicMaterialCombine.Maximum;

        GetComponent<SphereCollider>().material = physics_mat;

        //gravity
        rb.useGravity = useGravity;
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, explosionRange);
    }

    public void TakeDamage(int damage)
    {
        
    }
}

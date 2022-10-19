using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy2_AI : MonoBehaviour
{

    public NavMeshAgent agent;

    public Transform player;

    public LayerMask whatIsGround, whatIsPlayer;

    Bot botReference;

    //patrol
    public Transform[] waypoints;
    int waypointIndex = 0;
    bool waypointSet;

    //attacking
    public float timeBetweenAttacks;
    bool alreadyAttacked;
    public GameObject projectile;

    //states
    public float sightRange, attackRange, sightWhenChasingRange, actualSightRange;
    public bool playerInSightRange, playerInAttackRange;

    //animation
    Animator anim;
    private void Awake()
    {
        player = GameObject.Find("Player").transform;
        agent = GetComponent<NavMeshAgent>();
        anim = GetComponent<Animator>();
        botReference = GetComponent<Bot>();
        actualSightRange = sightRange;
    }

    private void Update()
    {
        //check sight/attack range
        playerInSightRange = Physics.CheckSphere(transform.position, actualSightRange, whatIsPlayer);
        playerInAttackRange = Physics.CheckSphere(transform.position, attackRange, whatIsPlayer);
        //enemyAnim.playerInSightRange = playerInSightRange;
        //enemyAnim.playerInAttackRange = playerInAttackRange;
        if (!botReference.isDead)
        {
            if (!playerInSightRange && !playerInAttackRange) Patroling();
            if (playerInSightRange && !playerInAttackRange) ChasePlayer();
            if (playerInSightRange && playerInAttackRange) AttackPlayer();
        }
        else
        {
            Debug.Log("STOP");
            agent.speed = 0;
            Destroy(GetComponent<Rigidbody>());
        }
    }

    private void Patroling()
    {
        actualSightRange = sightRange;
        //Animation
        anim.SetBool("Walk", true);
        anim.SetBool("Chase", false);
        anim.SetBool("Aim", false);

        agent.SetDestination(waypoints[waypointIndex].position);

        Vector3 distanceToWalkPoint = transform.position - waypoints[waypointIndex].position;

        //walkpoint reached
        if (distanceToWalkPoint.magnitude < 1f)
            waypointIndex++;

        if (waypointIndex > waypoints.Length - 1) waypointIndex = 0;
    }

    private void ChasePlayer()
    {
        actualSightRange = sightWhenChasingRange;

        anim.SetBool("Chase", true);
        anim.SetBool("Walk", false);
        anim.SetBool("Aim", false);

        agent.SetDestination(player.position);
    }
    private void AttackPlayer()
    {
        anim.SetBool("Aim", true);
        anim.SetBool("Chase", false);
        anim.SetBool("Walk", false);

        agent.SetDestination(transform.position);

        transform.LookAt(player);

        if (!alreadyAttacked)
        {
            Rigidbody rb = Instantiate(projectile, transform.position, Quaternion.identity).GetComponent<Rigidbody>();

            /*rb.AddForce(transform.forward * 32f, ForceMode.Impulse);
            rb.AddForce(transform.up * 8f, ForceMode.Impulse);*/
            Vector3 shootDirection = player.transform.position - transform.position;
            rb.velocity = shootDirection * 3.5f;

            alreadyAttacked = true;
            Invoke(nameof(ResetAttack), timeBetweenAttacks);
        }
    }

    private void ResetAttack()
    {
        alreadyAttacked = false;
    }

    private void DestroyEnemy()
    {
        Destroy(gameObject);
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackRange);
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, sightRange);
    }
}

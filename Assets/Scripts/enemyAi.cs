using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class enemyAi : MonoBehaviour
{
    public NavMeshAgent agent;

    public Transform player;

    public LayerMask whatIsGround, whatIsPlayer, whatIsEnemy;

    Bot botReference;

    //patrol
    public Vector3 walkPoint;
    [SerializeField] bool walkPointSet;
    public float walkPointRange;

    //attacking
    public float timeBetweenAttacks;
    bool alreadyAttacked;
    public GameObject projectile;

    //states
    public float sightRange, attackRange, sightWhenChasingRange, actualSightRange, alarmRange;
    public bool playerInSightRange, playerInAttackRange, enemyAlarmRange;


    Animator anim;


    bool forceFollowPlayer;

    private void Awake()
    {
        player = GameObject.Find("Player").transform;
        agent = GetComponent<NavMeshAgent>();
        anim = GetComponent<Animator>();
        botReference = GetComponent<Bot>();
        actualSightRange = sightRange;
    }

    private void Start()
    {
        botReference.OnTakenDamage += TakenDamage;
    }

    private void Update()
    {
        //check sight/attack range
        playerInSightRange = Physics.CheckSphere(transform.position, actualSightRange, whatIsPlayer);
        playerInAttackRange = Physics.CheckSphere(transform.position, attackRange, whatIsPlayer);

        //ampliar rango del chaseplayer
        //pasar los estados en un manager  o una entidad
        //patrolControler vigilantController

        if (!botReference.isDead)
        {
            if (!playerInSightRange && !playerInAttackRange) Patroling();
            if (playerInSightRange && !playerInAttackRange || forceFollowPlayer) ChasePlayer();
            if (playerInSightRange && playerInAttackRange) AttackPlayer();
        }
        else
        {
            Debug.Log("STOP");
            agent.speed = 0;
            //Destroy(GetComponent<Rigidbody>());
        }
    }

    public void FollowPlayer()
    {

    }

    private void Patroling()
    {
        agent.stoppingDistance = 1f;
        actualSightRange = sightRange;
        //Animation
        anim.SetBool("Walk", true);  //CAMBIO
        anim.SetBool("Chase", false);
        anim.SetBool("Aim", false);

        if (!walkPointSet) SearchWalkPoint();

        if (walkPointSet)
            agent.SetDestination(walkPoint);

        Vector3 distanceToWalkPoint = transform.position - walkPoint;

        //walkpoint reached
        if (distanceToWalkPoint.magnitude < 1.4f)
            walkPointSet = false;
    }

    private void SearchWalkPoint()
    {
        float randomZ = Random.Range(-walkPointRange, walkPointRange);
        float randomX = Random.Range(-walkPointRange, walkPointRange);

        walkPoint = new Vector3(transform.position.x + randomX, transform.position.y, transform.position.z + randomZ);

        if (Physics.Raycast(walkPoint, -transform.up, 2f, whatIsGround))
            walkPointSet = true;
    }

    private void TakenDamage()
    {
        forceFollowPlayer = true;
        Debug.Log("damage from enemyai");
        ChasePlayer();
        Invoke(nameof(SetForceFollowPlayer), 2f);
    }

    private void SetForceFollowPlayer()
    {
        forceFollowPlayer = false;
    }

    private void ChasePlayer()
    {
        agent.stoppingDistance = 2.5f;
        actualSightRange = sightWhenChasingRange;

        anim.SetBool("Chase", true);
        anim.SetBool("Walk", false);
        anim.SetBool("Aim", false);

        agent.SetDestination(player.position);

        var enemies = Physics.OverlapSphere(transform.position, alarmRange, whatIsEnemy);
        if (enemies.Length > 0)
        {
            foreach (var enemie in enemies)
            {
                enemie.GetComponent<enemyAi>()?.SetForceFollowPlayer();
            }
        }
    }
    private void TakenDamage()
    {
        forceFollowPlayer = true;
        Debug.Log("damage from enemyai");
        ChasePlayer();
        Invoke(nameof(SetForceFollowPlayer), 2f);
    }

    private void SetForceFollowPlayer()
    {
        forceFollowPlayer = false;
    }
    private void AttackPlayer()
    {
        anim.SetBool("Aim", true);
        anim.SetBool("Chase", false);
        anim.SetBool("Walk", false);

        agent.SetDestination(transform.position);

        Vector3 lookPosition = new Vector3(player.transform.position.x, player.transform.position.y - 0.5f, player.transform.position.z);
        transform.LookAt(lookPosition);

        if (!alreadyAttacked)
        {
            Rigidbody rb = Instantiate(projectile, transform.position, Quaternion.identity).GetComponent<Rigidbody>();

            rb.AddForce(transform.forward * 16f, ForceMode.Impulse);
            rb.AddForce(transform.up * 4f, ForceMode.Impulse);

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
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(walkPoint, 2f);
    }
}
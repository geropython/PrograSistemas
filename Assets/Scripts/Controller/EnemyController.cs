using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(Actor))]
public class EnemyController : MonoBehaviour, IEnemyAI
{
    public NavMeshAgent agent;
    public Bot botReference;
    bool forceFollowPlayer;

    //layers
    public LayerMask whatIsGround;


    //patrol
    public Vector3 walkPoint;
    bool walkPointSet;
    public float walkPointRange;

    //states
    [SerializeField] bool isEnemyPatroling;
    public float actualSightRange;
    public float sightRange;

    // Animation
    Animator anim;


    private void Awake()
    {
        anim = GetComponent<Animator>();
        actualSightRange = sightRange;
    }

    private void Start()
    {
        botReference.OnTakenDamage += TakenDamage;
    }


    private void Update()
    {
        if (isEnemyPatroling)
        {
            //Move();
        }
    }

    public void Execute()
    {
        throw new System.NotImplementedException();
    }

    public void Shoot()
    {
        throw new System.NotImplementedException();
    }

    public void Move(Vector3 direction)
    {
        throw new System.NotImplementedException();
    }

    public void Undo()
    {
        throw new System.NotImplementedException();
    }

    public void Patrol(Vector3 direction)
    {
        throw new System.NotImplementedException();
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
        //ChasePlayer();
        Invoke(nameof(SetForceFollowPlayer), 2f);
    }

    private void SetForceFollowPlayer()
    {
        forceFollowPlayer = false;
    }
}
    
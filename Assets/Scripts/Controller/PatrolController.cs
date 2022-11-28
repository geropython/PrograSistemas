using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


public class PatrolController : MonoBehaviour
{
    // COMMAND
    private CmdMovement _cmdMoveMovement;


    public NavMeshAgent agent;
    Animator anim;

    //patrol
    public Vector3 walkPoint;
    bool walkPointSet;
    public float walkPointRange;

    public LayerMask whatIsGround;

    private void Start()
    {
        _cmdMoveMovement = new CmdMovement(walkPoint);

    }

    public CmdMovement Move() => _cmdMoveMovement;


    private void Patroling()
    {
        if (!walkPointSet) SearchWalkPoint();

        agent.stoppingDistance = 1f;
       // actualSightRange = sightRange; ESTO SE DEBE ACTUALIZAR EN EL ENEMY CONTROLLER
        //Animation
        anim.SetBool("Walk", true);  //CAMBIO
        anim.SetBool("Chase", false);
        anim.SetBool("Aim", false);


        if (walkPointSet)
            Move();
            //agent.SetDestination(walkPoint);

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
}

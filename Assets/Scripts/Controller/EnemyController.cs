using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Actor))]
public class EnemyController : MonoBehaviour, IEnemyAI
{
    [SerializeField] bool isEnemyPatroling;

    /* COMMAND LIST */
    private CmdMovement _cmdMovement;

    private CmdAttack _cmdAttack;

    private void Start()
    {

    }


    private void Update()
    {
        if (isEnemyPatroling)
        {

            Move();
        }
    }

    public void Execute()
    {
        throw new System.NotImplementedException();
    }

    public void Move(Vector3 direction)
    {
        throw new System.NotImplementedException();
    }

    public void Shoot()
    {
        throw new System.NotImplementedException();
    }

    public void Undo()
    {
        throw new System.NotImplementedException();
    }



}
    
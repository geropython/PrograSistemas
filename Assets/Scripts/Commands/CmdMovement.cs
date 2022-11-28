using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


public class CmdMovement : MonoBehaviour, ICommand
{

    public NavMeshAgent agent;

    private Vector3 _direction;
    public CmdMovement(Vector3 direction)
    {
        _direction = direction;
    }

    public void Execute() => Move(_direction);

    public void Undo()
    {
        throw new System.NotImplementedException();
    }

    public void Move(Vector3 direction) => agent.SetDestination(direction);
}

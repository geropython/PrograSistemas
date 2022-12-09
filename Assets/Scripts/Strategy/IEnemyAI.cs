using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IEnemyAI : ICommand
{
    void Patrol(Vector3 direction);
    void Move(Vector3 direction);

    void Shoot();
}
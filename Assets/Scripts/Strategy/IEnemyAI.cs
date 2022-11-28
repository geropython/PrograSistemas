using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IEnemyAI : ICommand
{
    void Move(Vector3 direction);
    void Shoot();
}
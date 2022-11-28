using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName= "ActorStats", menuName = "Stats/ActorStats", order = 0)]
public class ActorStats : ScriptableObject
{
    [SerializeField] private int _maxLife = 100;
    [SerializeField] private int _movementSpeed = 100;


    public float MovementSpeed => _movementSpeed;

    public int MaxLife => _maxLife;


}

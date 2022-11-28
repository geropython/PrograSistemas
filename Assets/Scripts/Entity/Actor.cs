using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Actor : MonoBehaviour, IDamageable
{
    [SerializeField] private ActorStats _actorStats;

    private int _currentLife;

    public int MaxLife => _actorStats.MaxLife;
    public int CurrentLife => _currentLife;

    public float MovementSpeed => _actorStats.MovementSpeed;

    private void Start()
    {
        _currentLife = _actorStats.MaxLife;
    }

    public virtual void TakeDamage(int damage)
    {
        _currentLife -= damage;
        Debug.Log($"{name} Remaining life {_currentLife}");
        soundManager.PlaySound("piu");
        if (_currentLife <= 0)
        {
            //Debug.Log($"{name} DIED! :(");
            Die();
        }
    }

    public virtual void Die()
    {
        Destroy(gameObject);
    }
}

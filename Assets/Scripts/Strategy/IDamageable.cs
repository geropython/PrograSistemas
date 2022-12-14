using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IDamageable
{
    int MaxLife { get; }
    void TakeDamage(int damage);
    void Die();
}

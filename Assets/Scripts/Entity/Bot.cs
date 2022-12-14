using System;
using UnityEngine;
public class Bot : Actor
{
    public Action OnTakenDamage;
    Animator anim;
    public bool isDead = false;

    public void Awake()
    {
        OnTakenDamage = delegate { };
        anim = GetComponent<Animator>();
        isDead = false;
    }

    public override void TakeDamage(int damage)
    {
        base.TakeDamage(damage);
        if (anim != null) anim.SetTrigger("TakeDamage");
        OnTakenDamage.Invoke();
    }
    public override void Die()
    {
        isDead = true;
        Debug.Log("BOT DEAD");
        if (anim != null) anim.SetTrigger("Die");
        Destroy(gameObject, 2f);
    }
}

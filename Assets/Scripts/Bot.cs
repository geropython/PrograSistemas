using UnityEngine;
public class Bot : Actor
{
    Animator anim;
    public bool isDead = false;

    public void Awake()
    {
        anim = GetComponent<Animator>();
        isDead = false;
    }

    public override void TakeDamage(int damage)
    {
        base.TakeDamage(damage);
        if(anim != null) anim.SetTrigger("TakeDamage");
    }
    public override void Die()
    {
        isDead = true;
        Debug.Log("BOT DEAD");
        if (anim != null)  anim.SetTrigger("Die");
        Destroy(gameObject, 2f);
    }
}

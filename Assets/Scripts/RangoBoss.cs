using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangoBoss : MonoBehaviour
{
    public Animator ani;
    public BossScript boss;
    public int melee;

    void OnTriggerEnter(Collider coll)
    {
        if (coll.CompareTag("Player"))
        {
            melee = Random.Range(0, 3);
            switch (melee)
            {
                case 0:
                    ani.SetFloat("Skills", 0.5f);
                    boss.hit_Select = 0;
                    break;
                case 1:
                    ani.SetFloat("Skills", 0.625f);
                    boss.hit_Select = 1;
                    break;
                case 2:
                    ani.SetFloat("Skills", 0.75f);
                    boss.hit_Select = 2;
                    break;
            }
            ani.SetBool("Walk", false);
            ani.SetBool("Run", false);
            ani.SetBool("Attack", true);
            boss.atacando = true;
            GetComponent<CapsuleCollider>().enabled = false;
        }
    }

}

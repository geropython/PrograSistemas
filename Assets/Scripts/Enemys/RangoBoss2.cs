using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangoBoss2 : MonoBehaviour
{
    public Animator ani;
    public BossScript2 boss;
    public int melee;

    void OnTriggerEnter(Collider coll)
    {
        if (coll.CompareTag("Player"))
        {
            /*melee = Random.Range(0, 3);
            switch (melee)
            {
                case 0:*/
                    ani.SetFloat("Skills", 0);
                    boss.hit_Select = 0;
                    /*break;
            }*/
            ani.SetBool("Walk", false);
            ani.SetBool("Run", false);
            ani.SetBool("Attack", true);
            boss.atacando = true;
            GetComponent<CapsuleCollider>().enabled = false;
        }
    }

}

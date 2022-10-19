using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossFightTriggerScript : MonoBehaviour
{
    [SerializeField] GameObject wall;
    [SerializeField] BossScript boss;
    [SerializeField] BossScript2 boss2;
    [SerializeField] GameObject bossLifebar;
    void Start()
    {
        
    }


    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.layer == 8)
        {
            wall.SetActive(true);
            bossLifebar.SetActive(true);
            if (boss != null) boss.startFight = true;
            if (boss2 != null) boss2.startFight = true;
        }
    }
}

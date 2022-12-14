using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossFightTrigger1 : MonoBehaviour
{
    [SerializeField] GameObject wall;
    [SerializeField] BossScript boss;
    [SerializeField] GameObject bossLifebar;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == 8)
        {
            wall.SetActive(true);
            bossLifebar.SetActive(true);
            if (boss != null) boss.startFight = true;
        }
    }
}

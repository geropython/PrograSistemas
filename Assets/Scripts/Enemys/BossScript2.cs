using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BossScript2 : MonoBehaviour
{
    public int rutina;
    public float cronometro;
    public float time_rutinas;
    public Animator ani;
    public Quaternion angulo;
    public float grado;
    public GameObject target;
    public bool atacando;
    public bool isDashing;
    public RangoBoss2 rango;
    public float speed;
    public float dashSpeed;
    public GameObject[] hit;
    public int hit_Select;
    public bool startFight;
    public bool finishedDeathAnim;


    //flamethrower//
    public bool lanza_llamas;
    public List<GameObject> pool = new List<GameObject>();
    public GameObject fire;
    public GameObject cabeza;
    private float cronometro2;

    //jump//
    public float jump_distance;
    public bool direction_skill;

    //fire ball//
    public GameObject fire_ball;
    public GameObject point;
    public List<GameObject> pool2 = new List<GameObject>();

    public int fase = 1;
    public float HP_Min;
    public float HP_Max;
    public Image barra;
    public AudioSource musica;
    public bool muerto;

    public AudioClip stepSound;
    public AudioClip strongStepSound;
    public AudioClip fireballSound;
    public AudioSource audioSource;


    void Start()
    {
        ani = GetComponent<Animator>();
        audioSource = GetComponent<AudioSource>();
        target = GameObject.Find("Player");
        startFight = true; // CAMBIAR A FALSE
        finishedDeathAnim = false;
    }

    public void Comportamiento_Boss()
    {
        if (startFight == true) // CAMBIAR A CONDICION PARA QUE ARRANQUE LA PELEA
        {
            if (target == null) return;
            var lookPos = target.transform.position - transform.position;
            lookPos.y = 0;
            var rotation = Quaternion.LookRotation(lookPos);
            point.transform.LookAt(target.transform.position);
            //musica.enabled = true;

            if (Vector3.Distance(transform.position, target.transform.position) > 1 && !atacando)
            {
                switch (rutina)
                {
                    case 0:
                        //Walk//
                        transform.rotation = Quaternion.RotateTowards(transform.rotation, rotation, 2);
                        ani.SetBool("Walk", true);
                        ani.SetBool("Run", false);

                        if (transform.rotation == rotation)
                        {
                            transform.Translate(Vector3.forward * speed * Time.deltaTime);
                        }

                        ani.SetBool("Attack", false);

                        cronometro += 1 * Time.deltaTime;
                        if (cronometro > time_rutinas)
                        {
                            rutina = Random.Range(0, 3);
                            cronometro = 0;
                        }

                        break;
                    case 1:

                        //run//
                        transform.rotation = Quaternion.RotateTowards(transform.rotation, rotation, 2);
                        ani.SetBool("Walk", false);
                        ani.SetBool("Run", true);

                        if (transform.rotation == rotation)
                        {
                            transform.Translate(Vector3.forward * speed*2 * Time.deltaTime);
                        }

                        ani.SetBool("Attack", false);
                        cronometro += 1 * Time.deltaTime;
                        if (cronometro > 4f)
                        {
                            rutina = Random.Range(1, 3);
                            cronometro = 0;
                        }
                        break;
                    /*case 2:
                        ani.SetBool("Walk", false);
                        ani.SetBool("Run", false);
                        ani.SetBool("Attack", true);
                        ani.SetFloat("Skills", 0.875f);
                        transform.rotation = Quaternion.RotateTowards(transform.rotation, rotation, 2);
                        rango.GetComponent<CapsuleCollider>().enabled = false;
                        break;*/
                    case 2:
                        //jump//
                            jump_distance += 1 * Time.deltaTime;
                            ani.SetBool("Walk", false);
                            ani.SetBool("Run", false);
                            ani.SetBool("Attack", true);
                            ani.SetFloat("Skills", 1);
                            hit_Select = 1;
                            rango.GetComponent<CapsuleCollider>().enabled = false;
                            
                            if (direction_skill)
                            {
                                if(jump_distance < 1f)
                                {
                                    transform.rotation = Quaternion.RotateTowards(transform.rotation, rotation, 2);
                                }

                                transform.Translate(Vector3.forward * (Vector3.Distance(transform.position, target.transform.position) * 4f) * Time.deltaTime);
                            }
                        break;
                    /*case 3:
                        //fireball//
                        if (fase == 2)
                        {
                            ani.SetBool("Walk", false);
                            ani.SetBool("Run", false);
                            ani.SetBool("Attack", true);
                            ani.SetFloat("Skills", 1f);
                            rango.GetComponent<CapsuleCollider>().enabled = false;
                            transform.rotation = Quaternion.RotateTowards(transform.rotation, rotation, 0.5f);
                        }
                        else
                        {
                            rutina = 0;
                            cronometro = 0;
                        }

                        break;*/
                 
                }
            }
        }
    }

    public void Final_ani()
    {
        rutina = 0;
        ani.SetBool("Attack", false);
        atacando = false;
        rango.GetComponent<CapsuleCollider>().enabled = true;
        //lanza_llamas = false;
        jump_distance = 0;
        direction_skill = false;
        Debug.Log("FINISH ATTACK!");

    }

    public void DirectionAttackStart()
    {
        direction_skill = true;
    }

    public void DirectionAttackFinal()
    {
        direction_skill = false;
    }

    public void ColliderWeaponTrue()
    {
        hit[hit_Select].GetComponent<SphereCollider>().enabled = true;
    }

    public void ColliderWeaponFalse()
    {
        hit[hit_Select].GetComponent<SphereCollider>().enabled = false;
    }

    public GameObject GetBala()
    {
        for (int i = 0; i < pool.Count; i++)
        {
            if (!pool[i].activeInHierarchy)
            {
                pool[i].SetActive(true);
                return pool[i];
            }
        }
        GameObject obj = Instantiate(fire, cabeza.transform.position, cabeza.transform.rotation) as GameObject;
        pool.Add(obj);
        return obj;
    }
    public void LanzaLlamasSkill()
    {
        cronometro2 += 1 * Time.deltaTime;
        if (cronometro2 > 0.1f)
        {
            GameObject obj = GetBala();
            obj.transform.position = cabeza.transform.position;
            obj.transform.rotation = cabeza.transform.rotation;
            cronometro2 = 0;
        }
    }

    public void StartFire()
    {
        lanza_llamas = true;
    }
    public void StopFire()
    {
        lanza_llamas = false;
    }

    public void StartDash()
    {
        isDashing = true;
    }

    public void StopDash()
    {
        isDashing = false;
    }

    public GameObject GetFireBall()
    {
        for (int i = 0; i < pool2.Count; i++)
        {
            if (!pool2[i].activeInHierarchy)
            {
                pool2[i].SetActive(true);
                return pool2[i];
            }
        }
        GameObject obj = Instantiate(fire_ball, point.transform.position, point.transform.rotation) as GameObject;
        pool2.Add(obj);
        return obj;
    }

    public void FireBallSkill()
    {
        GameObject obj = Instantiate(fire_ball, point.transform.position, point.transform.rotation);
        //obj.transform.position = point.transform.position;
        //obj.transform.rotation = point.transform.rotation;
        obj.GetComponent<CustomBullets>().rb.velocity = obj.transform.forward * 15f;
        audioSource.PlayOneShot(fireballSound);
    }

    public void DashSkill()
    {
        transform.Translate(Vector3.forward * Vector3.Distance(transform.position, target.transform.position) * dashSpeed  * Time.deltaTime);
    }

    public void Vivo()
    {
        /*if (HP_Min < 500)
        {
            fase = 2;
            time_rutinas = 1;
        }*/

        Comportamiento_Boss();
        if (isDashing)
        {
            DashSkill();
        }

        if (lanza_llamas)
        {
            LanzaLlamasSkill();
        }
    }

    public void PlayStep()
    {
        audioSource.PlayOneShot(stepSound);
    }

    public void PlayStrongStep()
    {
        audioSource.PlayOneShot(strongStepSound);
    }

    public void FinishDeathAnimation()
    {
        finishedDeathAnim = true;
    }
    void Update()
    {
        barra.fillAmount = HP_Min / HP_Max;
        if (HP_Min > 0)
        {
            Vivo();
        }
        else
        {
            if (!muerto)
            {
                ani.SetTrigger("Dead");
                //musica.enabled = false;
                muerto = true;
            }
        }
    }
}
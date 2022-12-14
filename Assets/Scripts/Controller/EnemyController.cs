using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyController : MonoBehaviour
{
    [Header("Nav Mesh")]
    [Space(5)]
    [SerializeField] private NavMeshAgent agent;

    [Space(10)]

    //Enemy Actions
    [Header("Enemy Actions")]
    [Space(5)]
    [SerializeField] private bool _canPatrol = false;
    [SerializeField] private bool _canChase = false;
    [SerializeField] private bool _canAttack = false;
    [SerializeField] private bool _canAlarm = false;
    [Space(10)]

    //Ranges
    [Header("Enemy Range")]
    [Space(5)]
    [SerializeField] public float _sightRange;
    [SerializeField] private float _sightWhenChasingRange;
    [SerializeField] private float _attackRange;
    [SerializeField] private float _alarmRange;
    public float _actualSightRange;
    [Space(10)]

    //Layers
    [Header("Set Layers")]
    [Space(5)]
    [SerializeField] private LayerMask _whatIsGround;
    [SerializeField] private LayerMask _whatIsPlayer;
    [SerializeField] private LayerMask _whatIsEnemy;
    [Space(10)]

    [Header("Player Transform")]
    [Space(5)]
    [SerializeField] private Transform _player;
    [Space(10)]

    [Header("Set Proyectile")]
    [Space(5)]
    [SerializeField] private GameObject _projectile;
    [Space(10)]

    [SerializeField] private float _timerChaseInSeconds = 0f;


    //patrol
    private Vector3 walkPoint;
    bool walkPointSet;
    public float walkPointRange = 5f;
    bool forceFollowPlayer = false;

    //attacking
    [SerializeField] private float timeBetweenAttacks;
    bool alreadyAttacked;

    
    //states
    public bool playerInSightRange, playerInAttackRange, enemyAlarmRange, playerAlarmRange;

    //animation
    Animator anim;
    private Bot botReference;

    private void Awake()
    {
        _player = GameObject.Find("Player").transform;
        agent = GetComponent<NavMeshAgent>();
        anim = GetComponent<Animator>();
        botReference = GetComponent<Bot>();
        _actualSightRange = _sightRange;
    }
    private void Start()
    {
        botReference.OnTakenDamage += TakenDamage;
        agent.speed = 10;
    }

    private void Update()
    {
        playerInSightRange = Physics.CheckSphere(transform.position, _actualSightRange, _whatIsPlayer);
        playerInAttackRange = Physics.CheckSphere(transform.position, _attackRange, _whatIsPlayer);
        enemyAlarmRange = Physics.CheckSphere(transform.position, _alarmRange, _whatIsEnemy);

        if (_timerChaseInSeconds > 0)
        {
            _timerChaseInSeconds -= Time.deltaTime;
            if (_timerChaseInSeconds <= 0) forceFollowPlayer = false;
        }

        if (!botReference.isDead)
        {
            if (_canPatrol) Patroling();
            if (playerInSightRange || forceFollowPlayer) ChasePlayer();
            if (_canAttack) AttackPlayer();
            if (_canAlarm) Alarm();
        }
        else
        {
            agent.speed = 0;
        }
    }


    private void Patroling()
    {
        if (playerInSightRange && playerInAttackRange) return;

        agent.stoppingDistance = 1f;
        _actualSightRange = _sightRange;
        //Animation
        anim.SetBool("Walk", true);  //CAMBIO
        anim.SetBool("Chase", false);
        anim.SetBool("Aim", false);

        if (!walkPointSet) SearchWalkPoint();

        if (walkPointSet)
            agent.SetDestination(walkPoint);

        Vector3 distanceToWalkPoint = transform.position - walkPoint;

        //walkpoint reached
        if (distanceToWalkPoint.magnitude < 0f)
            walkPointSet = false;
    }

    private void SearchWalkPoint()
    {
        Debug.Log("search"+walkPointSet);

        float randomZ = Random.Range(-walkPointRange, walkPointRange);
        float randomX = Random.Range(-walkPointRange, walkPointRange);

        walkPoint = new Vector3(transform.position.x + randomX, transform.position.y, transform.position.z + randomZ);
        Debug.Log(walkPoint);
        if (Physics.Raycast(walkPoint, -transform.up, 2f, _whatIsGround))
            walkPointSet = true;
        Debug.Log(walkPointSet);
    }

    private void ChasePlayer()
    {
        if (!_canChase) return;

        agent.stoppingDistance = 2.5f;
        _actualSightRange = _sightWhenChasingRange;

        anim.SetBool("Chase", true); //CAMBIOS
        anim.SetBool("Walk", false);
        anim.SetBool("Aim", false);

        agent.SetDestination(_player.position);

    }
    private void TakenDamage()
    {
        forceFollowPlayer = true;
        Debug.Log("damage from enemyai");
        ChasePlayer();
        Invoke(nameof(SetForceFollowPlayer), 2f);
    }

    private void SetForceFollowPlayer(bool state = false, float time = 0)
    {
        Debug.Log("Fuerza chase");
        forceFollowPlayer = state;
        _timerChaseInSeconds = time;
    }
    private void AttackPlayer()
    {
        if (!playerInAttackRange) return;
        //Debug.Log("Ataca");

        anim.SetBool("Aim", true); //CAMBIOS
        anim.SetBool("Chase", false);
        anim.SetBool("Walk", false);

        agent.SetDestination(transform.position);

        Vector3 lookPosition = new Vector3(_player.transform.position.x, _player.transform.position.y - 0.5f, _player.transform.position.z);
        transform.LookAt(lookPosition);

        if (!alreadyAttacked)
        {
            Rigidbody rb = Instantiate(_projectile, transform.position, Quaternion.identity).GetComponent<Rigidbody>();

            rb.AddForce(transform.forward * 16f, ForceMode.Impulse);
            rb.AddForce(transform.up * 4f, ForceMode.Impulse);

            alreadyAttacked = true;
            Invoke(nameof(ResetAttack), timeBetweenAttacks);
        }
    }
    private void Alarm()
    {
        if (!playerInSightRange) return;

        Debug.Log("Entro alarm");

        Vector3 lookPosition = new Vector3(_player.transform.position.x, _player.transform.position.y - 0.5f, _player.transform.position.z);
        transform.LookAt(lookPosition);

        var enemies = Physics.OverlapSphere(transform.position, _alarmRange, _whatIsEnemy);
        Debug.Log(enemies.Length);

        if (enemies.Length > 0)
        {
            foreach (var enemie in enemies)
            {
                enemie.GetComponent<EnemyController>()?.SetForceFollowPlayer(true, 3f);
            }
        }

    }
    private void ResetAttack()
    {
        alreadyAttacked = false;
    }

    private void DestroyEnemy()
    {
        Destroy(gameObject);
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, _attackRange);
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, _sightRange);
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, _alarmRange);
    }
}

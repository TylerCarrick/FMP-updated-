using UnityEngine;
using UnityEngine.AI;

public class EnemyPatrol : MonoBehaviour
{
  public NavMeshAgent agent;
  public Animator anim;
  public Transform player;
  public GameObject Head;
  public LayerMask whatIsGround, whatIsPlayer;
  public Animator animAnimator;
    bool Idle;
    States state;

    enum States
    {
        Patrol,
        Chase,
        Attack
    }

  public Vector3 walkPoint;
  bool walkPointSet;
  public float walkPointRange;

  public float timeBetweenAttacks;
  bool alreadyAttacked;

  public float sightRange, attackRange;
  public bool playerInSightRange, playerInAttackRange;

    private void Awake()
    {
        player = GameObject.Find("Player").transform;
        agent = GetComponent<NavMeshAgent>();
        state = States.Patrol;
        animAnimator = GetComponent<Animator>();

    }

    private void Update()
    {
        playerInSightRange = Physics.CheckSphere(transform.position, sightRange, whatIsPlayer);
        playerInAttackRange = Physics.CheckSphere(transform.position, attackRange, whatIsPlayer);
        if (state == States.Patrol)
        {
            DoPatrol();
        }
        if (state == States.Chase)
        {
            DoChase();
        }

        if (state == States.Attack)
        {
            DoAttack();
        }
    }

    void DoAttack()
    {
        AttackPlayer();
        Debug.Log(name);
        if (!playerInAttackRange)
        {
            state = States.Chase;
        }

    }

    void DoChase()
    {
        ChasePlayer();

        if (!playerInSightRange)
        {
            state = States.Patrol;
        }

        if (playerInAttackRange)
        {
            state = States.Attack;
        }
        //check for attack


    }

    void DoPatrol()
    {
        patrolling();

        if( playerInSightRange )
        {
            state = States.Chase;
        }

    }

    /*
   

        if (!playerInSightRange && !playerInAttackRange) patrolling();
        if (playerInSightRange && !playerInAttackRange) ChasePlayer();
        if (playerInSightRange && playerInAttackRange) AttackPlayer();
    }
    */



    private void patrolling()
    {
       
        if (!walkPointSet) SearchWalkPoint();

        if (walkPointSet)
        {
            agent.SetDestination(walkPoint);
        }
        Vector3 distanceToWalkPoint = transform.position - walkPoint;

        if (distanceToWalkPoint.magnitude < 1f)
        {
            walkPointSet = false;
        }

    }

    private void SearchWalkPoint()
    {
        float randomZ = Random.Range(-walkPointRange, walkPointRange);
        float randomX = Random.Range(-walkPointRange, walkPointRange);

        walkPoint = new Vector3(transform.position.x + randomX, transform.position.y, transform.position.z + randomZ);

        if (Physics.Raycast(walkPoint, - transform.up, 2f, whatIsGround))
        {
            walkPointSet = true;
        }
    }

    private void ChasePlayer()
    {
        agent.SetDestination(player.position);
    }

    private void AttackPlayer()
    {
        agent.SetDestination(transform.position);

        FaceTarget();

        GetComponent<Animator>().SetTrigger("Swing");
        print("is attacking");
        if (alreadyAttacked)
        {
            alreadyAttacked = true;
            Invoke(nameof(ResetAttack), timeBetweenAttacks);
            ResetAttack();
            anim.SetBool("Swing", false);
        }
    }
    void FaceTarget()
    {
        Vector3 direction = (player.position - transform.position).normalized;
        Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z));
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * 25);

    }

    private void ResetAttack()
    {
        alreadyAttacked = true;
        
        AttackPlayer();
           
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackRange);
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, sightRange);
    }
}

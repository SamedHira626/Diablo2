using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class MPC : MonoBehaviour
{
    /*[SerializeField] private GameObject destinationPoint;
    private NavMeshAgent _agent;
    // Start is called before the first frame update
    void Start()
    {
        _agent=GetComponent<NavMeshAgent>();
    }

    // Update is called once per frame
    void Update()
    {
        _agent.SetDestination(destinationPoint.transform.position);
    }
    */

    public NavMeshAgent _agent;
    [SerializeField] Transform _player;
    public LayerMask ground, player;

    public Vector3 destinationPoint;
    private bool destinationPointSet;
    public float walkPointRange;

    public float timeBetweenAttacks;
    private bool alreadyAttacked;
    public GameObject sphere;
    public Animator animator;

    public float sightRange, attackRange;
    public bool playerInSightRange, playerInAttackRange;

    private void Awake()
    {
        _agent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();
    }

    private void Update()
    {
        playerInSightRange = Physics.CheckSphere(transform.position, sightRange, player);
        playerInAttackRange = Physics.CheckSphere(transform.position, attackRange, player);

        animator.SetBool("isClosing", false);

        if (!playerInSightRange && !playerInAttackRange)
        {
           Patroling();         
        }

        if (playerInSightRange && playerInAttackRange)
        {
           Attack();


            if(CheckPraying())
            {
                animator.SetBool("isClosing", true);

            }
 

        }

        if (playerInSightRange && !playerInAttackRange)
        {
            Chase();
        }

    }
    void Patroling()
    {
        if (!destinationPointSet)
        {
            SearchWalkPoint();
        }

        if (destinationPointSet)
        {
            _agent.SetDestination(destinationPoint);
            //Debug.Log("Walking towards destination");
        }

        Vector3 distanceToDestinationPoint = transform.position - destinationPoint;
        if (distanceToDestinationPoint.magnitude < 1.0f)
        {
            destinationPointSet = false;
        }
    }

    void SearchWalkPoint()
    {
        float randomX = UnityEngine.Random.Range(-walkPointRange, walkPointRange);
        float randomZ = UnityEngine.Random.Range(-walkPointRange, walkPointRange);

        destinationPoint = new Vector3(transform.position.x + randomX, transform.position.y, transform.position.z + randomZ);

        // Use Raycast to check if the destination point is above the ground
        RaycastHit hit;
        if (Physics.Raycast(destinationPoint, -transform.up, out hit, 1.0f, ground))
        {
            //Debug.Log("Destination point set false because it's not above the ground. Hit object name: " + hit.collider.gameObject.name);
            destinationPointSet = false;
        }
        else
        {
            destinationPointSet = true;
        }
    }

    void Chase()
    {
        _agent.SetDestination(_player.position);

    }
    void Attack()
    {
        _agent.SetDestination(transform.position);
        //transform.LookAt(_player);
        if (!alreadyAttacked)
        {
            alreadyAttacked = true;
        }

    }

    void ResetAttack()
    {
        alreadyAttacked = false;
    }

    bool CheckPraying()
    {
        Vector3 dist = transform.position - _player.position;
        Vector3 range = new Vector3(2, 0, 2);

        if (dist.x < range.x || dist.z < range.z)
        {
            return true;
        }
        return false;
    }

}
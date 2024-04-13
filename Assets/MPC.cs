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
    public bool playerInSightRange;
    public int isDeadCheck;
    public const int HALF_DEAD = 1;
    public const int FULLY_DEAD = 2;

    private void Awake()
    {
        _agent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();
        
        
    }

    private void Update()
    {
        playerInSightRange = Physics.CheckSphere(transform.position, sightRange, player);

        animator.SetBool("isClosing", false);

        if (!playerInSightRange)
        {
             Patroling();         
        }

        if (playerInSightRange)
        {
            Chase();
        }

        CheckDying();
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
        }

        Vector3 distanceToDestinationPoint = transform.position - destinationPoint;
        
        if (distanceToDestinationPoint.magnitude < 3.0f)
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
        

        if (CheckPraying())
        {
            animator.SetBool("isClosing", true);
            transform.LookAt(_player);

            if (HALF_DEAD == _player.GetComponent<CharacterControllerScr>().isDead)
            {   
                Debug.Log("first hit");
                animator.SetBool("getHit",true);
            }
            else if (FULLY_DEAD == _player.GetComponent<CharacterControllerScr>().isDead)
            {
                Debug.Log("Second hit");
                animator.SetBool("isDying", true);
            }
        }

        else
        {
            //transform.LookAt(_player);
             _agent.SetDestination(_player.position);    
        }
    }

    void CheckDying()
    {
        if (FULLY_DEAD == _player.GetComponent<CharacterControllerScr>().isDead)
        {
            StartCoroutine(ResetHittingRoutine());          
        }           
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

    public void Respawn()
    {
        StartCoroutine(ResetHittingRoutine());
    }

    private IEnumerator ResetHittingRoutine()
    {
        yield return new WaitForSeconds(4f);
        Destroy(this.gameObject);
    }

}
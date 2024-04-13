using UnityEngine;
using System.Collections;

public class CharacterControllerScr : MonoBehaviour
{
    public Camera cam;
    public float moveSpeed = 5f;
    public LayerMask clickableLayer;
    public Animator animator;

    private Vector3 destination;
    private bool isMoving;
    public Transform enemy;
    public float attackRange = 1.0f;
    public int isDead;
    public const int FULLY_DEAD = 2;

    void Awake()
    {
        animator = GetComponent<Animator>();
        destination = transform.position; // Başlangıçta karakterin mevcut konumunu hedef olarak ayarla
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = cam.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit, 100, clickableLayer))
            {
                destination = new Vector3(hit.point.x, transform.position.y, hit.point.z); // Y ekseni pozisyonunu koru
                isMoving = true;
                if (hit.transform == enemy && Vector3.Distance(transform.position, enemy.position) <= attackRange)
                {   
                    Debug.Log("hit");
                    animator.SetBool("hitting1",true);
                    transform.LookAt(enemy);
                    isMoving = false;
                    isDead++;
                    ResetHitting();                 
                }
            }
        }

        if (isMoving && Vector3.Distance(transform.position, destination) > 0.1f)
        {
            MovePlayer();
            animator.SetBool("isWalking", true);
        }
        else
        {
            isMoving = false;
            animator.SetBool("isWalking", false);
        }
    }

    void MovePlayer()
    {
        transform.position = Vector3.MoveTowards(transform.position, destination, moveSpeed * Time.deltaTime);
        
        // Karakterin hedefe doğru yumuşak bir dönüş yapmasını sağla
        Vector3 targetDirection = destination - transform.position;
        if (targetDirection != Vector3.zero)
        {
            Quaternion targetRotation = Quaternion.LookRotation(targetDirection);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, moveSpeed * Time.deltaTime);
        }
    }
    public void ResetHitting()
    {
        StartCoroutine(ResetHittingRoutine());
    }

    private IEnumerator ResetHittingRoutine()
    {
        yield return new WaitForSeconds(1.2f);
        animator.SetBool("hitting1", false);
    }

}

//using System.Collections;
//using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class characterControllerScr : MonoBehaviour
{

    public float moveSpeed = 3f; // Karakterin hareket h�z�
    public Animator animator;
    private Rigidbody rb;
    private Vector3 targetPosition; // Hedef konum


    void Start()
    {
        rb = GetComponent<Rigidbody>();
        animator = GetComponent<Animator>();
        targetPosition = new Vector3( 0,0,0);
    }

    void Update()
    {
        // Fare t�klamas� alg�lan�rsa
        if (Input.GetMouseButtonDown(0))
        {
            animator.SetBool("isWalking", true);

            // Fare pozisyonunu 3D d�nya koordinatlar�na d�n��t�r
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            // T�klanan yere ula��p ula�mad���n� kontrol et
            if (Physics.Raycast(ray, out hit))
            {
                // Karakterin hedef konumunu ayarla
                targetPosition = new Vector3(hit.point.x, transform.position.y, hit.point.z);

                // Karakterin y�n�n� hedef noktaya do�ru d�nd�r
                transform.LookAt(targetPosition);
            }
        }
    }

    void FixedUpdate()
    {
        // Hedef konuma do�ru hareket et
        if (Vector3.Distance(transform.position, targetPosition) > 0.1f)
        {      
            Vector3 movement = (targetPosition - transform.position).normalized * moveSpeed * Time.fixedDeltaTime;
            rb.MovePosition(rb.position + movement);
        }

        else
        {
            animator.SetBool("isWalking", false);
        }
    }
}



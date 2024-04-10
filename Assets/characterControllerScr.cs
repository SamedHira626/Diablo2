//using System.Collections;
//using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class characterControllerScr : MonoBehaviour
{
    public float moveSpeed = 5f;
    //public GameObject player;
    private Animator animator;
    private Camera mainCamera;

    void Start()
    {
        animator = GetComponent<Animator>();
        mainCamera = Camera.main;
    }

    void Update()
    {
        if (Input.GetMouseButton(0)) // Left mouse button
        {
           
            Vector3 targetPosition = mainCamera.ScreenToWorldPoint(Input.mousePosition);
            /*targetPosition.y = player.transform.position.y;*/ // Keep z-coordinate same as player
            targetPosition.y = 0;
            Debug.Log("target pos" + targetPosition);

            // Move towards the target position
            //player.transform.position = Vector3.MoveTowards(player.transform.position, targetPosition, moveSpeed * Time.deltaTime);

            transform.position = Vector3.MoveTowards(transform.position, targetPosition, moveSpeed * Time.deltaTime);

            // Calculate movement direction and adjust animation speed
            Vector3 movementDirection = targetPosition - transform.position;
            float movementSpeed = movementDirection.magnitude;
            //animator.SetFloat("Speed", movementSpeed);
            animator.SetBool("isWalking", true);

            Debug.Log("walking..");


            //if (movementDirection != Vector3.zero)
            //{
            //    transform.rotation = Quaternion.LookRotation(Vector3.forward, movementDirection);

            //}
        }
        else
        {
          
            //animator.SetFloat("Speed", 0f);
            animator.SetBool("isWalking", false);
        }
    }
}


//using System.Collections;
//using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class characterControllerScr : MonoBehaviour
{
    //public Camera cam;
    //public NavMeshAgent player;
    //public Animator playerAnimator;
    //public GameObject targetDest;


    //// Start is called before the first frame update
    //void Start()
    //{

    //}

    //// Update is called once per frame
    //void Update()
    //{
    //    Ray ray = cam.Sc

    //}


    public float moveSpeed = 5f;
    public GameObject player;
    private Animator animator;
    private Camera mainCamera;

    void Start()
    {
        animator = player.GetComponent<Animator>();
        mainCamera = Camera.main;
    }

    void Update()
    {
        if (Input.GetMouseButton(0)) // Left mouse button
        {
            // Convert mouse position to world space
            Vector3 targetPosition = mainCamera.ScreenToWorldPoint(Input.mousePosition);
            targetPosition.z = player.transform.position.z; // Keep z-coordinate same as player

            // Move towards the target position
            player.transform.position = Vector3.MoveTowards(player.transform.position, targetPosition, moveSpeed * Time.deltaTime);

            // Calculate movement direction and adjust animation speed
            Vector3 movementDirection = targetPosition - player.transform.position;
            float movementSpeed = movementDirection.magnitude;
            animator.SetFloat("Speed", movementSpeed);

            // Rotate player to face movement direction (optional)
            if (movementDirection != Vector3.zero)
            {
                player.transform.rotation = Quaternion.LookRotation(Vector3.forward, movementDirection);
            }
        }
        else
        {
            // Stop walking animation if not moving
            animator.SetFloat("Speed", 0f);
        }
    }
}


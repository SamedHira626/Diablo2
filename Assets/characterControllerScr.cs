//using System.Collections;
//using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class characterControllerScr : MonoBehaviour
{

    public float moveSpeed = 3f; // Karakterin hareket hýzý
    private Animator animator;
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
        // Fare týklamasý algýlanýrsa
        if (Input.GetMouseButtonDown(0))
        {
            animator.SetBool("isWalking", true);

            // Fare pozisyonunu 3D dünya koordinatlarýna dönüþtür
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            // Týklanan yere ulaþýp ulaþmadýðýný kontrol et
            if (Physics.Raycast(ray, out hit))
            {
                // Karakterin hedef konumunu ayarla
                targetPosition = new Vector3(hit.point.x, transform.position.y, hit.point.z);

                // Karakterin yönünü hedef noktaya doðru döndür
                transform.LookAt(targetPosition);

                Vector3 hitObjectPosition = hit.collider.gameObject.transform.position;
                Debug.Log(hit);
                if (hit.collider.CompareTag("enemy") /*&& CheckClosing(hitObjectPosition)*/)
                {
                    animator.SetBool("hitting1", true);
                    Debug.Log("HIT");
                }
                else
                {
                    animator.SetBool("hitting1", false);
                    Debug.Log("NOT HIT");
                }
            }
        }
    }

    void FixedUpdate()
    {
        // Hedef konuma doðru hareket et
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

    bool CheckClosing(Vector3 hitObjectPosition)
    {
        Vector3 dist = transform.position - hitObjectPosition;
        Vector3 range = new Vector3(2, 0, 0);

        if (dist.x < range.x)
        {
            //animator.SetBool("hitting1", true);

            return true;
        }
        return false;
    }

}



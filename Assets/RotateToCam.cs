using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateToCam : MonoBehaviour
{
    [SerializeField] private GameObject _camera;
    public Transform targetTransform;
    public float headPos;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (targetTransform != null)
        {
            transform.position = new Vector3(targetTransform.position.x, targetTransform.position.y + headPos, targetTransform.position.z);
            transform.SetPositionAndRotation(transform.position, new Quaternion(transform.rotation.x, -_camera.transform.rotation.y, transform.rotation.z, transform.rotation.w));
        }
        else
        {
            Debug.Log("destroying health bar");
            Destroy(this.gameObject);
        }

        //     Vector3 fixedRotation = new Vector3(0f, transform.rotation.eulerAngles.y, 0f);
        //     transform.rotation = Quaternion.Euler(fixedRotation);
        //
    }
}
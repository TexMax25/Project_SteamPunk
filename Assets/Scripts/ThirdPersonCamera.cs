using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThirdPersonCamera : MonoBehaviour
{
    public Transform target;
    public Vector3 offset;
    [Range(0, 1)]public float smoothFactor = 0.1f;
    
    public bool rotationActive;
    public float speedRotationFactor = 5.0f;

    public bool lookAtPlayer = false;
    

    // Start is called before the first frame update
    void Start()
    {
        offset = transform.position - target.position;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (rotationActive)
        {
            Quaternion canTurnAngle = Quaternion.AngleAxis(Input.GetAxis("Mouse X") * speedRotationFactor, Vector3.up);

            offset = canTurnAngle * offset;
        }

        Vector3 newPosition = target.position + offset;

        transform.position = Vector3.Slerp(transform.position, newPosition, smoothFactor);

        if (lookAtPlayer || rotationActive)
        {
            transform.LookAt(target);
        }

        if (Input.GetButton("Fire1"))
        {
            rotationActive = true;
        }
        else
        {
            rotationActive = false;
            transform.LookAt(target);
        }
    }
}

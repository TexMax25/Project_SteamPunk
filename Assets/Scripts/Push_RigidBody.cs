using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Push_RigidBody : MonoBehaviour
{
    public float pushForce = 3.0f;
    private float targetMass;
    
    private void OnControllerColliderHit(ControllerColliderHit hit)
    {
        Rigidbody body = hit.collider.attachedRigidbody;

        if (body == null || body.isKinematic)
        {
            return;
        }

        if (hit.moveDirection.y < -0.3f)
        {
           return; 
        }

        targetMass = body.mass;

        Vector3 pushDirection = new Vector3(hit.moveDirection.x, 0f, hit.moveDirection.z);

        body.velocity = pushDirection * pushForce / targetMass;
    }

}

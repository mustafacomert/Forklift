using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PendulumController : MonoBehaviour
{
    private Rigidbody rb;
    private float rotationSpeed = 30;
    private bool moveForward = true;
    private float maxDistance = 3;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        rb.AddForce((rotationSpeed) * (Vector3.forward + Vector3.up), ForceMode.Impulse);
    }


    private void FixedUpdate()
    {
        //Move();
    }


    private void Move()
    {
        if(moveForward && transform.position.z > maxDistance)
        {
            Debug.Log("back");
            rb.AddForce(rotationSpeed * Vector3.back, ForceMode.Impulse);
        }
        else if(!moveForward && transform.position.z < -maxDistance)
        {
            Debug.Log("forward");
            rb.AddForce(rotationSpeed * Vector3.forward, ForceMode.Impulse);
        }
    }
}

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
        //rb = GetComponent<Rigidbody>();
        //rb.AddForce((rotationSpeed) * (Vector3.forward + Vector3.up), ForceMode.Impulse);
        //Vector3 newPosition = new Vector3(0, 10, 0);
        //transform.position = newPosition;
    }


    private void FixedUpdate()
    {
        float angle = Mathf.Sin(Time.time) * 80; //tweak this to change frequency

        transform.rotation = Quaternion.AngleAxis(angle, Vector3.right);
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

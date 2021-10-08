using System;
using System.Collections;
using UnityEngine;

//make camera follow the boy, with the offset which obtained by scene, before running the game
public class CameraFollow : MonoBehaviour
{
    [SerializeField] private Transform actorT;
    private Vector3 desiredPos;
    private float offsetZ;
    private float smoothSpeed = 10f;
    private Vector3 smoothedPos;


    private void Start()
    {
        desiredPos = transform.position;
        offsetZ = transform.position.z - actorT.position.z;
        offsetZ = Mathf.Abs(offsetZ);
    }


    private void LateUpdate()
    {
        desiredPos.z = actorT.position.z - offsetZ;
        smoothedPos = Vector3.Lerp(transform.position, desiredPos, smoothSpeed * Time.deltaTime);
        transform.position = smoothedPos;
    }
}
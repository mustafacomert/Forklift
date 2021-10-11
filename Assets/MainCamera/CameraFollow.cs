using System;
using System.Collections;
using UnityEngine;

//make camera follow the boy, with the offset which obtained by scene, before running the game
public class CameraFollow : MonoBehaviour
{
    private Transform actorT;
    private Vector3 desiredPos;
    private float offsetX;
    private float offsetZ;
    private float smoothSpeed = 10f;
    private Vector3 smoothedPos;


    private void Start()
    {
        actorT = ForkliftTruckController.Instance.transform;
        desiredPos = transform.position;
        offsetX = transform.position.x - actorT.position.x;
        offsetZ = transform.position.z - actorT.position.z;
        offsetX = Mathf.Abs(offsetX);
        offsetZ = Mathf.Abs(offsetZ);
    }


    private void LateUpdate()
    {
        desiredPos.z = actorT.position.z - offsetZ;
        desiredPos.x = actorT.position.x - offsetX;
        smoothedPos = Vector3.Lerp(transform.position, desiredPos, smoothSpeed * Time.deltaTime);
        transform.position = smoothedPos;
    }
}
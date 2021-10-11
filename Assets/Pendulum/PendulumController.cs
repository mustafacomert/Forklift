using UnityEngine;
using System;

// ? 2017 TheFlyingKeyboard and released under MIT License
// theflyingkeyboard.net

//Rotates object at given speed between given angles

public class PendulumController : MonoBehaviour
{
    [SerializeField] private float rotationSpeed = 30;
    [SerializeField] private float startingAngle = 80;
    [SerializeField] private float endAngle;

    private Vector3 angle;

    private void Awake()
    {
        rotationSpeed = UnityEngine.Random.Range(rotationSpeed - 5, rotationSpeed + 5);
    }
    private void FixedUpdate()
    {
        angle.x = Mathf.PingPong(Time.time * rotationSpeed, endAngle) - startingAngle;
        transform.eulerAngles = angle;
    }
}
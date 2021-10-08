using UnityEngine;
using System;

public class FuelCanController : MonoBehaviour
{
    public static event Action<int> OnFuelCollectedEvent;
    [SerializeField] private int fuelAmount = 10;

    //a fuel can only collide with 
    //forklift truck, this is ensured by collision matrix
    //so, no need to check the tag or whatsoever
    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("trigger with " + other.name);
        OnFuelCollectedEvent?.Invoke(fuelAmount);
        Destroy(gameObject);
    }
}

using UnityEngine;
using System;

public class PendulumHead : MonoBehaviour
{
    public static event Action OnHitForkliftEvent;

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Forklift"))
        {
            OnHitForkliftEvent?.Invoke();
        }
    }
}

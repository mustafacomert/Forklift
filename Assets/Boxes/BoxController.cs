using UnityEngine;
using System;

public class BoxController : MonoBehaviour
{
    public static event Action OnBoxDroppedEvent;
    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.CompareTag(Tags.Ground))
        {
            OnBoxDroppedEvent?.Invoke();
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovePlatform : MonoBehaviour
{
    private Vector3 startPosition;
    private Vector3 endPosition;
    private Rigidbody rBody;

    private HashSet<GameObject> pickedUpObjs;
    
    private enum Direction
    {
        Forward,
        Back
    }

    [SerializeField] private float moveAmount = 10;
    [SerializeField] private int speed = 10;
    [SerializeField] private Direction dir;

    private const float waitSeconds = 2.5f;
    void Start()
    {
        pickedUpObjs = new HashSet<GameObject>();
        rBody = GetComponent<Rigidbody>();
        startPosition = transform.position;
        switch(dir){
            case Direction.Forward:
                endPosition = transform.position + Vector3.forward * moveAmount;
                break;
            case Direction.Back:
                endPosition = transform.position - Vector3.forward * moveAmount;
                break;
        }
        
        StartCoroutine(Vector3LerpCoroutine(gameObject, endPosition, speed));
    }


    private void LateUpdate()
    {
        lastPos = transform.position;
        foreach(GameObject obj in pickedUpObjs)
        {
            obj.transform.position += (lastPos - prevPos);
        }
        prevPos = transform.position;
    }

    void Update()
    {
        if (notOnTarget && rBody.position.z >= endPosition.z)
        {
            notOnTarget = false;
            StartCoroutine(Vector3LerpCoroutine(gameObject, startPosition, speed));
        }
        if (notOnTarget && rBody.position.z <= startPosition.z)
        {
            notOnTarget = false;
            StartCoroutine(Vector3LerpCoroutine(gameObject, endPosition, speed));
        }
    }
    private bool notOnTarget;
    Vector3 prevPos;
    Vector3 lastPos;
    IEnumerator Vector3LerpCoroutine(GameObject obj, Vector3 target, float speed)
    {
        Vector3 startPosition = obj.transform.position;
        float time = 0f;
        yield return new WaitForSeconds(2);

        while (rBody.position != target)
        {
            obj.transform.position = Vector3.Lerp(startPosition, target, 
                                                  (time / Vector3.Distance(startPosition, target)) * speed);
            time += Time.deltaTime;
            yield return null;
        }
        yield return new WaitForSeconds(waitSeconds);
        notOnTarget = true;
    }

    //private void OnCollisionEnter(Collision collision)
    //{
    //    if (collision.gameObject.CompareTag("Forklift"))
    //    {
    //        pickedUpObjs.Add(collision.collider.attachedRigidbody.gameObject);
    //    }
    //}

    //private void OnCollisionExit(Collision collision)
    //{

    //    if (collision.gameObject.CompareTag("Forklift"))
    //    {
    //        pickedUpObjs.Remove(collision.collider.attachedRigidbody.gameObject);
    //    }
    //}
    private void OnTriggerEnter(Collider col)
    {
        GameObject obj = col.attachedRigidbody.gameObject;
        if (obj.CompareTag("Forklift"))
        {
            pickedUpObjs.Add(obj);
        }
    }

    private void OnTriggerExit(Collider col)
    {
        GameObject obj = col.attachedRigidbody.gameObject;
        if (obj.CompareTag("Forklift"))
        {
            pickedUpObjs.Remove(obj);
        }
    }
}
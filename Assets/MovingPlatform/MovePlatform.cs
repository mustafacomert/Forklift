using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovePlatform : MonoBehaviour
{
    [SerializeField] private float moveAmount = 10;
    [SerializeField] private int speed = 10;
    [SerializeField] private Direction dir;
    [SerializeField] private float waitSeconds = 5f;

    private Vector3 startPosition;
    private Vector3 endPosition;
    private Rigidbody rBody;

    private HashSet<GameObject> pickedUpObjs;

    private bool taskFinished;
    private Vector3 prevPos;
    private Vector3 lastPos;

    private enum Direction
    {
        Forward,
        Back
    }

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
        
        StartCoroutine(Vector3LerpCoroutine(endPosition, speed));
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
        if (taskFinished && Vector3.Distance(transform.position, endPosition) == 0)
        {
            taskFinished = false;
            StartCoroutine(Vector3LerpCoroutine(startPosition, speed));
        }
        if (taskFinished && Vector3.Distance(transform.position, startPosition) == 0)
        {

            taskFinished = false;
            StartCoroutine(Vector3LerpCoroutine(endPosition, speed));
        }
    }

    IEnumerator Vector3LerpCoroutine(Vector3 target, float speed)
    {
        Vector3 startPosition = transform.position;
        float time = 0f;
        while (rBody.position != target)
        {
            transform.position = Vector3.Lerp(startPosition, target, 
                                                  (time / Vector3.Distance(startPosition, target)) * speed);
            time += Time.deltaTime;
            yield return null;
        }
        yield return new WaitForSeconds(waitSeconds);
        taskFinished = true;
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
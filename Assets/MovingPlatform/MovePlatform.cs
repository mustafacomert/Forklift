using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovePlatform : MonoBehaviour
{
    public int speed;
    private Vector3 startPosition;
    private Vector3 endPosition;
    private Rigidbody rBody;

    private HashSet<GameObject> pickedUpObjs;

    void Start()
    {
        pickedUpObjs = new HashSet<GameObject>();
        rBody = GetComponent<Rigidbody>();
        startPosition = transform.position;
        endPosition = transform.position + Vector3.forward * 10;
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
        yield return new WaitForSeconds(2);
        notOnTarget = true;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Forklift"))
        {
            pickedUpObjs.Add(collision.collider.attachedRigidbody.gameObject);
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        
        if (collision.gameObject.CompareTag("Forklift"))
        {
            pickedUpObjs.Remove(collision.collider.attachedRigidbody.gameObject);
        }
    }

}
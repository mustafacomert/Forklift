using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovePlatform : MonoBehaviour
{
    public int speed;
    private Vector3 startPosition;
    private Vector3 endPosition;
    private Rigidbody rBody;

    void Start()
    {
        rBody = GetComponent<Rigidbody>();
        startPosition = transform.position;
        endPosition = transform.position + Vector3.forward * 10;
        StartCoroutine(Vector3LerpCoroutine(gameObject, endPosition, speed));
    }


    private void LateUpdate()
    {
        lastPos = transform.position;

        if (catched)
        {
            //rb2.isKinematic = true;
            Debug.Log("wadsad");
            t.position += (lastPos - prevPos);
        }
        prevPos = transform.position;
    }

    void Update()
    {
        if (finished && rBody.position.z >= endPosition.z)
        {
            finished = false;
            StartCoroutine(Vector3LerpCoroutine(gameObject, startPosition, speed));
        }
        if (finished && rBody.position.z <= startPosition.z)
        {
            finished = false;
            StartCoroutine(Vector3LerpCoroutine(gameObject, endPosition, speed));
        }
    }
    private bool finished;
    private bool catched;
    private Transform t;
    Vector3 prevPos;
    Vector3 lastPos;
    Rigidbody rb2;
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
        finished = true;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Forklift"))
        {
            catched = true;
            t = collision.collider.attachedRigidbody.transform;
            rb2 = collision.collider.attachedRigidbody;
            Debug.Log("rb : " + rb2.name);
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.CompareTag("Forklift"))
        {
        }
    }

}
using UnityEngine;
using System.Collections;

public class MovingPlatform : MonoBehaviour
{
    private enum Directions
    {
        Up,
        Down,
        Left,
        Right, 
        Forward, 
        Back
    }

    [SerializeField] private Directions dir;
    [SerializeField] private float moveSpeed = 2f;
    [SerializeField] private float moveAmount = 6.5f;
    private Vector3 startingPos;
    private Rigidbody rb;
    private Vector3 endingPos;
    private Vector3 targetPos;
    private bool isAtStartingPos;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();

        startingPos = transform.position;
        isAtStartingPos = true;

        switch (dir)
        {
            case Directions.Left:
                targetPos = transform.position + Vector3.left * moveAmount;
                break;
            case Directions.Right:
                targetPos = transform.position + Vector3.right * moveAmount;
                break;
            case Directions.Up:
                targetPos = transform.position + Vector3.up * moveAmount;
                break;
            case Directions.Down:
                targetPos = transform.position + Vector3.down * moveAmount;
                break;
            case Directions.Forward:
                targetPos = transform.position + Vector3.forward * moveAmount;
                break;
            case Directions.Back:
                targetPos = transform.position + Vector3.back * moveAmount;
                break;
        }
        endingPos = targetPos;
    }

    private bool catched;
    private Transform t;
    private Vector3 offset;
    private Vector3 targetPos2;
    private float smoothSpeed = 10;
    Vector3 prevPos;
    Vector3 lastPos;
    Rigidbody rb2;


    //private void LateUpdate()
    //{
    //    lastPos = transform.position;
    //    if(catched)
    //    {
    //        //rb2.isKinematic = true;
    //        t.position += (lastPos - prevPos) * Time.deltaTime;
    //    }
    //    prevPos = transform.position;
    //}


    private void FixedUpdate()
    {
        rb.MovePosition(new Vector3(Mathf.Lerp(transform.position.x, targetPos.x, moveSpeed * Time.fixedDeltaTime),
                                    Mathf.Lerp(transform.position.y, targetPos.y, moveSpeed * Time.fixedDeltaTime),
                                    Mathf.Lerp(transform.position.z, targetPos.z, moveSpeed * Time.fixedDeltaTime)));
        //moves obstacle back and forth with the given direction and the given speed
        if (Vector3.Distance(transform.position, targetPos) < 0.1f)
        {
            if(isCoroutineFİnished)
            {
                isCoroutineFİnished = false;
                StartCoroutine(A());
            }
        }
    }


    private bool isCoroutineFİnished = true;
    IEnumerator A()
    {
        yield return new WaitForSeconds(3);
        isAtStartingPos = !isAtStartingPos;
        if (!isAtStartingPos)
            targetPos = startingPos;
        else
            targetPos = endingPos;
        isCoroutineFİnished = true;
    }


    //private void OnCollisionEnter(Collision collision)
    //{
    //    if(collision.gameObject.CompareTag("Forklift"))
    //    {
    //        catched = true;
    //        t = collision.collider.attachedRigidbody.transform;
    //        offset = transform.position - t.position;
    //        Debug.Log("offset : " + offset);
    //        rb2 = collision.collider.attachedRigidbody;
    //        Debug.Log("rb : " + rb2.name);
    //    }
    //}

    //private void OnCollisionExit(Collision collision)
    //{
    //    if (collision.gameObject.CompareTag("Forklift"))
    //    {
    //    }
    //}
}
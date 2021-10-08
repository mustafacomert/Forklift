using UnityEngine;

public class ForkController : MonoBehaviour
{
    private bool isMovingUp;
    private bool isMovingDown;
    private Vector3 highestPos = Vector3.up * 2;
    private float speedTranslate = 1;
    private Vector3 lowestPos = Vector3.zero;
    

    private void Update()
    {
        //GetInput();
    }

    private void FixedUpdate()
    {
        if (isMovingUp)
        {
            transform.localPosition = Vector3.MoveTowards(transform.localPosition, highestPos, speedTranslate * Time.fixedDeltaTime);
        }
        else if (isMovingDown)
        {
            transform.localPosition = Vector3.MoveTowards(transform.localPosition, lowestPos, speedTranslate * Time.fixedDeltaTime);
        }
    }


    //private void GetInput()
    //{
    //    isMovingUp = Input.GetKey(KeyCode.Z);
    //    isMovingDown = Input.GetKey(KeyCode.X);
    //}

    public void UpButton(bool x )
    {
        isMovingUp = x;
    }

    public void DownButton(bool x)
    {
        isMovingDown = x;
    }
}

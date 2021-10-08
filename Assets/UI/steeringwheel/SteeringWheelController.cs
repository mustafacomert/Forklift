using UnityEngine;
using UnityEngine.EventSystems;

public class SteeringWheelController : MonoBehaviour, IDragHandler, IPointerDownHandler, IPointerUpHandler
{
    public static SteeringWheelController Instance { get; private set; }
    public const float MAX_WHEEL_ANGLE = 200f;

    private bool steeringWheelHeld = false;
    private RectTransform wheelImgRT;
    private float currWheelAngle = 0f;
    private float prevWheelAngle = 0f;
    //steering wheel image center in screen coordinates
    private Vector2 center;
    //At what speed will the steering wheel return to angle zero after it is released?
    public float ReleasedSpeed = 300f;
    //will always be between -1 and 1
    public float steeringWheelValue;


    private void Awake()
    {
        Instance = this;
        wheelImgRT = GetComponent<RectTransform>();
    }

    //return: current steering wheel value, value always between -1 and 1
    public float GetSteeringWheelValue()
    {
        return steeringWheelValue;
    }

    void Update()
    {
        //if steering wheel is released and wheel angle isn't in the default angle 
        //revert the steering wheel its default angle which is zero
        if (!steeringWheelHeld && currWheelAngle != 0f)
        {
            float DeltaAngle = ReleasedSpeed * Time.deltaTime;
            //reverted
            if (Mathf.Abs(DeltaAngle) > Mathf.Abs(currWheelAngle))
                currWheelAngle = 0f;
            //revert by decreasing
            else if (currWheelAngle > 0f)
                currWheelAngle -= DeltaAngle;
            //revert by increasing
            else
                currWheelAngle += DeltaAngle;
        }

        wheelImgRT.localEulerAngles = new Vector3(0, 0, -MAX_WHEEL_ANGLE * steeringWheelValue);
        //value will be between -1 and 1
        steeringWheelValue = currWheelAngle / MAX_WHEEL_ANGLE;
    }


    public void OnPointerDown(PointerEventData eventData)
    {
        steeringWheelHeld = true;
        //center of the image in screen coordinates
        center = RectTransformUtility.WorldToScreenPoint(eventData.pressEventCamera, wheelImgRT.position);
        //vector from image center to clicked pixel coordinate
        Vector2 v = eventData.position - center;
        prevWheelAngle = Vector2.Angle(Vector2.up, v);
    }


    public void OnDrag(PointerEventData eventData)
    {
        //min length of vector "v" to change angle
        float minMagnitude = 400;
        //vector from image center to clicked pixel coordinate
        Vector2 v = eventData.position - center;
        float angle = Vector2.Angle(Vector2.up, v);
        if (v.sqrMagnitude >= minMagnitude)
        {
            //eger direksiyon "sag" taraftan tutulur ve saat yonunun tersi
            //istikamette cevrilirse
            //angle "negatif" olur
            //saat yonunde cevrilirse angle pozitif olur 
            if (eventData.position.x > center.x)
            {
                currWheelAngle += angle - prevWheelAngle;
            }
            //eger direksiyon "sol" taraftan tutulur ve saat yonunun tersi
            //istikamette cevrilirse
            //angle "pozitif" olur
            //saat yonunde cevrilirse angle negatif olur 
            else
            {
                currWheelAngle -= angle - prevWheelAngle;
            }
        }
        //don't let the steering wheel turn more than angle of MAX_WHEEL_ANGLE 
        currWheelAngle = Mathf.Clamp(currWheelAngle, -MAX_WHEEL_ANGLE, MAX_WHEEL_ANGLE);
        prevWheelAngle = angle;
    }


    public void OnPointerUp(PointerEventData eventData)
    {
        steeringWheelHeld = false;
        OnDrag(eventData);
    }
}
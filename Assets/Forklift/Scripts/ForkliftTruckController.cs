using UnityEngine;

public class ForkliftTruckController : MonoBehaviour
{
    public static ForkliftTruckController Instance { get; private set; }
    [SerializeField] private bool IS_MOBILE = true;
    private const string HORIZONTAL = "Horizontal"; 
    private const string VERTICAL = "Vertical";
    private const int MAX_WHEEL_ANGLE = 30;

    //they are child of this prefab
    //so drag and drop reference will do the trick
    [SerializeField] private WheelCollider rearRightWheelCol;
    [SerializeField] private WheelCollider frontRightWheelCol;
    [SerializeField] private WheelCollider rearLeftWheelCol;
    [SerializeField] private WheelCollider frontLeftWheelCol;

    [SerializeField] private Transform rearRightWheel;
    [SerializeField] private Transform frontRightWheel;
    [SerializeField] private Transform rearLeftWheel;
    [SerializeField] private Transform frontLeftWheel;

    private float horizontalInput;
    private bool isBraking;
    private bool isGasing;

    private float motorForce = 600;
    private float brakeForce = 1200;

    private Rigidbody rb;

    private SteeringWheelController steeringWheelController;

    private void Awake()
    {
        Instance = this;
        rb = GetComponent<Rigidbody>();
    }
    private void Start()
    {
        steeringWheelController = SteeringWheelController.Instance;
    }

    private void Update()
    {
        GetInput();
    }
    private Vector3 prevPos;
    private Vector3 currPos;
   
    private void FixedUpdate()
    {
        if(isBraking)
        {
            SetBrakeForce(0);
            SetMotorTorque(-motorForce);
          
        }
        else if(isGasing)
        {
            SetBrakeForce(0);
            SetMotorTorque(motorForce);
        }
        else
        {
            SetBrakeForce(brakeForce);
        }
        HandleSteering();
        MoveWheels();
    }


    private void GetInput()
    {
        if(!IS_MOBILE)
        {
            horizontalInput = Input.GetAxis(HORIZONTAL);
            isGasing = Input.GetKey(KeyCode.Y);
            isBraking = Input.GetKey(KeyCode.Space);
        }
        else
        {
            horizontalInput = steeringWheelController.GetSteeringWheelValue();
        }
    }

    private void SetMotorTorque(float motorForce)
    {
        frontRightWheelCol.motorTorque = motorForce;
        frontLeftWheelCol.motorTorque = motorForce;
    }

    private void SetBrakeForce(float brakeForce)
    {
        rearRightWheelCol.brakeTorque = brakeForce;
        frontRightWheelCol.brakeTorque = brakeForce;
        rearLeftWheelCol.brakeTorque = brakeForce;
        frontLeftWheelCol.brakeTorque = brakeForce;
    }

    private void HandleSteering()
    {
        frontRightWheelCol.steerAngle = MAX_WHEEL_ANGLE * horizontalInput;
        frontLeftWheelCol.steerAngle = MAX_WHEEL_ANGLE * horizontalInput;
    }

    private void MoveWheels()
    {
        MoveSingleWheel(rearRightWheelCol, rearRightWheel);
        MoveSingleWheel(frontRightWheelCol, frontRightWheel);
        MoveSingleWheel(rearLeftWheelCol, rearLeftWheel);
        MoveSingleWheel(frontLeftWheelCol, frontLeftWheel);
    }

    private void MoveSingleWheel(WheelCollider wheelCol, Transform wheel)
    {
        Vector3 pos;
        Quaternion rot;
        wheelCol.GetWorldPose(out pos, out rot);
        wheel.position = pos;
        wheel.rotation = rot;
    }

    //gas button onpointerdown and onpointerup event
    public void GasButton(bool isPressed)
    {
        if(IS_MOBILE)
            isGasing = isPressed;
    }
    //brake button onpointerdown and onpointerup event
    public void BrakeButton(bool isPressed)
    {
        if (IS_MOBILE)
            isBraking = isPressed;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwerveDrive : MonoBehaviour
{
    private Vector2 leftStick;
    private Vector2 rightStick;

    public Wheel frontLeftW, frontRightW;
    public Wheel backLeftW, backRightW;

    public Transform robot;
    public float robotRotation;

    public float motorForce;

    public float leftStickX;
    public float leftStickY;

    public float owo = 0.0f;

    private float ToDegrees(float radians)
    {
        return (radians / Mathf.PI) * 180.0f;
    }

    private float ToRadians(float degrees)
    {
        return (degrees / 180.0f) * Mathf.PI;
    }

    public float SimplifyRotation(float rotation)
    {
        while (rotation > 360.0f)
            rotation -= 360.0f;
        while (rotation < 0.0f)
            rotation += 360.0f;

        return rotation;
    }

    public float GetVectorRotation(Vector2 vector)
    {
        return 90 - ToDegrees(Mathf.Atan2(vector.y, vector.x));
    }

    public Vector2 ApplyRobotRotation(Vector2 originalVector)
    {
        float robotRotation = robot.eulerAngles.y;
        float angle = GetVectorRotation(originalVector);
        float magnitude = originalVector.magnitude;

        angle = SimplifyRotation(angle - robotRotation);

        return new Vector2(magnitude * Mathf.Cos(ToRadians(90-angle)), magnitude * Mathf.Sin(ToRadians(90 - angle)));
    }

    public Vector2 GetFrontLeftVector()
    {
        Vector2 turnVector = new Vector2(1.0f, 1.0f);
        turnVector.Normalize();
        turnVector *= rightStick.x;

        return (turnVector + ApplyRobotRotation(leftStick));
    }

    public Vector2 GetFrontRightVector()
    {
        Vector2 turnVector = new Vector2(1.0f, -1.0f);
        turnVector.Normalize();
        turnVector *= rightStick.x;

        return (turnVector + ApplyRobotRotation(leftStick));
    }

    public Vector2 GetBackLeftVector()
    {
        Vector2 turnVector = new Vector2(-1.0f, 1.0f);
        turnVector.Normalize();
        turnVector *= rightStick.x;

        return (turnVector + ApplyRobotRotation(leftStick));
    }

    public Vector2 GetBackRightVector()
    {
        Vector2 turnVector = new Vector2(-1.0f, -1.0f);
        turnVector.Normalize();
        turnVector *= rightStick.x;

        return (turnVector + ApplyRobotRotation(leftStick));
    }

    private void Steer()
    {
        //*
        if (!(leftStick.x == 0.0f && leftStick.y == 0.0f && rightStick.x == 0.0f))
        {
            frontLeftW.targetAngle = GetVectorRotation(GetFrontLeftVector());
            frontRightW.targetAngle = GetVectorRotation(GetFrontRightVector());
            backLeftW.targetAngle = GetVectorRotation(GetBackLeftVector());
            backRightW.targetAngle = GetVectorRotation(GetBackRightVector());
        }
        /*/
        frontLeftW.targetAngle = 45.0f;
        frontRightW.targetAngle = 135.0f;
        backLeftW.targetAngle = 315.0f;
        backRightW.targetAngle = 225.0f;
        //*/
    }

    private void Accelerate()
    {
        frontLeftW.SetTorque(GetFrontLeftVector().magnitude * motorForce);
        frontRightW.SetTorque(GetFrontRightVector().magnitude * motorForce);
        backLeftW.SetTorque(GetBackLeftVector().magnitude * motorForce);
        backRightW.SetTorque(GetBackRightVector().magnitude * motorForce);
    }

    public void GetInput()
    {
        leftStick.Set(Input.GetAxis("LeftHorizontal"), Input.GetAxis("LeftVertical"));
        rightStick.Set(Input.GetAxis("RightHorizontal"), Input.GetAxis("RightVertical"));
    }

    // Start is called before the first frame update
    void Start()
    {
        leftStick = new Vector2(0.0f, 0.0f);
        rightStick = new Vector2(0.0f, 0.0f);
    }

    void FixedUpdate()
    {
        GetInput();
        Steer();
        Accelerate();
        leftStickX = leftStick.x;
        leftStickY = leftStick.y;
        owo = GetVectorRotation(leftStick);
    }
}

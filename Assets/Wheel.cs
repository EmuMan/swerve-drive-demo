using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wheel : MonoBehaviour
{
    public WheelCollider wheelC;
    public Transform wheelT;

    public double rotationSpeed = 10;

    public double targetAngle = 0.0f;

    public double SimplifyAngle(double degrees)
    {
        while (degrees > 360.0f)
            degrees -= 360.0f;
        while (degrees < 0.0f)
            degrees += 360.0f;
        return degrees;
    }

    public double ClosestAngleTransform(double current, double target)
    {
        if (target == current)
            return 0.0f;
        else if (target > current)
        {
            if (target - 180.0f > current)
                current += 360.0f;
            return target - current;
        }
        else
        {
            if (target + 180.0f < current)
                target += 360.0f;
            return target - current;
        }
    }

    public void SetTorque(double torque)
    {
        wheelC.motorTorque = (float)torque;
    }

    void Update()
    {
        double angleTransform = ClosestAngleTransform(wheelC.steerAngle, targetAngle);
        if (Mathf.Abs((float)angleTransform) < rotationSpeed)
            wheelC.steerAngle = (float)SimplifyAngle(wheelC.steerAngle + angleTransform);
        else
            wheelC.steerAngle = (float)SimplifyAngle(wheelC.steerAngle + rotationSpeed * Mathf.Sign((float)angleTransform));
    }

    void FixedUpdate()
    {
        Vector3 pos = wheelT.position;
        Quaternion quat = wheelT.rotation;

        wheelC.GetWorldPose(out pos, out quat);

        wheelT.position = pos;
        wheelT.rotation = quat;
    }
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WheelController : MonoBehaviour {
    //gets all our wheel colliders
    public WheelCollider frontRight;
    public WheelCollider frontLeft;
    public WheelCollider backRight;
    public WheelCollider backLeft;

    public Transform frontRightTransform;
    public Transform frontLeftTransform;
    public Transform backRightTransform;
    public Transform backLeftTransform;

    public float acceleration = 500f;
    public float brakingForce = 300f;
    public float maxTurnAngle = 1f;    

    private float currentAcceleration;
    private float currentBrakeForce;
    private float currentTurnAngle;

    private bool braking = false;
    private float turnInput;
    private float accelerationInput;

    void FixedUpdate() {
        currentAcceleration = acceleration * -accelerationInput;

        if (braking) {
            currentBrakeForce = brakingForce;
        } else {
            currentBrakeForce = 0f;
        }

        frontRight.motorTorque = currentAcceleration;
        frontLeft.motorTorque = currentAcceleration;

        frontRight.brakeTorque = currentBrakeForce;
        frontLeft.brakeTorque = currentBrakeForce;
        backLeft.brakeTorque = currentBrakeForce;
        backRight.brakeTorque = currentBrakeForce;

        currentTurnAngle = maxTurnAngle * turnInput;
        frontLeft.steerAngle = currentTurnAngle;
        frontRight.steerAngle = currentTurnAngle;

        UpdateWheel(frontLeft, frontLeftTransform);
        UpdateWheel(frontRight, frontRightTransform);
        UpdateWheel(backLeft, backLeftTransform);
        UpdateWheel(backRight, backRightTransform);
    }

    void UpdateWheel(WheelCollider col, Transform trans) {
        //set the position of the wheels to match player input
        Vector3 position;
        Quaternion rotation;
        col.GetWorldPose(out position, out rotation);
        trans.position = position;
        trans.rotation = rotation;
    }

    public void setBraking(bool brk) {
        braking = brk;
    }

    public void setTurnInput(float input) {
        turnInput = input;
    }

    public void setAccelerationInput(float input)
    {
        accelerationInput = input;
    }
}

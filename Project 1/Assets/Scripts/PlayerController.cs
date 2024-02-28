using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {
    private Rigidbody rb;
    private Vector3 lastSafePosition;
    private WheelController wheelController;

    public Transform frontRightTransform;
    public Transform backLeftTransform;

    void Start() {
        //find components and set a default return position
        wheelController = this.GetComponent<WheelController>();
        rb = this.GetComponent<Rigidbody>();
        lastSafePosition = this.transform.position;
    }

    void FixedUpdate() {
        //determine if current position is 'safe' and store the value
        if (Physics.Raycast(frontRightTransform.position, Vector3.down, 1f) && Physics.Raycast(backLeftTransform.position, Vector3.down, 1f)) { // FIX THIS
            lastSafePosition = this.transform.position;
        }

        //falling off the map returns you to your last safe location
        if (this.transform.position.y < -5 || Input.GetKeyDown("r")) {
            rb.velocity = new Vector3(0, 0, 0);
            this.transform.eulerAngles = new Vector3(0, this.transform.rotation.y, 0);
            this.transform.position = lastSafePosition;
        }

        //checks for space braking as well as wasd or arrow key input to send to wheel controller
         if (Input.GetKey(KeyCode.Space)) {
            wheelController.setBraking(true);
        } else {
            wheelController.setBraking(false);
        }

        wheelController.setAccelerationInput(Input.GetAxis("Vertical"));
        wheelController.setTurnInput(Input.GetAxis("Horizontal"));
    }
}

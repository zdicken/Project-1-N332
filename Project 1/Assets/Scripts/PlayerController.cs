using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {
    private Rigidbody rb;
    private Vector3 lastSafePosition;

    public float speed;
    public float turnRate;

    void Start() {
        rb = this.GetComponent<Rigidbody>();
        lastSafePosition = this.transform.position;
    }

    void FixedUpdate() {
        //randomly change controls??
        rb.AddRelativeForce(Vector3.forward * -speed * Input.GetAxis("Vertical"));
        //Quaternion target = Quaternion.Euler(transform.rotation.x, transform.rotation.y + turnRate * Input.GetAxis("Horizontal"), transform.rotation.z);
        //transform.rotation = Quaternion.Slerp(transform.rotation, target, 5.0f);
        transform.Rotate(0, turnRate * Input.GetAxis("Horizontal"), 0);

        //determine if current position is 'safe' and store the value
        if(this.transform.position.y < -5) {
            rb.velocity = new Vector3(0, 0, 0);
            this.transform.position = lastSafePosition;
        }
    }
}

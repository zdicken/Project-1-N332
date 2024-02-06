using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {
    private Rigidbody rb;

    public float speed;
    public float turnRate;

    void Start() {
        rb = this.GetComponent<Rigidbody>();
    }

    void FixedUpdate() {
        //randomly change controls??
        rb.AddForce(new Vector3(speed * Input.GetAxis("Vertical"), 0.0f, 0.0f));
        Quaternion target = Quaternion.Euler(0, turnRate * Input.GetAxis("Horizontal"), 0);
        transform.rotation = Quaternion.Slerp(transform.rotation, target, 5.0f);
    }
}

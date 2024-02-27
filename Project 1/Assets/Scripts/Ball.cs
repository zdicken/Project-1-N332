using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour {
    private int timer;
    void FixedUpdate() {
        //increase timer, then destroy it if five seconds have passed
        timer++;
        print(timer);
        if(timer >= 300) {
            Destroy(this.gameObject);
        }
    }
}

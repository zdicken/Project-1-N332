using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Checkpoint : MonoBehaviour {
    private bool passed = false;

    public bool getPassed() {
        return passed;
    }

    public void setPassed(bool set = false) {
        passed = set;
    }

    private void OnTriggerEnter(Collider other) {
        //if this checkpoint is passed by the player, set it to be passed
        if (other.name.Contains("Player")) {
            this.setPassed(true);
        }
    }
}

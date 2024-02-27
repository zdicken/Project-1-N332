using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class aiCheckpoint : MonoBehaviour {
    private void OnTriggerEnter(Collider other) {
        if(other.name.Contains("Enemy")) {
            transform.parent.GetComponent<aiCheckpointManager>().passCheckpoint(this.gameObject);
        }
    }
}

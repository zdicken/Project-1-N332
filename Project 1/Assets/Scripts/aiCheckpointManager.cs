using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class aiCheckpointManager : MonoBehaviour {
    public Transform nextCheckpoint;
    public int lastCheckpoint;

    private int LayerIgnoreRaycast;

    public void Start() {
        LayerIgnoreRaycast = LayerMask.NameToLayer("Ignore Raycast");
        restart();
    }

    public void passCheckpoint(GameObject checkpoint) {
        checkpoint.SetActive(false);
        checkpoint.layer = LayerIgnoreRaycast;
        if (!(lastCheckpoint == transform.childCount)) {
            nextCheckpoint = this.transform.GetChild(lastCheckpoint).gameObject.transform;
            nextCheckpoint.gameObject.SetActive(true);
            nextCheckpoint.gameObject.layer = 0;
        }
        lastCheckpoint++;
    }

    public void canFinish() {
        if (lastCheckpoint - 1 == transform.childCount) {
            GameObject.FindWithTag("Enemy").GetComponent<raceAgent>().crossFinishLine();
        }
    }

    public void restart() {
        nextCheckpoint = this.transform.GetChild(0);
        lastCheckpoint = 0;
        for (int i = 0; i < transform.childCount; i++) {
            transform.GetChild(i).gameObject.SetActive(false);
            transform.GetChild(i).gameObject.layer = LayerIgnoreRaycast;
        }

        transform.GetChild(0).gameObject.SetActive(true);
        transform.GetChild(0).gameObject.layer = 0;
    }
}

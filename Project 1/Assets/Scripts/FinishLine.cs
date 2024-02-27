using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinishLine : MonoBehaviour {
    private GameController gameController;
    public aiCheckpointManager aiCheckpoints;

    void Start() {
        gameController = GameObject.FindWithTag("GameController").GetComponent<GameController>();
    }

    private void OnTriggerEnter(Collider other) {
        if(gameController) {
            gameController.finishLap();
        }

        if(other.name.Contains("Enemy")) {
            aiCheckpoints.canFinish();
        }
    }
}

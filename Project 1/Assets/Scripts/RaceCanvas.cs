using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class RaceCanvas : MonoBehaviour {
    public GameObject time;
    public GameObject lap;

    void Start() {
        //starts the race by finding all the necessary text components and setting the race to started in the game controller
        GameController gameController = GameObject.FindWithTag("GameController").GetComponent<GameController>();
        gameController.timerText = time.GetComponent<TMP_Text>();
        gameController.lapText = lap.GetComponent<TMP_Text>();
        gameController.isRaceRunning = true;
    }
}

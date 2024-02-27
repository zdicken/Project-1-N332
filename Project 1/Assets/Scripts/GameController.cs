using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class GameController : MonoBehaviour {
    public TMP_Text timerText;
    public TMP_Text lapText;
    public bool isRaceRunning = false;
    public static GameController instance;

    private int lap = 1;
    private float secondsCount;
    private int minuteCount;

    void Start() {
        //set the gameController to not be destroyed when switching scenes
        if(instance) {
            Destroy(this);
        } else {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }

    void Update() {
        if (isRaceRunning) {
            //if there is an active race, find the text object and begin updating it with Time
            secondsCount += Time.deltaTime;
            int sec = (int)secondsCount;
            if (sec.ToString().Length < 2) {
                timerText.text = minuteCount + ":0" + (int)secondsCount;
            } else {
                timerText.text = minuteCount + ":" + (int)secondsCount;
            }
            if (secondsCount >= 60) {
                minuteCount++;
                secondsCount = 0;
            }
        }
    }

    public void chooseLevel(string name) {
        SceneManager.LoadScene(name);
        secondsCount = 0;
        minuteCount = 0;
    }

    public void finishLap() {
        GameObject checkpoints = GameObject.FindWithTag("Checkpoints");
        for (int i = 0; i < checkpoints.transform.childCount; i++) {
            if(!checkpoints.transform.GetChild(i).gameObject.GetComponent<Checkpoint>().getPassed()) {
                return;
            }
        }
        lap++;
        lapText.text = "Lap " + lap.ToString();
        if (lap >= 3)
        {
            //finish the race
            isRaceRunning = false;
        }
    }

    public void quitGame() {
        //safely save data before quitting
        Application.Quit();
    }
}

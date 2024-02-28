using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ballSpawner : MonoBehaviour {
    public GameObject ball;
    public Vector3 leftSpawn;

    void FixedUpdate() {
        //randomly spawns balls!
        if(Random.Range(1,100) >= 98) {
            Instantiate(ball, new Vector3(leftSpawn.x + Random.Range(1, 6), leftSpawn.y, leftSpawn.z), Quaternion.identity);
        }
    }
}

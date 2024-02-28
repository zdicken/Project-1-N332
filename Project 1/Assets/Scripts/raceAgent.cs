using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.MLAgents;
using Unity.MLAgents.Actuators;
using Unity.MLAgents.Sensors;

public class raceAgent : Agent {
    private WheelController wheelController;
    private Vector3 lastSafePosition;
    private aiCheckpointManager aiCheckpoints;
    private Rigidbody rb;
    private int stuckTimer = 0;
    private int episodeTimer = 0;

    public override void Initialize() {
        //finds necessary components when first ran and sets an original safe position to return to
        wheelController = GetComponent<WheelController>();
        rb = GetComponent<Rigidbody>();
        lastSafePosition = this.transform.position;
        aiCheckpoints = GameObject.FindWithTag("aiCheckpoints").GetComponent<aiCheckpointManager>();
    }

    public override void OnEpisodeBegin() {
        //restarts the checkpoints and the agent's position
        this.transform.position = lastSafePosition;
        rb.velocity = new Vector3(0, 0, 0);
        this.transform.eulerAngles = new Vector3(0, 90, 0);
        aiCheckpoints.restart();
    }

    //Collecting extra Information that isn't picked up by the RaycastSensors
    public override void CollectObservations(VectorSensor sensor) {
        Vector3 diff = aiCheckpoints.nextCheckpoint.position - transform.position;

        sensor.AddObservation(diff / 20f);

        //constant negative reward to encourage speed
        AddReward(-0.0005f);
    }

    //Processing the actions received
    public override void OnActionReceived(ActionBuffers actions) {
        var input = actions.ContinuousActions;

        //take ai input to send to wheelcontrollerr
        wheelController.setAccelerationInput(input[1]);
        wheelController.setTurnInput(input[0] * 2f);

        if(this.transform.position.y < -5) {
            AddReward(-1f);
            EndEpisode();
        }

        //check if car is stuck AND increment episode timer
        if (rb.transform.InverseTransformDirection(rb.velocity).z < 0.1f && rb.transform.InverseTransformDirection(rb.velocity).z > -0.1f) {
            stuckTimer++;
        } else {
            stuckTimer = 0;
        }
        episodeTimer++;

        //reset if stuck or attempt took too long and make it sad for getting stuck
        if(stuckTimer > 300 || episodeTimer > 30000) {
            stuckTimer = 0;
            episodeTimer = 0;
            AddReward(-1f);
            EndEpisode();
        }
    }

    //For manual testing with human input, the actionsOut defined here will be sent to OnActionRecieved
    public override void Heuristic(in ActionBuffers actionsOut) {
        var action = actionsOut.ContinuousActions;

        action[0] = Input.GetAxis("Horizontal");
        action[1] = Input.GetAxis("Vertical");
    }

    public void incrementCheckpoint() {
         AddReward(0.25f);
    }

    public void crossFinishLine() {
         AddReward(1f);
         EndEpisode();
    }
}
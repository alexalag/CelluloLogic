using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnactiveCelluloBehavior : AgentBehaviour {

    private bool isDrawed;
    private GameObject playerThatDraw;

    void Start(){
        isDrawed = false;
        GameObject[] players;
        players = GameObject.FindGameObjectsWithTag("Player");
        playerThatDraw = players[0];
        // set the colors of the Cellulo
        this.agent.SetVisualEffect(VisualEffect.VisualEffectConstAll, Color.black , 0);
    }

    void Update() {
        if(isDrawed && Input.GetKeyDown(KeyCode.Space)) {
            isDrawed = false;
        } else if(!isDrawed && PlayerIsClosed() && Input.GetKeyDown(KeyCode.Space)) {
            isDrawed = true;
        }
        if(PlayerDistance(playerThatDraw) > ConstantsGame.maxDistDrawCellulo && isDrawed) {
            isDrawed = false;
        }
    }

    public override Steering GetSteering(){
        /*if(!Constants.isGameRun) {
            return new Steering();
        }*/

        Steering steering = new Steering();
        steering.linear = new Vector3(0,0,0);
        Vector3 dist = (playerThatDraw.transform.position - this.transform.position).normalized * agent.maxAccel;

        if(PlayerDistance(playerThatDraw) < ConstantsGame.maxDistDrawCellulo && PlayerDistance(playerThatDraw) > ConstantsGame.maxDistPushCellulo+0.1f && isDrawed) {
            steering.linear = dist;
        }
        GameObject[] players;
        players = GameObject.FindGameObjectsWithTag("Player");
        foreach (GameObject player in players) {
            if(PlayerDistance(playerThatDraw) < ConstantsGame.maxDistPushCellulo) {
                steering.linear += -dist;
            }
        }

        steering.linear = new Vector3(steering.linear.x, 0, steering.linear.z);
        steering.linear = this.transform.parent.TransformDirection(Vector3.ClampMagnitude(steering.linear , agent.maxAccel)) ;
        
        return steering;
    }

    public bool PlayerIsClosed() {
        GameObject[] players;
        players = GameObject.FindGameObjectsWithTag("Player");
        GameObject closest = null;
        float distance = Mathf.Infinity;
        Vector3 position = transform.position;

        foreach (GameObject player in players) {
            Vector3 diff = player.transform.position - position;
            float curDistance = diff.sqrMagnitude;
            if (curDistance < distance) {
                closest = player;
                distance = curDistance;
            }
        }

        if(distance < ConstantsGame.maxDistStartDrawingCellulo) {
            playerThatDraw = closest;
            return true;
        }

        return false;
    }

    public float PlayerDistance(GameObject player) {
        Vector3 position = transform.position;
        Vector3 diff = player.transform.position - position;
        float distance = diff.sqrMagnitude;
        return distance;
    }

    public bool getIsDrawed() {
        return isDrawed;
    }

    public bool IsPlayerThatDraw(GameObject player) {
        return player == playerThatDraw;
    }
}
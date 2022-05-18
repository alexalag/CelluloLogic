using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnactiveCelluloBehavior : AgentBehaviour {

    private bool isDrawed;
    private GameObject playerThatDraw;
    private bool canBeDeplaced;
    public GameObject HelpSignal;

    void Start(){
        isDrawed = false;
        canBeDeplaced = false;
        GameObject[] players;
        players = GameObject.FindGameObjectsWithTag("Player");
        playerThatDraw = players[0];
        // set the colors of the Cellulo
        this.agent.SetVisualEffect(VisualEffect.VisualEffectConstAll, Color.black , 0);
    }

    void Update() {
        if(isDrawed && !Input.GetKey(KeyCode.Space)) {
            DisplayHelpSignal(false);
            isDrawed = false;
        } else if(!isDrawed && PlayerIsClosed()) {
            DisplayHelpSignal(true);
            if(Input.GetKey(KeyCode.Space)) {
                isDrawed = true;
            }
        } else {
            DisplayHelpSignal(false);
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

        if(Input.GetKey(KeyCode.Space)) {
            if(PlayerDistance(playerThatDraw) < ConstantsGame.maxDistDrawCellulo && PlayerDistance(playerThatDraw) > ConstantsGame.maxDistPushCellulo+0.1f && isDrawed) {
                steering.linear = dist;
            }
            if(PlayerDistance(playerThatDraw) < ConstantsGame.maxDistPushCellulo && isDrawed) {
                steering.linear += -dist;
            }
        }
        
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
            canBeDeplaced = true;
            return true;
        }
        canBeDeplaced = false;
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

    public void DisplayHelpSignal(bool display) {
        HelpSignal.SetActive(display);
    }
}
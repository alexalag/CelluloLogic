using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class BotCelluloBehavior : AgentBehaviour
{

    public enum BotType{
        unactiveCellulo = 0, 
        gardian = 1
    }

    public enum Direction{
        HAUT = 0,
        GAUCHE = 1, 
        BAS = 2,
        DROITE = 3,
        AUCUNE = 4
    }

    public BotType type;

    // Pour le gardian
    public Direction direction;
    
    // Pour le unactiveCellulo
    private bool isDrawed;
    private GameObject playerThatDraw;
    private bool canBeDeplaced;
    public GameObject HelpSignal;

    void Start(){
        gameObject.tag = "Bot";

        isDrawed = false;
        canBeDeplaced = false;
        GameObject[] players;
        players = GameObject.FindGameObjectsWithTag("Player");
        playerThatDraw = players[0];

        // set the colors of the Cellulo
        if(type == 0) {
            this.agent.SetVisualEffect(VisualEffect.VisualEffectConstAll, Color.black , 0);
        } else {
            this.agent.SetVisualEffect(VisualEffect.VisualEffectConstAll, Color.red , 0);
        }
        
    }

    void Update() {

        //Unactive Cellulo
        if(type == 0) {
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
        
    }

    public override Steering GetSteering(){
        /*if(!Constants.isGameRun) {
            return new Steering();
        }*/

        Steering steering = new Steering();
        steering.linear = new Vector3(0,0,0);
        Vector3 dist = (playerThatDraw.transform.position - this.transform.position).normalized * agent.maxAccel;

        // cellulo inactif
        if(type == BotType.unactiveCellulo) {
            if(Input.GetKey(KeyCode.Space)) {
                if(PlayerDistance(playerThatDraw) < ConstantsGame.maxDistDrawCellulo && PlayerDistance(playerThatDraw) > ConstantsGame.maxDistPushCellulo+0.06f && isDrawed) {
                    steering.linear = dist;
                    //j'essaye comme je peus de le rendre fluide
                    steering.linear = new Vector3(steering.linear.x, 
                        150f-((float) Math.Pow(((PlayerDistance(playerThatDraw)-ConstantsGame.maxDistPushCellulo+0.06f)/(ConstantsGame.maxDistDrawCellulo-ConstantsGame.maxDistPushCellulo+0.06f)),2f)*130f), 
                        steering.linear.z);
                }
                if(PlayerDistance(playerThatDraw) < ConstantsGame.maxDistPushCellulo && isDrawed) {
                    steering.linear += -dist;
                    steering.linear = new Vector3(steering.linear.x, 120, steering.linear.z);
                }
            }
        }
        
        // Gardian
        if(type == BotType.gardian) {
            Vector3 pos = this.transform.position;
            switch (direction)
            {
                case Direction.DROITE:
                    if(pos[0] < ConstantsGame.maxX) {
                        steering.linear = new Vector3(2f, 0, 0);
                    } else {
                        steering.linear = new Vector3(0, 0, 0);
                        this.direction = Direction.HAUT;
                    }
                    break;
                case Direction.HAUT:
                    if(pos[2] < ConstantsGame.maxZ) {
                        steering.linear = new Vector3(0, 0, 2f);
                    } else {
                        steering.linear = new Vector3(0, 0, 0);
                        this.direction = Direction.GAUCHE;
                    }
                    break;
                case Direction.GAUCHE:
                    if(pos[0] > ConstantsGame.minX) {
                        steering.linear = new Vector3(-2f, 0, 0);
                    } else {
                        steering.linear = new Vector3(0, 0, 0);
                        this.direction = Direction.BAS;
                    }
                    break;
                case Direction.BAS:
                    if(pos[2] > ConstantsGame.minZ) {
                        steering.linear = new Vector3(0, 0, -2f);
                    } else {
                        steering.linear = new Vector3(0, 0, 0);
                        this.direction = Direction.DROITE;
                    }
                    break;
                default:
                    break;
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

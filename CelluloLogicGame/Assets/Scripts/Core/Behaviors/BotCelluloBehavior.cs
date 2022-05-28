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
    public GameObject gameOverMenu;
    private Light lampeTorche;
    
    // Pour le unactiveCellulo
    private bool isDrawed;
    private GameObject playerThatDraw;
    private bool canBeDeplaced;
    public GameObject HelpSignal;

    void Start(){
        gameObject.tag = "Bot";

        GameObject[] players;
        players = GameObject.FindGameObjectsWithTag("Player");
        playerThatDraw = players[0];
        lampeTorche = gameObject.GetComponentInChildren<Light>();

        NewLevel();
    }

    void Update() {

        //Unactive Cellulo
        if(type == BotType.unactiveCellulo) {
            if(isDrawed && !Input.GetKey(KeyCode.Space)) {
                DisplayHelpSignal(false);
                isDrawed = false;
            } else if(!isDrawed && PlayerIsClosed(ConstantsGame.maxDistStartDrawingCellulo)) {
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

        //gardian
        if(type == BotType.gardian)
        {
        }
        
    }

    public override Steering GetSteering(){
        if(!ConstantsGame.gameIsRunning) {
            if (type == BotType.gardian)
            {
                if (PlayerIsVisible())
                {
                    SeTourneVersPlayer();
                }
            }
            return new Steering();
        }

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
            if (PlayerIsVisible())
            {
                SeTourneVersPlayer();
                AttraperPlayer();
            }
            switch (direction)
            {
                case Direction.DROITE:
                    if(pos[0] < ConstantsGame.maxX) {
                        steering.linear = new Vector3(2f, 0, 0);
                    } else {
                        steering.linear = new Vector3(0, 0, 0);
                        if (transform.eulerAngles.y >= 0 && transform.eulerAngles.y <= 350)
                        {
                            transform.Rotate(new Vector3(0, -2f, 0));
                        }
                        else
                        {
                            this.direction = Direction.HAUT;
                        }
                    }
                    break;
                case Direction.HAUT:
                    if(pos[2] < ConstantsGame.maxZ) {
                        steering.linear = new Vector3(0, 0, 2f);
                    } else {
                        steering.linear = new Vector3(0, 0, 0);
                        if (transform.eulerAngles.y >= 270 || transform.eulerAngles.y <= 260)
                        {
                            transform.Rotate(new Vector3(0, -2f, 0));
                        } else
                        {
                            this.direction = Direction.GAUCHE;
                        }
                    }
                    break;
                case Direction.GAUCHE:
                    if(pos[0] > ConstantsGame.minX) {
                        steering.linear = new Vector3(-2f, 0, 0);
                    } else {
                        steering.linear = new Vector3(0, 0, 0);
                        if (transform.eulerAngles.y >= 180 || transform.eulerAngles.y <= 170)
                        {
                            transform.Rotate(new Vector3(0, -2f, 0));
                        }
                        else
                        {
                            this.direction = Direction.BAS;
                        }
                    }
                    break;
                case Direction.BAS:
                    if(pos[2] > ConstantsGame.minZ) {
                        steering.linear = new Vector3(0, 0, -2f);
                    } else {
                        steering.linear = new Vector3(0, 0, 0);
                        if (transform.eulerAngles.y >= 90 || transform.eulerAngles.y <= 80)
                        {
                            transform.Rotate(new Vector3(0, -2f, 0));
                        }
                        else
                        {
                            this.direction = Direction.DROITE;
                        }
                    }
                    break;
                default:
                    break;
            }
        }
        
        steering.linear = this.transform.parent.TransformDirection(Vector3.ClampMagnitude(steering.linear , agent.maxAccel)) ;

        return steering;
    }

    public bool PlayerIsVisible()
    {
        if(PlayerIsClosed(4.5f))
        {
            return true;
        }

        // On récupère tous les joueurs
        GameObject[] players;
        players = GameObject.FindGameObjectsWithTag("Player");

        foreach(GameObject player in players)
        {
            // On récupère des informations sur le positionnement du joueur en fonction de où regarde le bot
            Vector3 dist = (player.transform.position - this.transform.position);
            float angle = (float)Math.Atan((dist.z / dist.x));
            bool memeDirection = (direction == Direction.DROITE && dist.x > 0 && Math.Abs(dist.x) > Math.Abs(dist.z)) ||
                (direction == Direction.HAUT && dist.z > 0 && Math.Abs(dist.x) < Math.Abs(dist.z)) ||
                (direction == Direction.GAUCHE && dist.x < 0 && Math.Abs(dist.x) > Math.Abs(dist.z)) ||
                (direction == Direction.BAS && dist.z < 0 && Math.Abs(dist.x) < Math.Abs(dist.z));

            if (angle <= ConstantsGame.ANGLE / 2 && memeDirection)
            {
                // On lance un rayon pour voir si ca touche le joueur (sinon c'est qu'il y a un mur entre)
                RaycastHit hit;
                Physics.Raycast(this.transform.position, dist, out hit);
                if (hit.transform.tag == "Player") return true;
            }
        }
        return false;
    }

    public void SeTourneVersPlayer()
    {
        direction = Direction.AUCUNE;
        Vector3 playerDirection = (playerThatDraw.transform.position - this.transform.position);
        Quaternion lookRotation = Quaternion.LookRotation(playerDirection);
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * ConstantsGame.rotationSpeed);
    }

    public bool PlayerIsClosed(float dist) {
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

        if(distance < dist) {
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

    public void AttraperPlayer()
    {
        ConstantsGame.gameIsRunning = false;
        gameOverMenu.SetActive(true);
    }

    public void NewLevel()
    {
        isDrawed = false;
        canBeDeplaced = false;

        // set the colors of the Cellulo and the light
        if (type == 0)
        {
            this.agent.SetVisualEffect(VisualEffect.VisualEffectConstAll, Color.black, 0);
            lampeTorche.gameObject.SetActive(false);
        }
        else
        {
            this.agent.SetVisualEffect(VisualEffect.VisualEffectConstAll, Color.red, 0);
            lampeTorche.gameObject.SetActive(true);
        }
    }
}

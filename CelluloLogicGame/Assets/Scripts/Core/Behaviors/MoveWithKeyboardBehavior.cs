using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Input Keys
public enum InputKeyboard{
    arrows =0, 
    wasd = 1
}
public class MoveWithKeyboardBehavior : AgentBehaviour
{
    public string CelluloName;

    public BotCelluloBehavior unactiveCellulo;

    private bool onStone;
    private bool collisionBehavior;
    Steering steering = new Steering();

    private GameManager gameManager;

    public void Start(){
        gameObject.tag = "Player";
        onStone = false;

        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();

        // set the colors of the players
        if (CelluloName == "True") {
            this.agent.SetVisualEffect(VisualEffect.VisualEffectConstAll, Color.magenta, 0);
        } else {
            this.agent.SetVisualEffect(VisualEffect.VisualEffectConstAll, Color.blue, 0);
        }
    }

    public void Update() {
        if((unactiveCellulo.getIsDrawed() && unactiveCellulo.IsPlayerThatDraw(this.gameObject))) {
            onStone = true;
            agent.MoveOnStone();
        } else {
            onStone = false;
            agent.MoveOnIce();
        }

        // set the colors of the players
        if (CelluloName == "True")
        {
            this.agent.SetVisualEffect(VisualEffect.VisualEffectConstAll, Color.magenta, 0);
        }
        else
        {
            this.agent.SetVisualEffect(VisualEffect.VisualEffectConstAll, Color.blue, 0);
        }
    }

    public override Steering GetSteering(){

            if(!ConstantsGame.gameIsRunning) return new Steering();
            InputKeyboard inputKeyboard = CelluloName == "True" ? ConstantsGame.trueInput : ConstantsGame.falseInput;
            float xAxis;
            float zAxis;

            if( inputKeyboard == 0 ) {
                xAxis = Input.GetAxis ("Horizontal") ;
                zAxis = Input.GetAxis ("Vertical") ;
            } else {
                xAxis = Input.GetAxis("HorizontalWASD") ;
                zAxis = Input.GetAxis ("VerticalWASD") ;
            }
        
            Steering steering = new Steering();
            if(onStone) {
                steering.linear = new Vector3(xAxis, 7, zAxis)*agent.maxAccel;
            } else {
                steering.linear = new Vector3(xAxis, 0, zAxis)*agent.maxAccel;
            }

            Vector3 direction = new Vector3(xAxis, 0, zAxis);
            if(unactiveCellulo.type == 0 && unactiveCellulo.getIsDrawed() && unactiveCellulo.IsPlayerThatDraw(this.gameObject))
            {
                direction = unactiveCellulo.gameObject.transform.position - transform.position;
            } 
            if (direction != Vector3.zero)
            {
                Quaternion targetRotation = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z), Vector3.up);
                transform.rotation =
                    Quaternion.RotateTowards(transform.rotation, targetRotation, 700 * Time.deltaTime);
            }
            
            steering.linear = this.transform.parent.TransformDirection(Vector3.ClampMagnitude(steering.linear , agent.maxAccel));
        
        return steering;
    }

    public override void OnCelluloLongTouch(int key)
    {
        if (CelluloName == "True")
        {
            gameManager.longTruePressed();
        }
        else
        {
            gameManager.longFalsePressed();
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        agent.ActivateDirectionalHapticFeedback();
        foreach (ContactPoint contact in collision.contacts)
        GetSteering().linear = collision.contacts[0].normal.normalized*agent.maxAccel;
        collisionBehavior = true; 
    }

    void OnCollisionExit(Collision collision){
        agent.DeActivateDirectionalHapticFeedback();
        collisionBehavior = false; 
    }

}

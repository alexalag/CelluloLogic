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

    public void Start(){
        gameObject.tag = "Player";
        onStone = false;
        
        // set the colors of the players
        if(CelluloName == "True") {
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
        
        steering.linear = this.transform.parent.TransformDirection(Vector3.ClampMagnitude(steering.linear , agent.maxAccel));
        return steering;
    }

}

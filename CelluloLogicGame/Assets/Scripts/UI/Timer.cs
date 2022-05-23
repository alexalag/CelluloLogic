using UnityEngine;
using UnityEngine.UI;

/**
	This class is the implementation of the timer used in the game and how it is handled in it
*/
public class Timer : MonoBehaviour
{
    private float initTimerValue;
    private Text timerText;
    public float maxMinutes = 10f;
    public GameManager gameManager;
    private float time;
    
    public float getTime(){
        return time;
    }

    // Start is called before the first frame update
    public void Start() {
        time = 0f;
        timerText = GetComponent<Text>();
        timerText.text = string.Format("{0:00}:{1:00}", 0, 0);
    }

    // Update is called once per frame
    public void Update() {
        if(ConstantsGame.gameIsRunning) {
            time += Time.deltaTime;
        }
        timerText.text = string.Format("{0:00}:{1:00}", (int) time/60, (int) time%60);
        if(time >= maxMinutes*60) gameManager.EndLevel();
    }
}

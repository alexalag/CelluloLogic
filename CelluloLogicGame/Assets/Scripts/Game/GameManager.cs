using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    private bool Lv1Complete = false;
    private int currentLevel = 1;
    public Text scoreText;
    public Text endText;
    private int score;
    public Timer timer;
    public GameObject menuLevelFinish;
    public GameObject pauseButton;

    // Start is called before the first frame update
    void Start()
    {
        score = 0;
    }

    // Update is called once per frame
    void Update()
    {
                
    }

    public void StartGame() {
        ConstantsGame.gameIsRunning = true;
    }

    public void Pause() {
        ConstantsGame.gameIsRunning = false;
    }

    public void EndLevel() {
        ConstantsGame.gameIsRunning = false;
        menuLevelFinish.SetActive(true);
        pauseButton.SetActive(false);
        
        if(timer.getTime() > timer.maxMinutes){
            score = 0;
            endText.text = "You were too slow... Try again";
        } else {
            score = updateScore();
            endText.text = "Well done...Level completed !";
        }

        scoreText.text = string.Format("{0} pts", score);
        
    }

    private int updateScore(){
        float mult_factor;
        int levelScore;
        float levelTime;
    
        switch(currentLevel){   // j'ai fait un case pour que ce soit plus facile de changer en fonction des niveaux
            case 1: 
                mult_factor = 0.75f;
                levelScore = ConstantsGame.level1Score;
                levelTime = ConstantsGame.level1Time;
                break;
            default:
                mult_factor = 0;
                levelScore = 0;
                levelTime = 0;
                break;
        }
        
        int malus = Mathf.Min(0, (int)((levelTime - timer.getTime())*mult_factor));
        return levelScore + malus;
    }
}


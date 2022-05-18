using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour
{
    private bool Lv1Complete = false;
    private int currentLevel = 1;
    public GameObject menuLevelFinish;
    public GameObject pauseButton;

    // Start is called before the first frame update
    void Start()
    {
        
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
    }
}

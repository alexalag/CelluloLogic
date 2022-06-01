using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    //private bool Lv1Complete = false; => on en a pas besoin normalement
    public Text scoreText;
    public Text endText;
    private int score;
    public Timer timer;
    public GameObject menuLevelFinish;
    public GameObject pauseButton;

    //levels
    private int currentLevel;
    public List<GameObject> levels;

    //camera
    private const float speedCamera = 0.05f;
    private Vector3 level1posCamera;
    private Vector3 intervalInterLevel = new Vector3(28f, 0, 0);

    //Bot
    public BotCelluloBehavior botBehavior;
    public List<Vector3> startPosBotPerLevel;
    public List<BotCelluloBehavior.BotType> botTypePerLevel;

    //Map Cellulo Manager
    public GameObject mapCelluloManager;

    //long touch
    private bool longTrue;
    private bool longFalse;
    public GameObject startMenu;

    public int CurrentLevel { get => currentLevel; }

    // Start is called before the first frame update
    void Start()
    {
        longTrue = false;
        longFalse = false;
        currentLevel = 1;
        score = 0;
        level1posCamera = Camera.main.transform.position;
        // initialisation du bot
        botBehavior.gameObject.transform.position = startPosBotPerLevel[0];
        botBehavior.type = botTypePerLevel[0];
    }

    // Update is called once per frame
    void Update()
    {
        // la camera change de place toute seule à chaque level
        Camera.main.transform.position = Vector3.Lerp(Camera.main.transform.position, level1posCamera + (currentLevel-1) * intervalInterLevel, speedCamera);
        
        // long press pour démarer le jeu
        if(longTrue && longFalse && !ConstantsGame.gameIsRunning)
        {
            StartGame();
            longFalse = false;
            longTrue = false;
        } 
    }

    public void StartGame() {
        startMenu.SetActive(false);
        pauseButton.SetActive(true);
        ConstantsGame.gameIsRunning = true;
    }

    public void Pause() {
        ConstantsGame.gameIsRunning = false;
    }

    public void EndLevel() {
        ConstantsGame.gameIsRunning = false;
        menuLevelFinish.SetActive(true);
        pauseButton.SetActive(false);
        
        if(timer.getTime() > timer.maxMinutes*60){
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

    public void NextLevel()
    {
        menuLevelFinish.SetActive(false);

        // si on a pas encore fait tous les levels
        if(currentLevel < levels.Count)
        {
            ++currentLevel;

            // On désactive le level précédent et active le nouveau level
            levels[currentLevel - 2].SetActive(false);
            levels[currentLevel - 1].SetActive(true);

            // On met le bot à la bonne position et avec le bon type
            botBehavior.gameObject.transform.position = startPosBotPerLevel[currentLevel - 1];
            botBehavior.type = botTypePerLevel[currentLevel - 1];
            botBehavior.NewLevel();

            // On change la position de map Cellulo Manager pour pas que les cellulos sortent de la map
            mapCelluloManager.transform.Translate(intervalInterLevel);
            GameObject[] players;
            players = GameObject.FindGameObjectsWithTag("Player");
            foreach(GameObject player in players)
            {
                player.transform.Translate(new Vector3(0, 0, -28));
            }
            botBehavior.transform.Translate(-intervalInterLevel);

            // On réinitialise le timer
            timer.InitTimer();

            // On relance le jeu
            ConstantsGame.gameIsRunning = true;
        }

    }

    public void longTruePressed()
    {
        longTrue = true;
    }

    public void longFalsePressed()
    {
        longFalse = true;
    }
}


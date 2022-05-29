using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class DialogueBox : MonoBehaviour
{
    // Tous les différents textes qui peuvent apparaitre
    public List<string> texts;

    // Le prochain texte qui va être lu
    private int nextText;

    public GameObject TrueBox;
    public GameObject FalseBox;

    private bool talking;

    // game manager
    private GameManager gameManager;

    // Start is called before the first frame update
    void Start()
    {
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        nextText = 0;
        talking = false;
    }

    // Update is called once per frame
    void Update()
    {
        if(gameManager.CurrentLevel == 1)
        {
            if(nextText == 0)
            {
                (bool, string) isDial1 = Level1Dialog1();
                if (isDial1.Item1)
                {
                    ReadNextText("True" == isDial1.Item2);
                }
            }
            
        }
        if(gameManager.CurrentLevel == 2)
        {
            if (nextText < 2) nextText = 2;
            if(nextText == 2)
            {
                if(Level2Dialog1())
                {
                    ReadNextText(true);
                }
            }
        }
    }

    public void ReadNextText(bool isTrueTalking)
    {
        if (nextText >= texts.Count || talking) return;
        talking = true;
        if (isTrueTalking)
        {
            TrueBox.GetComponentInChildren<TextMeshProUGUI>().text = texts[nextText];
            TrueBox.SetActive(true);
            ++nextText;
        } else
        {
            ++nextText;
            FalseBox.GetComponentInChildren<TextMeshProUGUI>().text = texts[nextText];
            FalseBox.SetActive(true);
        }
        ConstantsGame.gameIsRunning = false;
    }

    public void CloseDialogBox()
    {
        if (!talking) return;
        talking = false;
        TrueBox.SetActive(false);
        FalseBox.SetActive(false);
        ++nextText;
        ConstantsGame.gameIsRunning = true;
    }

    private (bool, string) Level1Dialog1()
    {
        GameObject[] players;
        players = GameObject.FindGameObjectsWithTag("Player");
        GameObject bot = GameObject.FindGameObjectWithTag("Bot");
        Vector3 botPos = bot.transform.position;
        foreach (GameObject player in players)
        {
            RaycastHit hit;
            Vector3 playerPos = player.transform.position;
            Physics.Raycast(playerPos, botPos - playerPos, out hit);
            if (!(hit.transform == null || !hit.transform.CompareTag("Bot")))
            {
                return (true, player.GetComponent<MoveWithKeyboardBehavior>().CelluloName);
            }
        }
        return (false, "");
    }

    private bool Level2Dialog1()
    {
        FilsBehavior fil = GameObject.Find("Fil23").GetComponent<FilsBehavior>();
        return !fil.allume;
    }
}

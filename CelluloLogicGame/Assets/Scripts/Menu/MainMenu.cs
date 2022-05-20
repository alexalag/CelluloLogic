using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour {
    public Animator animator;
    public AudioSource audioSource;
    public AudioClip audioClip;
    public Toggle arrows1;
    public Toggle wasd1;
    public Toggle arrows2;
    public Toggle wasd2;
    private bool gameQuit;

    void Start()
    {
        Invoke("Flickering", 5f);
        gameQuit = false;
    }

    public void PlayGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex+ 1);
    }
    public void QuitGame()
    {
        animator.SetTrigger("Quit");
        gameQuit = true;
        //Debug.Log("Quit!");
        //Application.Quit();
    }
    public void BackToMenu()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 1);
    }

    public void Flickering() {
        if(!gameQuit) {
            audioSource.clip = audioClip;
            audioSource.Play();
            animator.SetTrigger("Clignoti");
            Invoke("Flickering", 10f);
        }
    }

    public void ArrowsInputPlayer1(bool isOn) {
        if(isOn) {
            ConstantsGame.trueInput = InputKeyboard.arrows;
            if(arrows2.isOn) arrows2.isOn = false;
            if(wasd1.isOn) wasd1.isOn = false;
            if(!wasd2.isOn) wasd2.isOn = true;  
        }
    }

    public void WASDInputPlayer1(bool isOn) {
        if(isOn) {
            ConstantsGame.trueInput = InputKeyboard.wasd;
            if(!arrows2.isOn) arrows2.isOn = true;
            if(arrows1.isOn) arrows1.isOn = false;
            if(wasd2.isOn) wasd2.isOn = false; 
        }
    }

    public void ArrowsInputPlayer2(bool isOn) {
        if(isOn) {
            ConstantsGame.falseInput = InputKeyboard.arrows;
            if(arrows1.isOn) arrows1.isOn = false;
            if(wasd2.isOn) wasd2.isOn = false;
            if(!wasd1.isOn) wasd1.isOn = true; 
        }
    }

    public void WASDInputPlayer2(bool isOn) {
        if(isOn) {
            ConstantsGame.falseInput = InputKeyboard.wasd;
            if(!arrows1.isOn) arrows1.isOn = true;
            if(arrows2.isOn) arrows2.isOn = false;
            if(wasd1.isOn) wasd1.isOn = false;
        }
    }
}

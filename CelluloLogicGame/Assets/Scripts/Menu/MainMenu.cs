using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public Animator animator;
    public AudioSource audioSource;
    public AudioClip audioClip;

    void Start()
    {
        Invoke("Flickering", 5f);
    }

    public void PlayGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex+ 1);
    }
    public void QuitGame()
    {
        Debug.Log("Quit!");
        Application.Quit();
    }
    public void BackToMenu()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 1);
    }

    public void Flickering() {
        audioSource.clip = audioClip;
        audioSource.Play();
        animator.SetTrigger("Clignoti");
        Invoke("Flickering", 10f);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class OnClick : MonoBehaviour
{

    public List<GameObject> stories;
    public List<GameObject> images;
    private int currentStory = 0;
    public List<Animator> animators;
    private int currentAnimator = 0;

    // Attention : stories.Count == images.Count !
    void Start() {
        Debug.Assert(stories.Count == images.Count);
    }

    // Permet d'afficher le texte d'un seul coup et donc d'annuler l'animation en cliquant sur l'écran
    public void Click() {
        if(ConstantsUI.writing) {
            ConstantsUI.writing = false;
        } 
    }

    // Permet de passer au prochain morceau de l'histoire
    public void Next() {
        if(currentStory < images.Count && 
            (currentStory == images.Count-1 || images[currentStory] != images[currentStory+1])) {
            animators[currentAnimator].SetBool("Finish", true);
            ++currentAnimator;
            Invoke("NextChangeImage", 1f);
        } else {
            if(currentStory < stories.Count) {
                stories[currentStory].SetActive(false);
            }
            ++currentStory; 
            if(currentStory < stories.Count) {
                stories[currentStory].SetActive(true);
            }
        }
        
    }

    // Permet de passer directement à la suite du jeu en passant l'histoire
    public void NextAll() {
        animators[currentAnimator].SetBool("Finish", true);
        Invoke("ChangeScene", 1f);
    }

    // Permet de passer à la scene suivante
    public void ChangeScene() {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex+ 1);
    }

    // Fonction utilitaire de Next(), appelée uniquement quand l'image change en plus du texte
    private void NextChangeImage() {
        if(currentStory < stories.Count) {
            stories[currentStory].SetActive(false);
        }
        if(currentStory < images.Count) {
            images[currentStory].SetActive(false);
        }
        ++currentStory; 
        if(currentStory < images.Count) {
            images[currentStory].SetActive(true);
        } else {
            ChangeScene();
        }
        if(currentStory < stories.Count) {
            stories[currentStory].SetActive(true);
        }
    }
}

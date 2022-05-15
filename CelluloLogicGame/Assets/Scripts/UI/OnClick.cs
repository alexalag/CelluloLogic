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
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void Click() {
        if(Constantes.writing) {
            Constantes.writing = false;
        } 
    }

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

    public void StartGame() {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex+ 1);
    }

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
            StartGame();
        }
        if(currentStory < stories.Count) {
            stories[currentStory].SetActive(true);
        }
    }
}

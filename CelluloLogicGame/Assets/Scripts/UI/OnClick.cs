using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnClick : MonoBehaviour
{

    public List<GameObject> stories;
    public List<GameObject> images;
    private int currentStory = 0;
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
        images[currentStory].SetActive(false);
        stories[currentStory].SetActive(false);
        ++currentStory;
        if(currentStory < stories.Count) {
            stories[currentStory].SetActive(true);
        }
        if(currentStory < images.Count) {
            images[currentStory].SetActive(true);
        }
    }
}

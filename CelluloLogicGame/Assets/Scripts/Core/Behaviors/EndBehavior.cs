using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndBehavior : MonoBehaviour
{

    public GameManager gameManager;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnTriggerEnter(Collider player) {
        if(player.transform.parent.gameObject.tag == "Player") {
            gameManager.EndLevel();
            print("trigger");
        }
    }
}

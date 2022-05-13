using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NormalGateBehavior : AgentBehaviour
{
    private GameManager gameManager;
    // Start is called before the first frame update
    void Start()
    {
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        gameObject.tag = "NormalGate";
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

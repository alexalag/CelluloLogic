using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NormalGateBehavior : MonoBehaviour
{
    private GameManager gameManager;
    public GameObject fil;
    private Animator animator;
    public bool finalDoor;
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        gameObject.tag = "NormalGate";
    }

    // Update is called once per frame
    void Update()
    {
        if (fil.GetComponent<FilsBehavior>().allume){
            animator.ResetTrigger("Close Door");
            animator.SetTrigger("Open Door");
        } else {
            if(!finalDoor) {
                animator.ResetTrigger("Open Door");
                animator.SetTrigger("Close Door");
            }
        }
    }
}

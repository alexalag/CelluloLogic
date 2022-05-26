using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NormalGateBehavior : MonoBehaviour
{
    private GameManager gameManager;
    public GameObject fil;
    public Animator animator;
    public Animator animator2;
    public bool finalDoor;
    private AudioSource audioSource;
    private bool closed;
    // Start is called before the first frame update
    void Start()
    {
        closed = true;
        audioSource = GetComponent<AudioSource>();
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        animator.gameObject.tag = "NormalGate";
        animator2.gameObject.tag = "NormalGate";
    }

    // Update is called once per frame
    void Update()
    {
        if (fil.GetComponent<FilsBehavior>().allume){
            animator.ResetTrigger("Close Door");
            animator2.ResetTrigger("Close Door");
            animator.SetTrigger("Open Door");
            animator2.SetTrigger("Open Door");
            if(closed) {
               audioSource.Play();
               closed = false;
            }
        } else {
            if(!finalDoor) {
                animator.ResetTrigger("Open Door");
                animator2.ResetTrigger("Open Door");
                animator.SetTrigger("Close Door");
                animator2.SetTrigger("Close Door");
                if(!closed) {
                    audioSource.Play();
                    closed = true;
                }
            }
        }
    }
}

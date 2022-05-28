using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NormalGateBehavior : MonoBehaviour
{
    private GameManager gameManager;
    public GameObject fil;
    public Animator animator;
    public Animator animator2;
    public bool canBeOpen;
    public bool canBeClosed;
    private AudioSource audioSource;
    private bool closed;

    public bool startOpened;

    // Start is called before the first frame update
    void Start()
    {
        if(startOpened)
        {
            animator.SetTrigger("Instant Open");
            animator2.SetTrigger("Instant Open");
        }
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
            if(canBeOpen)
            {
                animator.ResetTrigger("Close Door");
                animator2.ResetTrigger("Close Door");
                animator.SetTrigger("Open Door");
                animator2.SetTrigger("Open Door");
                if(closed) {
                    audioSource.Play();
                    closed = false;
                }
            }
            
        } else {
            if(canBeClosed) {
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

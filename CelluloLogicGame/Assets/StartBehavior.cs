using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartBehavior : MonoBehaviour
{
    public FilsBehavior fil;
    private bool trueIsTriggerStay, falseIsTriggerStay;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        print(trueIsTriggerStay || falseIsTriggerStay);
        if (!trueIsTriggerStay && !falseIsTriggerStay)
        {
            fil.allume = false;
        } else
        {
            fil.allume = true;
        }
    }

    public void OnTriggerEnter(Collider player)
    {
        if (player.transform.parent.gameObject.tag == "Player")
        {
            if (player.transform.parent.gameObject.GetComponent<MoveWithKeyboardBehavior>().CelluloName == "True")
            {
                trueIsTriggerStay = true;
            }
            else
            {
                falseIsTriggerStay = true;
            }
        }
    }

    public void OnTriggerExit(Collider player)
    {
        if (player.transform.parent.gameObject.tag == "Player")
        {
            if (player.transform.parent.gameObject.GetComponent<MoveWithKeyboardBehavior>().CelluloName == "True")
            {
                trueIsTriggerStay = false;
            }
            else
            {
                falseIsTriggerStay = false;
            }
        }
    }
}

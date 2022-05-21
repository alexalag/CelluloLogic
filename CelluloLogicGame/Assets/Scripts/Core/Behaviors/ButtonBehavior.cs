using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonBehavior : MonoBehaviour
{
    public List<GameObject> filsOut;
    private int numberOfCurrentCollider;

    // Start is called before the first frame update
    void Start()
    {
        numberOfCurrentCollider = 0;
    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnTriggerEnter(Collider other)
    {
        if (other.transform.parent.gameObject.tag == "Player"|| other.transform.parent.gameObject.tag == "Unactive"){
            foreach(GameObject fil in filsOut) {
                fil.GetComponent<FilsBehavior>().allume = true;
            }
            ++numberOfCurrentCollider;
        }
    }
    void OnTriggerExit(Collider other)
    {
        if (other.transform.parent.gameObject.tag == "Player" || other.transform.parent.gameObject.tag == "Unactive"){
            --numberOfCurrentCollider;
        }
        if (numberOfCurrentCollider <= 0) {
            foreach(GameObject fil in filsOut) {
                fil.GetComponent<FilsBehavior>().allume = false;
            }
        }
    }

}

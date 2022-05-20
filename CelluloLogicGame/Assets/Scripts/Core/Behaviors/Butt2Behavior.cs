using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Butt2Behavior : MonoBehaviour
{
    public GameObject fil1; // grand fil
    public GameObject fil2; // petit fil avant not
    public GameObject fil3; // le fil qui va au AND gate
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

     void OnTriggerEnter(Collider other)
    {
        if (other.transform.parent.gameObject.tag == "Player"|| other.transform.parent.gameObject.tag == "Unactive" ){
            fil1.GetComponent<FilsBehavior>().allume = true;
            fil2.GetComponent<FilsBehavior>().allume = false;
            fil3.GetComponent<FilsBehavior>().allume = false;
        } 
    }

    void OnTriggerExit(Collider other)
    {
        if (other.transform.parent.gameObject.tag == "Player"|| other.transform.parent.gameObject.tag == "Unactive" ){
            fil1.GetComponent<FilsBehavior>().allume = false;
            fil2.GetComponent<FilsBehavior>().allume = true;
            fil3.GetComponent<FilsBehavior>().allume = true;
        } 
    }
}

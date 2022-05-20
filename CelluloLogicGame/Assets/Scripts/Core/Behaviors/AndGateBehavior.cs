using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AndGateBehavior : MonoBehaviour
{
    public GameObject fil1;
    public GameObject fil2;
    public GameObject fil3;
    public GameObject fil4;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(fil1.GetComponent<FilsBehavior>().allume && fil2.GetComponent<FilsBehavior>().allume){
            fil3.GetComponent<FilsBehavior>().allume = true;
            fil4.GetComponent<FilsBehavior>().allume = true;
        } else {
            fil3.GetComponent<FilsBehavior>().allume = false;
            fil4.GetComponent<FilsBehavior>().allume = false;
        }
    }
}

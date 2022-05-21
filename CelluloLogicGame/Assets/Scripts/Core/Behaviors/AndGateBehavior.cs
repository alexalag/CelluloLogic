using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AndGateBehavior : MonoBehaviour
{
    public GameObject filIn1;
    public GameObject filIn2;
    public List<GameObject> filsOut;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        foreach(GameObject fil in filsOut) {
            fil.GetComponent<FilsBehavior>().allume = 
                filIn1.GetComponent<FilsBehavior>().allume && filIn2.GetComponent<FilsBehavior>().allume;
        }
    }
}

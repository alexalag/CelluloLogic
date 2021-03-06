using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AndGateBehavior : MonoBehaviour
{
    public List<GameObject> filsIn;
    public List<GameObject> filsOut;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        bool tousAlumes = true;
        foreach(GameObject fil in filsIn) {
            if(!fil.GetComponent<FilsBehavior>().allume) tousAlumes = false;
        }
        foreach(GameObject fil in filsOut) {
            fil.GetComponent<FilsBehavior>().allume = tousAlumes;
        }
    }
}

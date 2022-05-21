using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NotGateBehavior : MonoBehaviour
{
    public GameObject filIn;
    public List<GameObject> filsOut;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        foreach(GameObject fil in filsOut) {
            fil.GetComponent<FilsBehavior>().allume = !filIn.GetComponent<FilsBehavior>().allume;
        }
    }


}

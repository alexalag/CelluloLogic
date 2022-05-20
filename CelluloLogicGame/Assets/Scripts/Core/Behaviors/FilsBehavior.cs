using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FilsBehavior : MonoBehaviour
{
    public bool allume = false; // tester si ça bug pour les fils allumés au début
    public Material Neon;
    public Material Eteint;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (allume)
        {
            this.GetComponent<MeshRenderer>().material = Neon;
        } else {
            this.GetComponent<MeshRenderer>().material = Eteint;
        }
    }
}

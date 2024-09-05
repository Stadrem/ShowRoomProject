using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class matTest : MonoBehaviour
{
    Material mat;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        RaycastHit hit;


        if (Physics.Raycast(Camera.main.transform.position, Camera.main.transform.forward, out hit))
            {

        }
    }
}

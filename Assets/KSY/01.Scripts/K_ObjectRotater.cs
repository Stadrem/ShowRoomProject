using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class K_ObjectRotater : MonoBehaviour
{
    public bool canRotate;
    public float speed = 200f;
    void Start()
    {
        
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            canRotate = true;
        }
        if (Input.GetMouseButtonUp(0))
        {
            canRotate = false;
        }
        if (canRotate)
        {
            
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class K_ObjectRotater : MonoBehaviour
{
    public bool canRotate;
    public float speed = 200f;

    float x;
    float y;


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
            x = -Input.GetAxis("Mouse X") * Time.deltaTime * speed;
            //y = Input.GetAxis("Mouse Y") * Time.deltaTime * speed;

            transform.Rotate(0, x, 0, Space.World);
            //transform.Rotate(y, 0, 0, Space.World);
        }
    }
}

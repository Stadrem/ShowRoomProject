using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class K_CamRot : MonoBehaviour
{
    public bool cam;
    public float rotSpeed;
    float my;
    float mx;
    public K_PlayerMove playerMove;

    void Update()
    {
        if (playerMove.currState == K_PlayerMove.PlayerState.Click) return;
        if (cam)
        {
            my -= Input.GetAxis("Mouse Y") * Time.deltaTime * rotSpeed;
            my = Mathf.Clamp(my, -40f, 40f);
            
        }
        else
        {
            mx += Input.GetAxis("Mouse X") * Time.deltaTime * rotSpeed;
            //transform.localEulerAngles += new Vector3(0, mx * Time.deltaTime* rotSpeed, 0);
        }
        transform.localEulerAngles = new Vector3(my, mx, 0);
    }
}

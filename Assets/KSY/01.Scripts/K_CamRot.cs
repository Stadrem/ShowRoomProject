using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class K_CamRot : MonoBehaviourPun
{
    public bool cam;
    public float rotSpeed;
    float my;
    float mx;
    public K_PlayerMove playerMove;

    private float initialY;

    private void Start()
    {
        //if(cam && transform.childCount == 0 && photonView.IsMine)
        //{
        //    Camera.main.transform.parent = gameObject.transform;
        //    Camera.main.transform.localPosition = Vector3.zero;
        //    Camera.main.transform.localRotation = Quaternion.identity;
        //}
        // 초기 Y 값을 Start에서 한 번만 저장
        initialY = transform.localEulerAngles.y;
    }
    void Update()
    {
        if (cam && photonView.IsMine)
        {
            Camera.main.transform.position = transform.position;
            Camera.main.transform.rotation = transform.rotation;
        }
        if (playerMove.currState == K_PlayerMove.PlayerState.Click) return;
        if (!photonView.IsMine) return;
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
        transform.localEulerAngles = new Vector3(my, initialY + mx, 0);
    }
}

﻿using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PlayerUiSet : MonoBehaviour
{
    PhotonView pv;

    public TMP_Text text_Player;

    public GameObject talkPivot;

    public TMP_Text text_Chat;

    // Start is called before the first frame update
    void Start()
    {
        pv = GetComponentInParent<PhotonView>();

        print(pv.Owner.NickName);

        text_Player.text = pv.Owner.NickName;
    }

    // Update is called once per frame
    void Update()
    {
        //항상 메인 카메라에 보이도록 회전 처리
        transform.forward = Camera.main.transform.forward;
    }
}

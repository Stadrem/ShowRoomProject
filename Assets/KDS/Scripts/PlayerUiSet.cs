using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PlayerUiSet : MonoBehaviour
{
    PhotonView pv;

    public TMP_Text text_Player;

    public Canvas ui;

    // Start is called before the first frame update
    void Start()
    {
        pv = GetComponent<PhotonView>();

        print(pv.Owner.NickName);

        text_Player.text = pv.Owner.NickName;
    }

    // Update is called once per frame
    void Update()
    {
        //항상 메인 카메라에 보이도록 회전 처리
        ui.transform.forward = Camera.main.transform.forward;
    }
}

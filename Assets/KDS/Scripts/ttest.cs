using Photon.Pun;
using Photon.Realtime;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ttest : MonoBehaviour
{
    GameObject player;
    public TMP_Text[] text = new TMP_Text[5];

    // Start is called before the first frame update
    void Start()
    {
        print(PhotonNetwork.PlayerList);

        player = PhotonNetwork.Instantiate("Capsule", new Vector3(0,0,0), Quaternion.identity);

        foreach (KeyValuePair<int, Player> playerInfo in PhotonNetwork.CurrentRoom.Players)
        {

            string playerName = playerInfo.Value.NickName;
            // 원하는 로직 추가

            print(playerName);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

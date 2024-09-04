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

    PhotonView pv;

    // Start is called before the first frame update
    void Start()
    {
        //print(PhotonNetwork.PlayerList);

        player = PhotonNetwork.Instantiate("Capsule", new Vector3(0,0,0), Quaternion.identity);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

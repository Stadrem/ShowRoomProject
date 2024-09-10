using Photon.Pun;
using Photon.Realtime;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using static UnityEditor.PlayerSettings;

public class PlayerMovePhoton : MonoBehaviour, IPunObservable
{
    K_PlayerMove kpm;
    PhotonView pv;

    float h, v, prevH, prevV = 0;

    float trackingSpeed = 50;

    Vector3 myPos;
    Quaternion myRot;

    GameObject player;

    Animator myAnim;

    // Start is called before the first frame update
    void Start()
    {
        kpm = GetComponentInParent<K_PlayerMove>();
        pv = GetComponentInParent<PhotonView>();
        player = transform.parent.gameObject;
        myAnim = GetComponentInParent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!pv.IsMine)
        {
            //h = kpm.h;
            //v = kpm.v;

            player.transform.position = Vector3.Lerp(player.transform.position, myPos, Time.deltaTime * trackingSpeed);
            player.transform.rotation = Quaternion.Lerp(player.transform.rotation, myRot, Time.deltaTime * trackingSpeed);

            h = Mathf.Lerp(prevH, h, Time.deltaTime * 100);
            v = Mathf.Lerp(prevV, v, Time.deltaTime * 100);

            myAnim.SetFloat("Horizontal", h);
            myAnim.SetFloat("Vertical", v);

            prevH = h;
            prevV = v;
        }
    }

    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        //만일, 데이터를 서버에 전송(PhotonView.IsMine == true)하는 상태라면...
        if (stream.IsWriting)
        {
            //iterable 데이터를 보낸다.
            stream.SendNext(player.transform.position);
            stream.SendNext(player.transform.rotation);
            stream.SendNext(h);
            stream.SendNext(v);
        }
        //그렇지 않고, 만일 데이터를 서버로 부터 읽어오는 상태라면...
        else if (stream.IsReading)
        {
            myPos = (Vector3)stream.ReceiveNext();
            myRot = (Quaternion)stream.ReceiveNext();
            h = (float)stream.ReceiveNext();
            v = (float)stream.ReceiveNext();
        }
    }
}

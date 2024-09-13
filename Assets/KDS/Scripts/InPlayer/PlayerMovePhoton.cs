using Photon.Pun;
using Photon.Realtime;
using Photon.Voice.PUN;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerMovePhoton : MonoBehaviour, IPunObservable
{
    K_PlayerMove kpm;
    PhotonView pv;
    public PlayerUiSet playerUiSet;

    float h, v, prevH, prevV = 0;

    float trackingSpeed = 50;

    Vector3 myPos;
    Quaternion myRot;

    GameObject player;

    Animator myAnim;

    public GameObject iconRec;

    PhotonVoiceView voiceView;

    bool isTalking = false;

    // Start is called before the first frame update
    void Start()
    {
        kpm = GetComponent<K_PlayerMove>();
        pv = GetComponent<PhotonView>();
        player = gameObject;
        voiceView = GetComponent<PhotonVoiceView>();
    }

    // Update is called once per frame
    void Update()
    {
        if (myAnim == null) myAnim = transform.GetChild(0).GetComponentInChildren<Animator>();

        //타인의 캐릭터 이동 처리
        if (!pv.IsMine)
        {
            player.transform.position = Vector3.Lerp(player.transform.position, myPos, Time.deltaTime * trackingSpeed);
            player.transform.rotation = Quaternion.Lerp(player.transform.rotation, myRot, Time.deltaTime * trackingSpeed);

            h = Mathf.Lerp(prevH, h, Time.deltaTime * 100);
            v = Mathf.Lerp(prevV, v, Time.deltaTime * 100);

            prevH = h;
            prevV = v;

            if (Mathf.Abs(h) > 0.1f || Mathf.Abs(v) > 0.1f)
            {
                myAnim?.SetBool("Move", true);
            }
            else
            {
                myAnim?.SetBool("Move", false);
            }
        }
        //내 캐릭터라면 이동 값 저장
        else
        {
            h = Input.GetAxis("Horizontal");
            v = Input.GetAxis("Vertical");
        }

        myAnim.SetFloat("h", h);
        myAnim.SetFloat("v", v);

        //현재 말을 하고 있다면 보이스 아이콘을 활성화
        if (pv.IsMine)
        {
            iconRec.SetActive(voiceView.IsRecording);
        }
        else
        {
            iconRec.SetActive(isTalking);
        }

        //만약 V키를 누르면 음성 활성화함
        if (Input.GetKeyDown(KeyCode.V))
        {
            RPC_TalkAnim();
        }
    }

    public void RPC_SelectButton(int value)
    {
        if (pv.IsMine)
        {
            pv.RPC("SelectButton", RpcTarget.All, value);
        }
    }

    [PunRPC]
    public void SelectButton(int value) // SelectButton을 누름으로써 값 테스트.    
    {
        Debug.Log("Dropdown Value: " + value);

        //아바타 설정있는 스크립트 불러오기 -> 캐릭터에 붙어있는 스크립트
        K_PlayerMove kpm = player.GetComponent<K_PlayerMove>();

        //아바타 변경 함수
        kpm.SetAvatar(kpm.bodys[value]);

        myAnim = GetComponentInChildren<Animator>();
    }

    public void RPC_TalkPopUp(string value)
    {
        if (pv.IsMine)
        {
            pv.RPC("TalkPopUp", RpcTarget.All, value);
        }
    }

    [PunRPC]
    void TalkPopUp(string receiveMessage)
    {
        StartCoroutine(ChatPopUp(receiveMessage));

        myAnim.SetTrigger("Talk");
    }

    public void RPC_TalkAnim()
    {
        if (pv.IsMine)
        {
            pv.RPC("TalkAnim", RpcTarget.All);
        }
    }

    [PunRPC]
    void TalkAnim()
    {
        myAnim.SetTrigger("Talk");
    }

    IEnumerator ChatPopUp(string text)
    {
        playerUiSet.talkPivot.SetActive(true);

        playerUiSet.text_Chat.text = text;

        yield return new WaitForSeconds(2);

        playerUiSet.talkPivot.SetActive(false);

        playerUiSet.text_Chat.text = "";
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
            stream.SendNext(voiceView.IsRecording);
        }
        //그렇지 않고, 만일 데이터를 서버로 부터 읽어오는 상태라면...
        else if (stream.IsReading)
        {
            myPos = (Vector3)stream.ReceiveNext();
            myRot = (Quaternion)stream.ReceiveNext();
            h = (float)stream.ReceiveNext();
            v = (float)stream.ReceiveNext();
            isTalking = (bool)stream.ReceiveNext();
        }
    }
}

using ExitGames.Client.Photon;
using Photon.Pun;
using Photon.Realtime;
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.Jobs;
using UnityEngine;

public class ChatManager : MonoBehaviourPun, IOnEventCallback
{
    PhotonPlayerBase ppb;
    PlayerUiSet pus;
    GameObject talkPivot;
    public TMP_Text text_chat;
    public TMP_Text text_chatContent;
    public TMP_InputField input_chat;

    const byte chattingEvent = 1;

    private void OnEnable()
    {
        //함수 방식
        PhotonNetwork.NetworkingClient.AddCallbackTarget(this);
    }

    // Start is called before the first frame update
    void Start()
    {
        ppb = GetComponent<PhotonPlayerBase>();

        input_chat = GameUiCanvas.instance.input_chat;

        text_chatContent = GameUiCanvas.instance.Text_chatContent;
        //인풋 필드의 제출 이벤트에 함수를 바인딩한다.
        input_chat.onSubmit.AddListener(SendMyMessage);

        text_chatContent.text = "";

        StartCoroutine(Delay());

    }

    public void SendMyMessage(string msg)
    {
        Debug.Log("SendMyMessage 호출");

        msg = input_chat.text;
        if (input_chat.text.Length > 0)
        {
            Debug.Log("채팅 전송");
            //이벤트에 보낼 채팅 내용
            object[] sendContent = new object[] {PhotonNetwork.NickName, msg };

            //송신 옵션
            RaiseEventOptions eventOptions = new RaiseEventOptions();
            eventOptions.Receivers = ReceiverGroup.All;
            eventOptions.CachingOption = EventCaching.DoNotCache;

            //이벤트 송신 시작
            PhotonNetwork.RaiseEvent(1, sendContent, eventOptions, SendOptions.SendUnreliable);

            input_chat.text = "";
        }
    }

    //Raise 콜백 함수, 같은 룸의 다른 사용자로부터 이벤트가 왔을 때 실행되는 함수
    public void OnEvent(EventData photonEvent)
    {
        //만일, 받은 이벤트가 채팅 이벤트라면... 
        if (photonEvent.Code == chattingEvent)
        {
            Debug.Log("채팅 받음");
            //받은 내용을 "닉네임: 채팅 내용" 형식으로 ScrollView의 text에 전달.
            object[] receiveObejct = (object[])photonEvent.CustomData;

            string receiveMessage = $"\n{receiveObejct[0].ToString()}: {receiveObejct[1].ToString()}";

            StopAllCoroutines();

            StartCoroutine(ChatPopUp(receiveMessage, 2f));
        }
    }

    private void OnDisable()
    {
        //함수로 할 때
        PhotonNetwork.NetworkingClient.RemoveCallbackTarget(this);
    }

    IEnumerator Delay()
    {
        yield return new WaitForSeconds(1.0f);
        pus = ppb.player.GetComponentInChildren<PlayerUiSet>();
        talkPivot = pus.talkPivot;
        text_chat = pus.text_Chat;

        text_chat.text = "";
    }

    public IEnumerator ChatPopUp(string text, float time)
    {
        talkPivot.SetActive(true);

        text_chat.text = text;

        text_chatContent.text += text;

        yield return new WaitForSeconds(time);

        talkPivot.SetActive(false);
    }
}

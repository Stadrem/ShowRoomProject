using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using System.Reflection;
using System;
using UnityEngine.UI;
using TMPro;

public class ConnectionManager : MonoBehaviourPunCallbacks
{
    public string setRoom = "";

    public int playerCount = 5;

    string userName;

    public GameObject firstCanvas;
    public GameObject secondCanvas;
    public TMP_InputField joinCodeText;

    public void StartLogin()
    {
        //접속을 위한 설정
        PhotonNetwork.GameVersion = "1.0.0";
        PhotonNetwork.NickName = AccountDate.instance.response.userName;
        PhotonNetwork.AutomaticallySyncScene = true;

        //접속을 서버에 요청
        PhotonNetwork.ConnectUsingSettings();

        HttpManager.GetInstance().serverLodingOn();

        print("닉네임 설정 : " + PhotonNetwork.NickName);

        HttpManager.GetInstance().Alert("서버 접속 요청 중", 1.0f);
    }

    public override void OnConnected()
    {
        base.OnConnected();

        //Name 서버에 접속이 완료 되었음을 알려줌
        print(MethodInfo.GetCurrentMethod().Name + " is Call");
    }

    public override void OnDisconnected(DisconnectCause cause)
    {
        base.OnDisconnected(cause);

        //실패 원인을 출력
        Debug.LogError("Disconnected from Server - " + cause);
    }

    public override void OnConnectedToMaster()
    {
        base.OnConnectedToMaster();

        //Master 서버에 접속이 완료 되었음을 알려줌
        print(MethodInfo.GetCurrentMethod().Name + " is Call");

        HttpManager.GetInstance().serverLodingOff();
        HttpManager.GetInstance().Alert("환영합니다!", 3.0f);

        //서버의 로비로 들어간다.
        PhotonNetwork.JoinLobby();
    }

    public override void OnJoinedLobby()
    {
        base.OnJoinedLobby();

        //서버 로비에 들어갔음을 알려줌
        print(MethodInfo.GetCurrentMethod().Name + " is Call");

        firstCanvas.SetActive(false);
        secondCanvas.SetActive(true);

        //CreateRoom();
    }

    public void CreateRoom()
    {
        setRoom = joinCodeText.text;

        if (setRoom == "")
        {
            HttpManager.GetInstance().Alert("초대 코드를 입력해주세요.", 2.0f);
        }
        else
        {
            //나의 룸을 만든다.
            RoomOptions roomOpt = new RoomOptions();
            roomOpt.MaxPlayers = playerCount;
            roomOpt.IsOpen = true;
            roomOpt.IsVisible = true;

            AccountDate.GetInstance().joinCode = setRoom;

            PhotonNetwork.CreateRoom(setRoom, roomOpt, TypedLobby.Default);
        }

    }

    public void JoinRoom()
    {
        setRoom = joinCodeText.text;

        if (setRoom == "")
        {
            HttpManager.GetInstance().Alert("초대 코드를 입력해주세요.", 2.0f);
        }
        else
        {
            AccountDate.GetInstance().joinCode = setRoom;

            PhotonNetwork.JoinRoom(setRoom);
        }
    }

    public override void OnCreatedRoom()
    {
        base.OnCreatedRoom();

        //성공적으로 방이 개설되었음을 알려줌
        print(MethodInfo.GetCurrentMethod().Name + " is Call");

        JoinRoom();
    }

    public override void OnCreateRoomFailed(short returnCode, string message)
    {
        base.OnCreateRoomFailed(returnCode, message);

        JoinRoom();
    }

    public override void OnJoinedRoom()
    {
        base.OnJoinedRoom();

        //성공적으로 방에 참여했음을 알려줌
        print(MethodInfo.GetCurrentMethod().Name + " is Call");

        //방에 입장한 친구들은 모두 1번 씬으로 이동
        PhotonNetwork.LoadLevel(1);

        //GameUiCanvas.GetInstance().MakeNamePlate(AccountDate.GetInstance().currentInfo.userName);
    }

    public override void OnJoinRoomFailed(short returnCode, string message)
    {
        base.OnJoinRoomFailed(returnCode, message);

        //룸에 입장이 실패 원인을 출력
        Debug.LogError("Disconnected from Room - " + message);

        if(message == "Game does not exist")
        {
            HttpManager.GetInstance().Alert("방이 없습니다.", 2.0f);
        }
    }
}

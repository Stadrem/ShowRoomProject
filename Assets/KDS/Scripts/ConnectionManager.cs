using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using System.Reflection;
using System;

public class ConnectionManager : MonoBehaviourPunCallbacks
{
    public static ConnectionManager instance;

    public string setRoom = "main";

    public int playerCount = 5;

    string userName;

    private void Awake()
    {
        if (instance == null)
        {
            // 인스턴스 설정
            instance = this;

            // 씬 전환 시 객체 파괴 방지
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            // 이미 인스턴스가 존재하면 현재 객체를 파괴
            Destroy(gameObject);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        //Screen.SetResolution(640, 480, false);
    }

    // Update is called once per frame
    void Update()
    {

    }

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

        //서버의 로비로 들어간다.
        PhotonNetwork.JoinLobby();
    }

    public override void OnJoinedLobby()
    {
        base.OnJoinedLobby();

        //서버 로비에 들어갔음을 알려줌
        print(MethodInfo.GetCurrentMethod().Name + " is Call");

        CreateRoom();
    }

    public void CreateRoom()
    {
        //나의 룸을 만든다.
        RoomOptions roomOpt = new RoomOptions();
        roomOpt.MaxPlayers = playerCount;
        roomOpt.IsOpen = true;
        roomOpt.IsVisible = true;

        PhotonNetwork.CreateRoom(setRoom, roomOpt, TypedLobby.Default);
    }

    public void JoinRoom()
    {
        PhotonNetwork.JoinRoom(setRoom);
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
    }

    //룸에 다른 플레이어가 입장했을 때의 콜백 함수
    public override void OnPlayerEnteredRoom(Player newPlayer)
    {
        base.OnPlayerEnteredRoom(newPlayer);

        string playerMsg = $"{newPlayer.NickName}님이 입장하셨습니다.";

        GameUiCanvas.instance.StartPlate();
    }

    //룸에 있던 다른 플레이어가 퇴장했을 때의 콜백 함수
    public override void OnPlayerLeftRoom(Player otherPlayer)
    {
        base.OnPlayerLeftRoom(otherPlayer);

        string playerMsg = $"{otherPlayer.NickName}님이 퇴장하셨습니다.";

        GameUiCanvas.instance.StartPlate();
    }
}

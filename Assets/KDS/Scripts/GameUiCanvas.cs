using Photon.Pun;
using Photon.Realtime;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameUiCanvas : MonoBehaviourPunCallbacks
{
    //싱글톤 생성
    public static GameUiCanvas instance;

    public GameObject list_Name;

    public GameObject namePlatePrefab;

    public TMP_Text joinCodeText;

    public TMP_Dropdown avatarDropdown;

    public TMP_InputField input_chat;

    public TMP_Text Text_chatContent;

    public GameObject iconMicOFF;

    public GameObject iconMicON;

    public PhotonPlayerBase ppb;

    public PhotonView pv;


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
        StartPlate();

        joinCodeText.text = AccountDate.instance.joinCode;

        StartCoroutine(StartDelay());
    }

    // Update is called once per frame
    void Update()
    {
        //만약 V키를 누르면 음성 활성화함
        if (Input.GetKeyDown(KeyCode.V))
        {
            //iconMicON이 활성화되어 있으면 비활성화하고, 비활성화되어 있으면 활성화. iconMicOFF도 반대로 동작.
            bool isActive = iconMicON.activeSelf;
            iconMicON.SetActive(!isActive);
            iconMicOFF.SetActive(isActive);
        }
    }

    public void GameExit()
    {
        Application.Quit();
    }

    public void StartPlate()
    {
        Dictionary<int, Photon.Realtime.Player> tt = PhotonNetwork.CurrentRoom.Players;

        foreach(var c in tt)
        {
            print("key : " + c.Key);
        }
        print("현재 플레이어 수 : " + PhotonNetwork.CurrentRoom.Players.Count);

        foreach (var playerEntry in tt)
        {
            Photon.Realtime.Player ad = playerEntry.Value;

            print("닉네임 : " + ad.NickName);

            MakeNamePlate(ad.NickName);
        }
    }


    public void MakeNamePlate(string name)
    {
        //기존의 방 정보를 삭제함.
        for (int i = 0; i < list_Name.transform.childCount; i++)
        {
            Destroy(list_Name.transform.GetChild(i).gameObject);
        }

        // 모든 플레이어의 닉네임을 표시함.
        foreach (var player in PhotonNetwork.CurrentRoom.Players.Values)
        {
            GameObject plate = Instantiate(namePlatePrefab, list_Name.transform);

            NamePlate np = plate.GetComponent<NamePlate>();

            // 각 플레이어의 닉네임을 사용함.
            np.NewNamePlate(player.NickName); 
        }
    }

    //룸에 다른 플레이어가 입장했을 때의 콜백 함수
    public override void OnPlayerEnteredRoom(Player newPlayer)
    {
        base.OnPlayerEnteredRoom(newPlayer);

        string playerMsg = $"{newPlayer.NickName}님이 입장하셨습니다.";

        StartPlate();
    }

    //룸에 있던 다른 플레이어가 퇴장했을 때의 콜백 함수
    public override void OnPlayerLeftRoom(Player otherPlayer)
    {
        base.OnPlayerLeftRoom(otherPlayer);

        string playerMsg = $"{otherPlayer.NickName}님이 퇴장하셨습니다.";

        StartPlate();
    }

    public void DropSelectButton()
    {
        pv.GetComponentInChildren<PlayerMovePhoton>().RPC_SelectButton((int)avatarDropdown.value);
    }

    /*
    public void RPC_SelectButton()
    {
        if (pv.IsMine)
        {
            pv.RPC("SelectButton", RpcTarget.All, (int)avatarDropdown.value);
        }
    }

    [PunRPC]
    public void SelectButton(int value) // SelectButton을 누름으로써 값 테스트.    
    { 
        Debug.Log("Dropdown Value: " + value);

        //아바타 설정있는 스크립트 불러오기 -> 캐릭터에 붙어있는 스크립트
        K_PlayerMove kpm = ppb.player.GetComponent<K_PlayerMove>();

        //아바타 변경 함수
        kpm.SetAvatar(kpm.bodys[value]);
    }
    */

    IEnumerator StartDelay()
    {
        yield return new WaitForSeconds(1.0f);

        ppb = GameObject.Find("PhotonPlayerBase").GetComponent<PhotonPlayerBase>();

        pv = ppb.pv;
    }
}

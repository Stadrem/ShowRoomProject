using Photon.Pun;
using Photon.Realtime;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameUiCanvas : MonoBehaviour
{
    //싱글톤 생성
    public static GameUiCanvas instance;

    public GameObject list_Name;

    public GameObject namePlatePrefab;

    public TMP_Text joinCodeText;

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

        joinCodeText.text = ConnectionManager.instance.setRoom;
    }

    // Update is called once per frame
    void Update()
    {

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
}

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

[System.Serializable]
public class UsersInfo
{
    public long id;
    public int userNo;
    public string userId;
    public string userName;
    public string createDate;
    public int pointAmount;
    public string description;
}

public class K_ProcessData : MonoBehaviour
{
    int totalPointperID;
    int addPoint;
    public Action action;
    public int totalPoint;
    public GameObject buttonMethod;
    

    static K_ProcessData instance;

    UsersInfo infos = new UsersInfo();


    public static K_ProcessData GetInstance()
    {
        if(instance == null)
        {
            GameObject go = new GameObject();
            go.name = "K_ProcessData";
            go.AddComponent<K_ProcessData>();
        }
        return instance;
    }

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Start()
    {
    }

    public void ShowTotalPoints()
    {
        K_HttpInfo info = new K_HttpInfo();
        info.url = "http://192.168.1.17:8080/points/all";
        info.onComplete = (downloadHandler) =>
        {
            print(downloadHandler.text);
            string jsonData = "{ \"data\" : " + downloadHandler.text + "}";
            JsonArray<UsersInfo> allInfo = JsonUtility.FromJson<JsonArray<UsersInfo>>(jsonData);
            print(allInfo);
            foreach (var c in allInfo.data)
            {
                print("아이디 : " + c.id);
                print(c.userId);
                print(c.userNo);
                print(c.createDate);
                print("점수 : " + c.pointAmount);
                totalPoint += c.pointAmount;
            }
            if(action != null) action();
        };
        StartCoroutine(K_HttpManager.GetInstance().Get(info));
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha2)) // Get 테스트
        {
            K_HttpInfo info = new K_HttpInfo();
            info.url = "http://192.168.1.17:8080/points/all";
            info.onComplete = (downloadHandler) => 
            { 
                print(downloadHandler.text);
                string jsonData = "{ \"data\" : " + downloadHandler.text + "}";
                JsonArray<UsersInfo> allInfo = JsonUtility.FromJson<JsonArray<UsersInfo>>(jsonData);
                print(allInfo);
                foreach(var c in allInfo.data)
                {
                    print("아이디 : " + c.id);
                    print(c.createDate);
                    print("점수 : " + c.pointAmount);
                    totalPoint += c.pointAmount;
                }
                if (action != null) action();
            };
            StartCoroutine(K_HttpManager.GetInstance().Get(info));
        }
        //if (Input.GetKeyDown(KeyCode.Alpha4)) // Post 테스트 // 완료. // 이미지 보내기
        //{
        //    HttpInfo info = new HttpInfo();
        //    info.url = "http://192.168.1.17:8080/api/image";
        //    info.onComplete = OnComplete;
        //    info.contentType =  "image/png";
        //    //info.body = $"{Application.dataPath}/KSY/Test0.png";
        //    info.body = $"{Application.dataPath}/KSY/Test{GameManager_K.GetInstance().camShotCnt}.png";
        //    print(info.body);
        //    GameManager_K.GetInstance().PicturePath(info.body);
        //    StartCoroutine(HttpManager.GetInstance().UploadFileByByte(info));
        //}
        //if (Input.GetKeyDown(KeyCode.Alpha3)) // 정보 보내기 테스트
        //{
        //    HttpInfo info = new HttpInfo();
        //    info.url = "http://192.168.1.17:8080/api/image";
        //    info.onComplete = OnComplete;
        //    info.contentType = "/Application_Json";
        //    info.body = $"{Application.dataPath}/KSY/Test{WebCamOpen.instance.cnt}.PNG";
        //    StartCoroutine(HttpManager.GetInstance().UploadFileByByte(info));
        //}
    }


    public void TransferData()
    {
        K_HttpInfo info = new K_HttpInfo();
        info.url = "http://192.168.1.17:8080/api/image";
        info.onComplete = OnComplete;
        info.contentType = "image/png";
        //info.body = $"{Application.dataPath}/KSY/Test{GameManager_K.GetInstance().camShotCnt}.png";
        print("보낸 자료 : " + info.body);
        //GameManager_K.GetInstance().PicturePath(info.body);
        StartCoroutine(K_HttpManager.GetInstance().UploadFileByByte(info));
        //if(buttonCs != null)
        //{
        //    buttonCs.noteText.text = "분석중입니다.";
        //    buttonCs.ActiveNoteUI(true);
        //}
    }
    void OnComplete(DownloadHandler downloadHandler)
    {
        print(downloadHandler.text);

        //predict = JsonUtility.FromJson<Predict>(downloadHandler.text);
        JsonArray<Predict> allInfo = JsonUtility.FromJson<JsonArray<Predict>>(downloadHandler.text);
        //if (buttonCs != null)
        //{
        //    buttonCs.CheckKind();
        //    buttonCs.NoteButton(true);
        //}
        
        //print(predict.prediction); // pet-bottle 나옴.
    }





    public void CompleteMission() // 미션 완료했을 때 시행.
    {
        AddPoint(addPoint);
    }

    public void AddPoint(int add) // 포인트 증가시키는 함수.
    {
        totalPointperID += add;
    }


}

using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Net;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.Rendering;
using UnityEngine.UI;

public class HttpTest : MonoBehaviour
{
    void Start()
    {
        
    }

    public PostInfoArray allPostInfo;

    //"https://jsonplaceholder.typicode.com/posts"
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            K_HttpInfo info = new K_HttpInfo();
            info.url = "http://mtvs.helloworldlabs.kr:7771/api/string?parameter=안녕하세요";
            info.onComplete = OnComplete;
            StartCoroutine(K_HttpManager.GetInstance().Get(info));
        }

        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            K_HttpInfo info = new K_HttpInfo();
            //info.url = "https://jsonplaceholder.typicode.com/albums";
            info.url = "https://ssl.pstatic.net/melona/libs/1506/1506331/b8145c5a724d3f2c9d2b_20240813152032478.jpg";
            info.onComplete = (downloadHandler) => { File.WriteAllBytes(Application.dataPath + "/image2.jpg", downloadHandler.data); };
            StartCoroutine(K_HttpManager.GetInstance().Get(info));
        }

        //if (Input.GetKeyDown(KeyCode.Alpha3))
        //{
        //    // 가상의 나의 데이터를 만들자.
        //    UserInfo userInfo = new UserInfo();
        //    userInfo.name = "메타버스";
        //    userInfo.age = 3;
        //    userInfo.height = 185.5f;

        //    HttpInfo info = new HttpInfo();
        //    info.url = "http://mtvs.helloworldlabs.kr:7771/api/json";
        //    info.body = JsonUtility.ToJson(userInfo);
        //    info.contentType = "application/json";
        //    info.onComplete = (downloadHandler) => { print(downloadHandler.text); };
        //    StartCoroutine(HttpManager.GetInstance().Post(info));
        //}

        if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            K_HttpInfo info = new K_HttpInfo();
            info.url = "http://mtvs.helloworldlabs.kr:7771/api/file";
            info.contentType = "multipart/form-data";
            info.body = "C:\\Users\\Admin\\Downloads\\image.jpg";
            info.onComplete = (downloadHandler) => { File.WriteAllBytes(Application.dataPath + "/image5.jpg", downloadHandler.data); };
            StartCoroutine(K_HttpManager.GetInstance().UploadFileByFormData(info));
        }
        
        if (Input.GetKeyDown(KeyCode.Alpha5))
        {
            K_HttpInfo info = new K_HttpInfo();
            info.url = "http://mtvs.helloworldlabs.kr:7771/api/byte";
            info.contentType = "image/jpg";
            info.body = "C:\\Users\\Admin\\Downloads\\image12.jpg";
            info.onComplete = (downloadHandler) => { File.WriteAllBytes(Application.dataPath + "/image6.jpg", downloadHandler.data); };
            StartCoroutine(K_HttpManager.GetInstance().UploadFileByByte(info));
        }

        if (Input.GetKeyDown(KeyCode.Alpha6))
        {
            K_HttpInfo info = new K_HttpInfo();
            info.url = "https://ssl.pstatic.net/melona/libs/1506/1506331/b8145c5a724d3f2c9d2b_20240813152032478.jpg";
            info.onComplete = (downloadHandler) => {
                // 다운로드된 데이터를 Texture2D로 변환.
                DownloadHandlerTexture handler = downloadHandler as DownloadHandlerTexture; // as를 써주면 변환이 안될경우 null값을 반환해주기 때문에 안전.
                Texture2D texture = handler.texture;
                
                // texture를 이용해서 Sprite로 변환.
                Sprite sprite = Sprite.Create(texture, new Rect(0, 0, texture.width, texture.height), Vector2.zero);

                Image image = GameObject.Find("Image").GetComponent<Image>();
                image.sprite = sprite;
            };
            StartCoroutine(K_HttpManager.GetInstance().DownloadSprite(info));
        }

        if (Input.GetKeyDown(KeyCode.Alpha7))
        {
            K_HttpInfo info = new K_HttpInfo();
            info.url = "오디오url";
            info.contentType = "오디오타입";
            info.onComplete = (downloadHandler) => {
                DownloadHandlerAudioClip handler = downloadHandler as DownloadHandlerAudioClip;
                
                myAudio.clip = handler.audioClip;
            };
            StartCoroutine(K_HttpManager.GetInstance().DownloadAudio(info));
        }

        
    }
    public AudioSource myAudio;

    void OnComplete(DownloadHandler downloadHandler)
    {
        print(downloadHandler.text);

        string jsonData = "{ \"data\" : " + downloadHandler.text + "}";
        //print(jsonData);
        // jsonData를 PostInfoArray형으로 바꾸자.
        //allPostInfo = JsonUtility.FromJson<PostInfoArray>(jsonData);

        JsonArray<PostInfo> allInfo = JsonUtility.FromJson<JsonArray<PostInfo>>(jsonData);
    }
}

//[System.Serializable]
//public struct UserInfo
//{
//    public string name;
//    public int age;
//    public float height;
//}
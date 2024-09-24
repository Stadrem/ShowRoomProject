using JetBrains.Annotations;
using Photon.Pun.Demo.SlotRacer.Utils;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.Networking;


[System.Serializable]
public struct PostInfo
{
    public int userId;
    public int id;
    public string title;
    public string body;
}

[System.Serializable]
public struct Predict
{
    public string prediction;
}

[System.Serializable]
public struct PostInfoArray
{
    public List<PostInfo> data;
}
[System.Serializable]
public struct JsonArray<T>
{
    public List<T> data;
}

public class K_HttpInfo
{
    public string url = "";

    // Body 데이터
    public string body = "";

    // contentType
    public string contentType = "";

    public string token = "";

    // 통신 성공 후 호출되는 함수 담을 변수
    public Action<DownloadHandler> onComplete;

    // 요청 후 에러뜨면 호출될 델리게이트
    public Action<string> onError;
}

public class K_HttpManager : MonoBehaviour
{
    static K_HttpManager instance;

    public static K_HttpManager GetInstance()
    {
        if(instance == null)
        {
            GameObject go = new GameObject();
            go.name = "HttpManager";
            go.AddComponent<K_HttpManager>();
            // go가 만들어지고 go의 Awake가 실행됨.
        }
        return instance;
    }

    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    
    
    // GET : 서버에게 데이터를 조회 요청.
    public IEnumerator Get(K_HttpInfo info)
    {
        using (UnityWebRequest webRequest = UnityWebRequest.Get(info.url))
        {
            webRequest.SetRequestHeader("Authorization", "Bearer " + info.token);
            //print("token : " + info.token);
            // 서버에 요청 보내기
            yield return webRequest.SendWebRequest(); // 응답이 오고 난 이후에 시행되게 함.

            // 서버에게 응답이 왔다.
            DoneRequest(webRequest, info);

            //// 만약에 결과가 정상이라면
            //if(webRequest.result == UnityWebRequest.Result.Success)
            //{
            //    // 우리가 원하는 데이터를 처리
            //    //print(webRequest.downloadHandler.text);
            //    //File.WriteAllBytes(Application.dataPath + "/image.jpg", webRequest.downloadHandler.data);

            //    // 응답 온 데이터를 요청한 클래스로 보내자.
            //    if(info.onComplete != null)
            //    {
            //        info.onComplete(webRequest.downloadHandler);
            //    }
            //}
            //// 그렇지 않다면(Error라면)
            //else
            //{
            //    // Error에 대한 이유를 출력.
            //    Debug.LogError("Net Error : " + webRequest.error);
            //}

        }

    }

    // 서버에게 내가 보내는 데이터를 생성해줘
    public IEnumerator Post(K_HttpInfo info)
    {
        using (UnityWebRequest webRequest = UnityWebRequest.Post(info.url, info.body, info.contentType))
        {
            //Dictionary<string, string> token = new Dictionary<string, string>();
            //token.Add("Bearer", info.token);

            webRequest.timeout = 60;
            //webRequest.SetRequestHeader("Authorization", token.ToString());
            if (info.token != "")
            {
                webRequest.SetRequestHeader("Authorization", "Bearer " + info.token);
                print("token : " + info.token);
            }
            else if (info.token == "")
            {
                print("ai와 통신");
            }
            // 서버에 요청 보내기
            yield return webRequest.SendWebRequest(); // 응답이 오고 난 이후에 시행되게 함.

            // 서버에게 응답이 왔다.
            DoneRequest(webRequest, info);

        }
    }

    // 파일 업로드 (form-data)
    public IEnumerator UploadFileByFormData(K_HttpInfo info)
    {
        // info.data에는 파일의 위치
        // info.data에 있는 파일을 byte 배열로 읽어오자.
        //byte[] data = File.ReadAllBytes("C:\\Users\\Admin\\Downloads\\image.jpg");
        byte[] data = File.ReadAllBytes(info.body);

        // data를 MultipartForm으로 세팅
        List<IMultipartFormSection> formData = new List<IMultipartFormSection>();
        formData.Add(new MultipartFormFileSection("file", data, "image2.jpg", info.contentType));


        //using (UnityWebRequest webRequest = UnityWebRequest.Post(info.url, info.body, info.contentType))
        using (UnityWebRequest webRequest = UnityWebRequest.Post(info.url, formData))
        {
            // 서버에 요청 보내기
            yield return webRequest.SendWebRequest(); // 응답이 오고 난 이후에 시행되게 함.

            // 서버에게 응답이 왔다.
            DoneRequest(webRequest, info);

            //// 만약에 결과가 정상이라면
            //if (webRequest.result == UnityWebRequest.Result.Success)
            //{
            //    // 응답 온 데이터를 요청한 클래스로 보내자.
            //    if (info.onComplete != null)
            //    {
            //        info.onComplete(webRequest.downloadHandler);
            //    }
            //}
            //// 그렇지 않다면(Error라면)
            //else
            //{
            //    // Error에 대한 이유를 출력.
            //    Debug.LogError("Net Error : " + webRequest.error);
            //}

        }
    }

    // 파일 업로드
    public IEnumerator UploadFileByByte(K_HttpInfo info)
    {
        byte[] data = File.ReadAllBytes(info.body);

        using (UnityWebRequest webRequest = new UnityWebRequest(info.url, "POST"))
        {
            // 업로드 하는 데이터
            webRequest.uploadHandler = new UploadHandlerRaw(data);
            webRequest.uploadHandler.contentType = info.contentType;

            // 응답 받는 데이터 공간
            webRequest.downloadHandler = new DownloadHandlerBuffer();

            // 서버에 요청 보내기
            yield return webRequest.SendWebRequest(); // 응답이 오고 난 이후에 시행되게 함.

            // 서버에게 응답이 왔다.
            DoneRequest(webRequest, info);
        }
    }
    
    public IEnumerator DownloadSprite(K_HttpInfo info)
    {
        using(UnityWebRequest webRequest = UnityWebRequestTexture.GetTexture(info.url))
        {
            yield return webRequest.SendWebRequest();

            DoneRequest(webRequest, info);
        }
    }
    
    public IEnumerator DownloadAudio(K_HttpInfo info)
    {
        using (UnityWebRequest webRequest = UnityWebRequestMultimedia.GetAudioClip(info.url, AudioType.WAV))
        {
            yield return webRequest.SendWebRequest();

            DoneRequest(webRequest, info);
        }
    }


    void DoneRequest(UnityWebRequest webRequest, K_HttpInfo info)
    {
        // 만약에 결과가 정상이라면
        if (webRequest.result == UnityWebRequest.Result.Success)
        {
            // 응답 온 데이터를 요청한 클래스로 보내자.
            if (info.onComplete != null)
            {
                info.onComplete(webRequest.downloadHandler);
            }
        }
        // 그렇지 않다면(Error라면)
        else
        {
            // Error에 대한 이유를 출력.
            Debug.LogError("Net Error : " + webRequest.error);

            if (info.onError != null)
            {
                info.onError(webRequest.error);
            }
        }
    }

}

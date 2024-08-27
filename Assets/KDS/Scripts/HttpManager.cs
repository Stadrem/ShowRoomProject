using JetBrains.Annotations;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Net;
using UnityEngine;
using UnityEngine.Networking;
using static AccountDate;

// HttpInfo 클래스 선언: HTTP 요청 관련 정보를 담는 클래스
public class HttpInfo
{
    // 요청할 URL
    public string url = "";

    // 전송할 데이터
    public string body = "";

    // 컨텐츠 타입
    public string contentType = "";

    // 요청이 완료되면 호출될 델리게이트
    public Action<DownloadHandler> onComplete;
}

public class HttpManager : MonoBehaviour
{
    //싱글톤 생성
    static HttpManager instance;

    // 싱글톤 인스턴스를 반환하는 메소드
    public static HttpManager GetInstance()
    {
        if (instance == null)
        {
            // 새로운 게임 오브젝트 생성
            GameObject go = new GameObject();

            // 이름 설정
            go.name = "HttpManager";

            // HttpManager 컴포넌트를 추가
            go.AddComponent<HttpManager>();
        }

        return instance;
    }

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

    //GET : 서버에게 데이터를 조회 요청
    public IEnumerator Get(HttpInfo info)
    {
        // GET 요청 생성
        using (UnityWebRequest webRequest = UnityWebRequest.Get(info.url))
        {
            // 요청 전송 및 응답 대기
            yield return webRequest.SendWebRequest();

            // 요청 완료 시 처리
            DoneRequest(webRequest, info);
        }
    }

    //Post : 데이터를 서버로 전송
    public IEnumerator Post(HttpInfo info)
    {
        //유니티의 http 통신 기능
        using (UnityWebRequest webRequest = UnityWebRequest.Post(info.url, info.body, info.contentType))
        {
            //서버에 요청 보내기
            yield return webRequest.SendWebRequest();

            //서버에게 응답이 왔다.
            DoneRequest(webRequest, info);
        }
    }

    // 파일 업로드를 form-data로 처리하는 코루틴
    public IEnumerator UploadFileByFormData(HttpInfo info)
    {
        //info.data에 있는 파일을 byte 배열로 읽어오자
        byte[] data = File.ReadAllBytes(info.body);

        //data를 MultipartForm으로 세팅
        List<IMultipartFormSection> formData = new List<IMultipartFormSection>();
        formData.Add(new MultipartFormFileSection("file", data, "image.jpg", info.contentType));

        //유니티의 http 통신 기능
        using (UnityWebRequest webRequest = UnityWebRequest.Post(info.url, formData))
        {
            //서버에 요청 보내기
            yield return webRequest.SendWebRequest();

            //서버에게 응답이 왔다.
            DoneRequest(webRequest, info);
        }
    }

    //파일 업로드
    public IEnumerator UploadFileByByte(HttpInfo info)
    {
        //info.data에 있는 파일을 byte 배열로 읽어오자
        byte[] data = File.ReadAllBytes(info.body);

        //유니티의 http 통신 기능
        using (UnityWebRequest webRequest = new UnityWebRequest(info.url, "POST"))
        {
            //업로드하는 데이터
            webRequest.uploadHandler = new UploadHandlerRaw(data);
            webRequest.uploadHandler.contentType = info.contentType;

            //응답받는 데이터 공간
            webRequest.downloadHandler = new DownloadHandlerBuffer();

            //서버에 요청 보내기
            yield return webRequest.SendWebRequest();

            //서버에게 응답이 왔다.
            DoneRequest(webRequest, info);
        }
    }

    //스프라이트 받기
    public IEnumerator DownloadSprite(HttpInfo info)
    {
        using (UnityWebRequest webRequest = UnityWebRequestTexture.GetTexture(info.url))
        {
            yield return webRequest.SendWebRequest();

            DoneRequest(webRequest, info);
        }
    }

    //오디오 받기
    public IEnumerator DownloadAudio(HttpInfo info)
    {
        using (UnityWebRequest webRequest = UnityWebRequestMultimedia.GetAudioClip(info.url, AudioType.WAV))
        {
            yield return webRequest.SendWebRequest();

            DownloadHandlerAudioClip handler = webRequest.downloadHandler as DownloadHandlerAudioClip;
            //handler.audioClip; 을 Audiosource에 셋팅하고 플레이

            DoneRequest(webRequest, info);
        }
    }

    // 요청 완료 시 호출되는 메소드
    void DoneRequest(UnityWebRequest webRequest, HttpInfo info)
    {
        //만약에 결과가 정상이라면
        if (webRequest.result == UnityWebRequest.Result.Success)
        {
            //응답온 데이터를 요청한 클래스로 보내자.
            if (info.onComplete != null)
            {
                info.onComplete(webRequest.downloadHandler);

                ParseUserInfo(webRequest.downloadHandler);
            }
        }
        //그렇지 않다면 (Error 라면)
        else
        {
            //Error에 대한 이유 출력
            Debug.LogError("Net Error = " + webRequest.error);
        }
    }

    // 응답 데이터를 UserInfo 구조체로 변환하는 예시
    void ParseUserInfo(DownloadHandler downloadHandler)
    {
        string json = downloadHandler.text;
        UserInfo userInfo = JsonUtility.FromJson<UserInfo>(json);
        Debug.Log("User ID: " + userInfo.userId);
        Debug.Log("User Name: " + userInfo.userName);
        // 필요한 다른 필드들도 출력 가능
    }
}
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;
using static AccountDate;

public class FirstCanvasManager : MonoBehaviour
{
    public TMP_InputField texttId;
    public TMP_InputField textPassword;
    public TMP_InputField textName;
    public TMP_InputField textArea;
    public TextMeshProUGUI textFamilly;

    public TMP_InputField logintId;
    public TMP_InputField loginPassword;

    public GameObject joinFullset;
    public GameObject loginFullset;

    string loginUrl = "http://125.132.216.190:12450/api/auth/login";
    string joinUrl = "http://125.132.216.190:12450/api/auth/signup";

    void Update()
    {
        RenderSettings.skybox.SetFloat("_Rotation", Time.time * 1);
    }

    public void JoinFinishClick()
    {
        if(string.IsNullOrEmpty(texttId.text) || string.IsNullOrEmpty(textPassword.text) || string.IsNullOrEmpty(textName.text) || string.IsNullOrEmpty(textArea.text)) 
        {
            print("칸이 비었음");
            HttpManager.GetInstance().Alert("빈칸을 채워주세요.", 2.0f);
            return;
        }

        JoinJsonConvert();

        LoginPopUp();
    }

    public void LoginClick()
    {
        if (string.IsNullOrEmpty(logintId.text) || string.IsNullOrEmpty(loginPassword.text))
        {
            print("칸이 비었음");
            HttpManager.GetInstance().Alert("아이디 및 비밀번호를 입력해주세요.", 2.0f);
            return;
        }

        LoginJsonConvert();
    }

    void JoinJsonConvert()
    {
        // 전송할 데이터 객체 생성
        UserInfo userInfo = new UserInfo();
        userInfo.userId = texttId.text;
        userInfo.userPassword = textPassword.text;
        userInfo.userName = textName.text;
        userInfo.userArea = Convert.ToInt32(textArea.text);
        userInfo.userFamilly = Convert.ToInt32(textFamilly.text);

        // HttpInfo 객체 생성
        HttpInfo info = new HttpInfo();

        // 요청할 URL 설정
        info.url = joinUrl;

        // 전송할 데이터를 JSON 형식으로 변환하여 설정
        info.body = JsonUtility.ToJson(userInfo);

        print(info.body);

        // 콘텐츠 타입 설정
        info.contentType = "application/json";

        //델리게이트에 그냥 넣기 - 람다식 방식  - 지금 여기선 연산 단계 없음
        info.onComplete = (DownloadHandler downloadHandler) =>
        {
            // 서버로부터 받은 응답 출력
            print(downloadHandler.text);
        };

        // POST 요청을 위한 코루틴 실행
        StartCoroutine(HttpManager.GetInstance().Register(info));
    }

    void LoginJsonConvert()
    {
        // 전송할 데이터 객체 생성
        UserAccount accountInfo = new UserAccount();
        accountInfo.userId = logintId.text;
        accountInfo.userPassword = loginPassword.text;

        // HttpInfo 객체 생성
        HttpInfo info = new HttpInfo();

        // 요청할 URL 설정
        info.url = loginUrl;

        // 전송할 데이터를 JSON 형식으로 변환하여 설정
        info.body = JsonUtility.ToJson(accountInfo);

        print(info.body);

        // 콘텐츠 타입 설정
        info.contentType = "application/json";

        //델리게이트에 그냥 넣기 - 람다식 방식  - 지금 여기선 연산 단계 없음
        info.onComplete = (DownloadHandler downloadHandler) =>
        {
            // 서버로부터 받은 응답 출력
            print(downloadHandler.text);
        };

        // POST 요청을 위한 코루틴 실행
        StartCoroutine(HttpManager.GetInstance().Login(info));
    }

    public void JoinPopUp()
    {
        logintId.text = "";
        loginPassword.text = "";

        joinFullset.SetActive(true);
        loginFullset.SetActive(false);
    }

    public void LoginPopUp()
    {
        texttId.text = "";
        textPassword.text = "";
        textName.text = "";
        textArea.text = "";

        joinFullset.SetActive(false);
        loginFullset.SetActive(true);
    }
}

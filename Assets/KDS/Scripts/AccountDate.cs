using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.Networking;

public class AccountDate : MonoBehaviour
{
    //싱글톤 생성
    static AccountDate instance;

    // 싱글톤 인스턴스를 반환하는 메소드
    public static AccountDate GetInstance()
    {
        if (instance == null)
        {
            // 새로운 게임 오브젝트 생성
            GameObject go = new GameObject();

            // 이름 설정
            go.name = "HttpManager";

            // HttpManager 컴포넌트를 추가
            go.AddComponent<AccountDate>();
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

    // PostInfo 구조체 선언: 서버에서 가져온 데이터를 담기 위한 구조체
    [System.Serializable]
    public struct UserInfo
    {
        public string userId;
        public string userPassword;
        public string userName;
        public int userAge;
        public string userGender;
        public int userFamilly;
    }

    [System.Serializable]
    public struct UserAccount
    {
        public string userId;
        public string userPassword;
    }

    public void InAccount()
    {
        // 전송할 데이터 객체 생성
        UserInfo currentInfo = new UserInfo();
        currentInfo.userId = "";
        currentInfo.userPassword = "";
        currentInfo.userName = "";
        currentInfo.userAge = Convert.ToInt32("");
        currentInfo.userGender = "";
        currentInfo.userFamilly = Convert.ToInt32("");
    }
}


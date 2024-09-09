using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using Unity.VisualScripting.Antlr3.Runtime.Tree;
using UnityEngine;
using UnityEngine.Networking;

public class AccountDate : MonoBehaviour
{
    //싱글톤 생성
    public static AccountDate instance;

    // 싱글톤 인스턴스를 반환하는 메소드
    public static AccountDate GetInstance()
    {
        if (instance == null)
        {
            // 새로운 게임 오브젝트 생성
            GameObject go = new GameObject();

            // 이름 설정
            go.name = "AccountDate";

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

    // 회원 가입시 보낼 정보
    [System.Serializable]
    public struct UserInfo
    {
        public string userId;
        public string userPassword;
        public string userName;
        public int userArea;
        public int userFamilly;
    }

    //로그인 시 보낼 정보
    [System.Serializable]
    public struct UserAccount
    {
        public string userId;
        public string userPassword;
    }

    [System.Serializable]
    public struct Response
    {
        public string userId;
        public string userName;
        public string grantType;
        public string accessToken;
        public string accessTokenValidTime;
        public string refreshToken;
        public string refreshTokenValidTime;
    }

    [System.Serializable]
    public struct AccountSet
    {
        public bool success;
        public Response response;
        public string error;
    }

    public Response response = new Response();

    //로그인 후 정보 저장
    public void InAccount(string userId, string userName, string grantType, string accessToken, string accessTokenValidTime, string refreshToken, string refreshTokenValidTime)
    {
        response.userId = userId;
        response.userName = userName;
        response.grantType = grantType;
        response.accessToken = accessToken;
        response.accessTokenValidTime = accessTokenValidTime;
        response.refreshToken = refreshToken;
        response.refreshTokenValidTime = refreshTokenValidTime;
    }
}
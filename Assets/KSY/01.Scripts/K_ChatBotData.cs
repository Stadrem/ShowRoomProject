using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using static K_EventMethodRef;

public class K_ChatBotData : MonoBehaviour
{
    

    void Start()
    {
        
    }
    public ChatBotAns ans;
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            K_HttpInfo info = new K_HttpInfo();
            info.url = "http://meta-ai.iptime.org:8989/ask";
            ChatBotInput chat = new ChatBotInput();
            chat.user_id = "woosub";
            chat.question = "BESPOKE 냉장고 소비전력 알려줘";
            info.body = JsonUtility.ToJson(chat);
            info.contentType = "application/json";
            info.onComplete = (downloadHandler) => { 
                string jsonData = "{ \"data\" : " + downloadHandler.text + "}";
                print(downloadHandler.text);
                //JsonArray<ChatBotAns> allInfo = JsonUtility.FromJson<JsonArray<ChatBotAns>>(jsonData);
                ans = JsonUtility.FromJson<ChatBotAns>(downloadHandler.text);
                print(ans.answer);
            };
            StartCoroutine(K_HttpManager.GetInstance().Post(info));
        }
    }

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

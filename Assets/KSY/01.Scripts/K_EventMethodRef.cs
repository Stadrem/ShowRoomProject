using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class K_EventMethodRef : MonoBehaviour
{
    [System.Serializable]
    public class ChatBot
    {
        public string user_id;
        public string question;
    }

    [System.Serializable]
    public class ChatBotAns
    {
        public string answer;
    }
    public ChatBotAns ans;

    public TMP_InputField input;
    public TMP_InputField output;

    void Start()
    {
        
    }

    void Update()
    {
        
    }

    public void TransferInput()
    {
        HttpInfo info = new HttpInfo();
        info.url = "http://meta-ai.iptime.org:8989/ask";
        ChatBot chat = new ChatBot();
        chat.user_id = "woosub";
        chat.question = input.text;
        info.body = JsonUtility.ToJson(chat);
        info.contentType = "application/json";
        info.onComplete = (downloadHandler) => {
            print(downloadHandler.text);
            ans = JsonUtility.FromJson<ChatBotAns>(downloadHandler.text);
            print(ans.answer);
        };
        StartCoroutine(HttpManager.GetInstance().Post(info));
    }
}

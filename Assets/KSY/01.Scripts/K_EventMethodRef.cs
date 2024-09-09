﻿using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using static UnityEditor.Rendering.CameraUI;

public class K_EventMethodRef : MonoBehaviour
{
    [System.Serializable]
    public class ChatBotInput
    {
        public string question;
        public string member_id;
        public string area_size;
        public string housemate_num;
    }

    [System.Serializable]
    public class ChatBotAns
    {
        public int id;
        public string createdAt;
        public string member_id;
        public string member_name;
        public string answer;
        public string area_size;
        public string housemate_num;
    }
    [System.Serializable]
    public class ChatBotInput_AI
    {
        public string user_id;
        public string question;
        public string area_size;
        public string housemate_num;
    }
    [System.Serializable]
    public class ChatBotAns_AI
    {
        public string answer;
    }
    
    public ChatBotAns ans;
    public ChatBotAns_AI ansAI;
    public TMP_InputField input;
    public TMP_Text output;
    public TMP_Dropdown items;

    public bool ToAI = true;
    
    void Start()
    {
        
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            print("Caption : " + items.captionText.text);
        }
    }

    

    public void TransferInput()
    {
        K_HttpInfo info = new K_HttpInfo();
        ChatBotInput chat = new ChatBotInput();
        ChatBotInput_AI chatAI = new ChatBotInput_AI();
        if (AccountDate.GetInstance().response.userId == null || AccountDate.GetInstance().response.userId == "")
        {
            print("userId가 null이거나 빔.");
            chat.member_id = "0";
            chat.area_size = "8평";
            chat.housemate_num = "13";
            chatAI.user_id = "0";
            chatAI.area_size = "8평";
            chatAI.housemate_num = "13";

        }
        else
        {
            //chat.member_id = AccountDate.instance.currentInfo.userId;
            chatAI.user_id = "0";
            chatAI.area_size = "8평";
            chatAI.housemate_num = "13";
            chat.member_id = "0";
            chat.area_size = "8평";
            chat.housemate_num = "13";
            print("흠");
        }
        chat.question = input.text;
        chatAI.question = input.text;
        input.text = "처리중입니다...";
        output.text = "처리중입니다...";
        input.interactable = false;
        
        
        Debug.Log(info.body);
        info.contentType = "application/json";
        if (ToAI)
        {
            info.url = "http://meta-ai.iptime.org:8282/ask";
            info.token = "";
            info.body = JsonUtility.ToJson(chatAI);
            print("AI와 통신 중");
        }
        else if (!ToAI)
        {
            info.body = JsonUtility.ToJson(chat);
            info.url = "http://125.132.216.190:12450/api/talks";
            info.token = AccountDate.GetInstance().response.accessToken;
        }
        info.onComplete = (downloadHandler) => {
            print(downloadHandler.text);
            if (!ToAI)
            {
                ans = JsonUtility.FromJson<ChatBotAns>(downloadHandler.text);
                print(ans.answer);
                ans.answer = ans.answer.Replace("**", "  ");
                output.text = ans.answer;
            }
            else if (ToAI)
            {
                ansAI = JsonUtility.FromJson<ChatBotAns_AI>(downloadHandler.text);
                print(ansAI.answer);
                ansAI.answer = ansAI.answer.Replace("**", "  ");
                output.text = ansAI.answer;
            }
            
            
            input.text = "";
            input.interactable = true;
        };
        info.onError = (error) => { 
            print(error);
            output.text = "실패했습니다.";
            input.text = "";
            input.interactable = true;
        };
        StartCoroutine(K_HttpManager.GetInstance().Post(info));
    }

    public void InActive(GameObject gameObject)
    {
        gameObject.SetActive(false);
    }

    public void Active(GameObject gameObject)
    {
        gameObject.SetActive(true);
    }
}

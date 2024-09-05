using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class K_EventMethodRef : MonoBehaviour
{
    [System.Serializable]
    public class ChatBotInput
    {
        public string user_id;
        public string question;
        public string area_size;
        public string housemate_num;
    }

    [System.Serializable]
    public class ChatBotAns
    {
        public string answer;
    }
    public ChatBotAns ans;

    public TMP_InputField input;
    public TMP_Text output;
    public TMP_Dropdown items;

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
        info.url = "http://125.132.216.190:12450/api/talks";
        
        ChatBotInput chat = new ChatBotInput();
        if (AccountDate.GetInstance().currentInfo.userId == null || AccountDate.GetInstance().currentInfo.userId == "")
        {
            chat.user_id = "woosub";
            chat.area_size = "8평";
            chat.housemate_num = "13";
        }
        else
        {
            chat.user_id = AccountDate.instance.currentInfo.userId;
        }
        chat.question = input.text;
        input.text = "처리중입니다...";
        output.text = "처리중입니다...";
        input.interactable = false;
        info.token = AccountDate.GetInstance().response.accessToken;
        info.body = JsonUtility.ToJson(chat);
        info.contentType = "application/json";
        info.onComplete = (downloadHandler) => {
            print(downloadHandler.text);
            ans = JsonUtility.FromJson<ChatBotAns>(downloadHandler.text);
            print(ans.answer);
            output.text = ans.answer;
            input.text = "";
            input.interactable = true;
        };
        StartCoroutine(K_HttpManager.GetInstance().Post(info));
    }

    public void Close(GameObject gameObject)
    {
        gameObject.SetActive(false);
    }
}

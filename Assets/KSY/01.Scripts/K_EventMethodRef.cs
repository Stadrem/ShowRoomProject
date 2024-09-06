using System.Collections;
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
        public string memberName;
        public string question;
        public string createdAt;
        public int member_id;
        public string area_size;
        public string housemate_num;
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
            print("userId가 null이거나 빔.");
            chat.member_id = "0";
            chat.area_size = "8평";
            chat.housemate_num = "13";
        }
        else
        {
            //chat.member_id = AccountDate.instance.currentInfo.userId;
            chat.member_id = "0";
            chat.area_size = "8평";
            chat.housemate_num = "13";
            print("흠");
        }
        chat.question = input.text;
        input.text = "처리중입니다...";
        output.text = "처리중입니다...";
        input.interactable = false;
        info.token = AccountDate.GetInstance().response.accessToken;
        info.body = JsonUtility.ToJson(chat);
        Debug.Log(info.body);
        info.contentType = "application/json";
        info.onComplete = (downloadHandler) => {
            print(downloadHandler.text);
            ans = JsonUtility.FromJson<ChatBotAns>(downloadHandler.text);
            print(ans.question);
            output.text = ans.question;
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

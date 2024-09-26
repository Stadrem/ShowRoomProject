using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Networking;
[System.Serializable]
public class ChatBotInput
{
    public string question;
    public string user_id;
    public string area_size;
    public string housemate_num;
}

[System.Serializable]
public class ChatBotAns
{
    public string answer;
    public string createdAt;
    public string userId;
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

[System.Serializable]
public class RefrigeratorData
{
    public string productName; // 제품명
    public string productType; // 제품 타입
    public string installationType; // 설치 타입
    public string dimensions; // 크기
    public double weight; // 무게
    public int totalCapacity; // 전체 용량
    public int fridgeCapacity; // 냉장실 용량
    public int freezerCapacity; // 냉동실 용량
    public string energyEfficiencyRating;// 소비 효율 등급
    public double powerConsumption; // 소비전력
}

[System.Serializable]
public struct RefriArray
{
    public List<RefrigeratorData> data;
}

public class K_EventMethodRef : MonoBehaviour
{
    


    public ChatBotAns ans;
    public ChatBotAns_AI ansAI;
    public TMP_InputField input;
    public TMP_Text output;
    public TMP_Dropdown items;

    public bool ToAI = true;

    string rf1 = "RS84B5081SA";
    string rf2 = "RF90DG9111S9";

    void Start()
    {
        GetRefriData();
    }
    
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            print("Caption : " + items.captionText.text);
        }
        if (Input.GetKeyDown(KeyCode.N))
        {
            GetRefriData();
            //print("refriArray 1번 : " + refriArray.data[1]);
        }
    }
    public K_ScriptableObjTest scriptableObj;
    public RefriArray refriArray;
    public void GetRefriData()
    {
        K_HttpInfo info = new K_HttpInfo();
        info.url = "http://125.132.216.190:12450/api/refrigerators";
        info.token = AccountDate.GetInstance().response.accessToken;
        info.onComplete = (downloadHandler) =>
        {
            string jsonData = "{ \"data\" : " + downloadHandler.text + "}";
            print(downloadHandler.text);
            refriArray = JsonUtility.FromJson<RefriArray>(jsonData);
            print("refriArray 0번 : " + refriArray.data[0].productName);
            foreach(var c in refriArray.data)
            {
                scriptableObj.dic.Add(c.productName, c);
            }
            scriptableObj.SetData();
        };
        StartCoroutine(K_HttpManager.GetInstance().Get(info));
    }
    
    public void SaveDataToScriptableObj()
    {

    }

    public void TransferInput()
    {
        K_HttpInfo info = new K_HttpInfo();
        ChatBotInput chat = new ChatBotInput();
        ChatBotInput_AI chatAI = new ChatBotInput_AI();
        if (AccountDate.GetInstance().response.userId == null || AccountDate.GetInstance().response.userId == "")
        {
            print("userId가 null이거나 빔.");
            chat.user_id = "0";
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
            chat.user_id = AccountDate.GetInstance().response.userId;
            chat.area_size = "30평";
            chat.housemate_num = "4";
            print("흠");
        }
        chat.question = input.text;
        chatAI.question = input.text;
        input.text = "처리중입니다...";
        output.text = "처리중입니다...";
        input.interactable = false;
        
        info.contentType = "application/json";
        if (ToAI)
        {
            info.url = "http://metaai2.iptime.org:8282/ask";
            info.token = "";
            info.body = JsonUtility.ToJson(chatAI);
            print("AI와 통신 중");
        }
        else if (!ToAI)
        {
            info.body = JsonUtility.ToJson(chat);
            info.url = "http://125.132.216.190:12450/api/talks";
            info.token = AccountDate.GetInstance().response.accessToken;
            Debug.Log(info.body);
        }
        info.onComplete = (downloadHandler) => {
            print(downloadHandler.text);
            if (!ToAI)
            {
                ans = JsonUtility.FromJson<ChatBotAns>(downloadHandler.text);
                print(ans.answer);
                ans.answer = ans.answer.Replace("**", "  ");
                if (C_TextPrint != null) StopCoroutine(C_TextPrint);
                C_TextPrint = StartCoroutine(TextPrint(output, ans.answer, 0.05f));
                
                if (ans.answer.Contains(rf1))
                {
                    K_UIManager.GetInstance().objectControl.SetObject(1);
                }
                else if (ans.answer.Contains(rf2))
                {
                    K_UIManager.GetInstance().objectControl.SetObject(2);
                }
                //output.text = ans.answer;
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
    Coroutine C_TextPrint;
    IEnumerator TextPrint(TMP_Text output, string input, float delay)
    {
        output.text = "";
        foreach (char c in input)
        {
            if(c == '\n')
            {
                output.text += System.Environment.NewLine;
            }
            else
            {
                output.text += c;
            }
            yield return new WaitForSeconds(delay);
        }
        if (C_TextPrint != null) C_TextPrint = null;
    }
    //IEnumerator TextPrint(TMP_Text output, string input, float delay)
    //{
    //    int count = 0;
    //    output.text = "";
    //    while (count != input.Length)
    //    {
    //        if (count < input.Length)
    //        {
    //            output.text += input[count];
    //            count++;
    //        }
    //        yield return new WaitForSeconds(delay);
    //    }
    //    if (C_TextPrint != null) C_TextPrint = null;
    //}

    public void InActive(GameObject gameObject)
    {
        gameObject.SetActive(false);
    }

    public void Active(GameObject gameObject)
    {
        gameObject.SetActive(true);
    }
}

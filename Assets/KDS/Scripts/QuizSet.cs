using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class QuizSet : MonoBehaviour
{
    public static QuizSet instance;

    private void Awake()
    {
        if (instance == null)
        {
            // 인스턴스 설정
            instance = this;

        }
        else
        {
            // 이미 인스턴스가 존재하면 현재 객체를 파괴
            Destroy(gameObject);
        }
    }

    public TMP_Text text_q;
    public TMP_Text text_hint;
    public Slider slider_time;
    public Button btn_enter;

    [System.Serializable]
    public struct QuizArray
    {
        public QuizInfo quizInfo;
    }

    [System.Serializable]
    public struct QuizInfo
    {
        public string quiz;
        public string hint;
        public string answer;
    }

    public QuizArray[] quizArray =new QuizArray[0];

    public void QuizStart()
    {

    }

}

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
    public TMP_InputField InputField_answer;

    public Slider slider_time;
    public Button btn_enter;
    public GameObject failPopup;
    public GameObject wrongAnswerPopup;
    public GameObject okAnswerPopup;

    string currentAnswer = "";

    bool quizTimerStart = false;

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

    private void Update()
    {
        if (quizTimerStart)
        {
            slider_time.value = Mathf.Clamp(slider_time.value - 0.05f * Time.deltaTime, 0, 1);
        }

        if(slider_time.value <= 0.01f)
        {
            failPopup.SetActive(true);
            StartCoroutine(failTimer());
        }
    }
    IEnumerator failTimer()
    {
        quizTimerStart = false;

        slider_time.value = 1;

        yield return new WaitForSeconds(5.0f);

        failPopup.SetActive(false);
        gameObject.SetActive(false);
    }

    IEnumerator WrongAnswer()
    {
        wrongAnswerPopup.SetActive(true);

        yield return new WaitForSeconds(3.0f);

        wrongAnswerPopup.SetActive(false);
    }

    public QuizArray[] SelectQuiz = new QuizArray[3];

    public void QuizStart()
    {
        // 배열 선언
        int[] quizNumArray = new int[3];

        // 0~5의 숫자를 리스트로 만듦
        List<int> availableNumbers = new List<int> { 0, 1, 2, 3, 4, 5 };

        // 랜덤 객체 생성
        System.Random random = new System.Random();

        // 중복되지 않는 숫자를 배열에 채워 넣음
        for (int i = 0; i < quizNumArray.Length; i++)
        {
            // 랜덤한 인덱스를 선택
            int randomIndex = random.Next(availableNumbers.Count);

            // 선택된 숫자를 배열에 넣음
            quizNumArray[i] = availableNumbers[randomIndex];

            // 사용한 숫자를 리스트에서 제거하여 중복 방지
            availableNumbers.RemoveAt(randomIndex);

            SelectQuiz[i].quizInfo.quiz = quizArray[quizNumArray[i]].quizInfo.quiz;
            SelectQuiz[i].quizInfo.hint = quizArray[quizNumArray[i]].quizInfo.hint;
            SelectQuiz[i].quizInfo.answer = quizArray[quizNumArray[i]].quizInfo.answer;
        }

        quizTimerStart = true;

        text_q.text = SelectQuiz[0].quizInfo.quiz;
        text_hint.text = SelectQuiz[0].quizInfo.hint;
        currentAnswer = SelectQuiz[0].quizInfo.answer;
    }

    public void QuizAnswerEnter()
    {
        if(InputField_answer.text == currentAnswer)
        {

        }
        else
        {
            StartCoroutine(WrongAnswer());
            InputField_answer.text = "";
        }
    }
}

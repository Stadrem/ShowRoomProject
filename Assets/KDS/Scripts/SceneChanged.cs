using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChanged : MonoBehaviour
{
    public static SceneChanged instance;

    bool isFirstSceneLoad = true;

    public bool isSecondScene = false;

    void Awake()
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

        SceneManager.activeSceneChanged += OnSceneChanged;
    }

    private void OnSceneChanged(Scene previousScene, Scene newScene)
    {
        if (isFirstSceneLoad)
        {
            // 첫 씬 로드 이벤트 무시
            isFirstSceneLoad = false;
            return; 
        }

        foreach (Transform child in transform)
        {
            child.gameObject.SetActive(true);
        }

        isSecondScene = true;

        StartCoroutine(Loading());
    }

    IEnumerator Loading()
    {
        yield return new WaitForSeconds(0.5f);

        UiSoundManager.instance.playerMove = AccountDate.GetInstance().player.GetComponent<K_PlayerMove>();
    }
}

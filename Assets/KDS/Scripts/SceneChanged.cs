using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChanged : MonoBehaviour
{
    private bool isFirstSceneLoad = true;

    void Awake()
    {
        SceneManager.activeSceneChanged += OnSceneChanged;

        // 씬 전환 시 객체 파괴 방지
        DontDestroyOnLoad(gameObject);

    }

    private void OnSceneChanged(Scene previousScene, Scene newScene)
    {
        if (isFirstSceneLoad)
        {
            isFirstSceneLoad = false;
            return; // 첫 씬 로드 이벤트 무시
        }

        foreach (Transform child in transform)
        {
            child.gameObject.SetActive(true);
        }
    }
}

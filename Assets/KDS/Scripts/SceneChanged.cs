using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChanged : MonoBehaviour
{
    void Awake()
    {
        SceneManager.activeSceneChanged += OnSceneChanged;

        // 씬 전환 시 객체 파괴 방지
        DontDestroyOnLoad(gameObject);

    }

    private void OnSceneChanged(Scene previousScene, Scene newScene)
    {
        foreach (Transform child in transform)
        {
            child.gameObject.SetActive(true);
        }
    }
}

using Cinemachine;
using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class K_UIManager : MonoBehaviour
{
    private static K_UIManager instance;
    public GameObject player;
    public PhotonView playerPV;
    public GameObject img_Aim;
    public GameObject currentUI;

    void Awake()
    {
        if (instance == null) instance = this;
        else Destroy(gameObject);
    }

    void Update()
    {
        
    }

    public void OpenUI()
    {
        currentUI.SetActive(true);
    }

    // 카메라 포커스 함수. 각 UIController에서 호출
    public void OnCamFocusIn(GameObject virtualCam)
    {
        var cvc = virtualCam.GetComponent<CinemachineVirtualCamera>();
        cvc.Priority = 11;
    }

    public void OnCamFocusOut(GameObject virtualCam)
    {
        var cvc = virtualCam.GetComponent<CinemachineVirtualCamera>();
        cvc.Priority = 9;
    }

    public static K_UIManager GetInstance()
    {
        if(instance == null)
        {
            new GameObject("UIManager", typeof(K_UIManager));
        }
        return instance;
    }

    public void FindPlayer()
    {
        player = AccountDate.GetInstance().player;
    }

    public void Enabled_UI()
    {
        AccountDate.GetInstance().SetPlayerState(K_PlayerMove.PlayerState.Click);
        img_Aim.SetActive(false);
    }

    public void Disabled_UI()
    {
        AccountDate.GetInstance().SetPlayerState(K_PlayerMove.PlayerState.Move);
        img_Aim.SetActive(true);
    }
}

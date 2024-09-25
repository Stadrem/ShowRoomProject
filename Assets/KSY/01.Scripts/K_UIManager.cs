using Cinemachine;
using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class K_UIManager : MonoBehaviour
{
    private static K_UIManager instance;
    public GameObject player;
    public PhotonView playerPV;
    public GameObject img_Aim;
    public GameObject ui_ObjGuide;
    public GameObject currentUI;

    public SkinnedMeshRenderer rf1_MeshRenderer;
    public SkinnedMeshRenderer rf2_MeshRenderer;
    public Material[] mat_Rf1_Doors = new Material[3];
    public Material[] mat_Rf2_Doors = new Material[4];
    public Dictionary<Material, Color> dic_DoorsColor = new Dictionary<Material, Color>();
    public Color[] colors = new Color[3];
    public Image[] buttonsImage = new Image[3];
    public GameObject[] bg_Buttons = new GameObject[3];
    int previousIdx = -1;

    void Awake()
    {
        if (instance == null) instance = this;
        else Destroy(gameObject);
    }

    public void IndicateSelectedColor(int num)
    {
        if(previousIdx != -1)
        {
            bg_Buttons[previousIdx].SetActive(false);
        }
        bg_Buttons[num].SetActive(true);
    }

    private void Start()
    {
        for(int i =0; i < 3; i++)
        {
            colors[i] = buttonsImage[i].color;
        }
        InitSaveMats();
    }

    void InitSaveMats()
    {
        mat_Rf1_Doors[0] = rf1_MeshRenderer.materials[1];
        mat_Rf1_Doors[1] = rf1_MeshRenderer.materials[2];
        mat_Rf1_Doors[2] = rf1_MeshRenderer.materials[11];
        mat_Rf2_Doors[0] = rf2_MeshRenderer.materials[11];
        mat_Rf2_Doors[1] = rf2_MeshRenderer.materials[5];
        mat_Rf2_Doors[2] = rf2_MeshRenderer.materials[12];
        mat_Rf2_Doors[3] = rf2_MeshRenderer.materials[1];
        foreach(var c in mat_Rf1_Doors)
        {
            c.color = colors[0];
            dic_DoorsColor.Add(c, c.color);
        }
        foreach (var c in mat_Rf2_Doors)
        {
            c.color = colors[0];
            dic_DoorsColor.Add(c, c.color);
        }
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
        img_Aim.SetActive(false);
        ui_ObjGuide.SetActive(false);
        AccountDate.GetInstance().SetPlayerState(K_PlayerMove.PlayerState.Click);
    }

    public void Disabled_UI()
    {
        img_Aim.SetActive(true);
        ui_ObjGuide.SetActive(true);
        AccountDate.GetInstance().SetPlayerState(K_PlayerMove.PlayerState.Move);
    }
}

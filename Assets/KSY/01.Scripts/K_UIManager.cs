using Cinemachine;
using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class K_UIManager : MonoBehaviour
{
    private static K_UIManager instance;

    public GameObject img_Aim;
    public GameObject ui_ObjGuide;

    public SkinnedMeshRenderer rf1_MeshRenderer;
    public SkinnedMeshRenderer rf2_MeshRenderer;
    public Material[] mat_Rf1_Doors = new Material[3];
    public Material[] mat_Rf2_Doors = new Material[4];
    public Material selectedMat;
    public Dictionary<Material, int> dic_DoorsColor = new Dictionary<Material, int>();
    public Color[] colors = new Color[3];
    public Image[] buttonsImage = new Image[3];
    public GameObject[] bg_Buttons = new GameObject[3];
    int previousIdx = 0;

    public TMP_Text[] txt_Data;
    public K_ScriptableObjTest data;

    void Awake()
    {
        if (instance == null) instance = this;
        else Destroy(gameObject);
    }

    public void SetSelectedMat(int rf, int idx)
    {
        if(rf == 1)
        {
            selectedMat = mat_Rf1_Doors[idx];
        }
        else if(rf == 2)
        {
            selectedMat = mat_Rf2_Doors[idx];
        }
        if (dic_DoorsColor.TryGetValue(selectedMat, out int value))
        {
            IndicateSelectedColor(value);
        }
    }

    public void IndicateSelectedColor(int num)
    {
        if(previousIdx != -1)
        {
            bg_Buttons[previousIdx].SetActive(false);
        }
        bg_Buttons[num].SetActive(true);
        previousIdx = num;
    }

    public void ClickedButton(int num)
    {
        IndicateSelectedColor(num);
        if (selectedMat != null)
        {
            selectedMat.color = colors[num];
            dic_DoorsColor.Remove(selectedMat);
            dic_DoorsColor.Add(selectedMat, num);
        }
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
            dic_DoorsColor.Add(c, 0);
        }
        foreach (var c in mat_Rf2_Doors)
        {
            c.color = colors[0];
            dic_DoorsColor.Add(c, 0);
        }
    }

    void Update()
    {
        
    }


    // 카메라 포커스 함수. 각 UIController에서 호출
    public void OnCamFocusIn(GameObject virtualCam)
    {
        var cvc = virtualCam.GetComponent<CinemachineVirtualCamera>();
        cvc.Priority = 11;
        Camera.main.cullingMask = Camera.main.cullingMask & ~(1 << LayerMask.NameToLayer("Player"));
    }

    public void OnCamFocusOut(GameObject virtualCam)
    {
        var cvc = virtualCam.GetComponent<CinemachineVirtualCamera>();
        cvc.Priority = 9;
        Camera.main.cullingMask |= 1 << LayerMask.NameToLayer("Player");
    }

    public static K_UIManager GetInstance()
    {
        if(instance == null)
        {
            new GameObject("UIManager", typeof(K_UIManager));
        }
        return instance;
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

    public void SetData(string productName)
    {
        RefrigeratorData data = new RefrigeratorData();
        if (this.data.dic.TryGetValue(productName, out data))
        {
            txt_Data[0].text = data.productName;
            txt_Data[1].text = data.productType;
            txt_Data[2].text = data.installationType;
            txt_Data[3].text = data.dimensions.ToString().Replace("×", "*");
            txt_Data[4].text = data.weight.ToString();
            txt_Data[5].text = data.totalCapacity.ToString();
            txt_Data[6].text = data.fridgeCapacity.ToString();
            txt_Data[7].text = data.freezerCapacity.ToString();
            txt_Data[8].text = data.energyEfficiencyRating;
            txt_Data[9].text = data.powerConsumption.ToString();
            print("제품명 : " + data.productName + " 입니다.");
        }
        else
        {
            Debug.LogError("제품명이 맞지 않습니다.");
        }
    }





}

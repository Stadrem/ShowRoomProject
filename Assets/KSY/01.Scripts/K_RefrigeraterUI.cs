using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class K_RefrigeraterUI : MonoBehaviour
{
    public TMP_Text[] txt_Data;
    public K_ScriptableObjTest data;
    void Start()
    {
        
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.T))
        {
            SetData("BESPOKE 냉장고 4도어 902L");
        }
    }
    //productType;
    //productName;
    //installationType;
    //dimensions;
    //weight;
    //totalCapacity;
    //fridgeCapacity;
    //freezerCapacity;
    //energyEfficiencyRating;
    //powerConsumption;
    void SetData(string productName)
    {
        RefrigeratorData data = new RefrigeratorData();
        if(this.data.dic.TryGetValue(productName, out data))
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

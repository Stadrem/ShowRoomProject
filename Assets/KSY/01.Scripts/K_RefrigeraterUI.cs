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
        
    }

    void SetData(string productName)
    {
        RefrigeratorData data = this.data.dic.GetValueOrDefault(productName);
        int index = 0;
        
    }
}

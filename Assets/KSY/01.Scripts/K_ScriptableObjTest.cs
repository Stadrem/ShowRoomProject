using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ProductData", menuName = "Custom/Data")]
public class K_ScriptableObjTest : ScriptableObject
{
    public Dictionary<string, RefrigeratorData> dic = new Dictionary<string, RefrigeratorData>();

    public RefrigeratorData[] list;

    public void SetData()
    {
        if(dic.Count != 0)
        {
            list = new RefrigeratorData[dic.Count];
            int index = 0;
            foreach(var c in dic.Values)
            {
                list[index++] = c;
            }
        }
    }
}



using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class K_ForTest : MonoBehaviour
{
    void Start()
    {
        
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha6))
        {
            K_UIManager.GetInstance();
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class K_OverlayCanvasEvent : MonoBehaviour
{
    void Start()
    {
        
    }

    void Update()
    {
        
    }

    public void OpenURLWeb()
    {
        Application.OpenURL(K_UIManager.GetInstance().url);
    }


}

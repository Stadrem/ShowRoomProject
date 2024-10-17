using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OverlayCanvasEvent : MonoBehaviour
{
    void Start()
    {
        
    }

    void Update()
    {
        
    }

    public void OpenURLWeb()
    {
        Application.OpenURL(UIManager.GetInstance().url);
    }


}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class K_OverlayCanvasEvent : MonoBehaviour
{
    public string webURL;
    void Start()
    {
        
    }

    void Update()
    {
        
    }

    public void OpenURLWeb()
    {
        Application.OpenURL(webURL);
    }


}

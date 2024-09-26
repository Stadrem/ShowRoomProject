using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class K_UIController : MonoBehaviour
{
    public Button[] btn_Panels;
    public GameObject[] panels;
    int previousIndex = -1;
    public GameObject virtualCam;

    public ColorBlock normal;
    public ColorBlock selected;

    void Start()
    {
        normal = btn_Panels[0].colors;
        selected = normal;
        selected.normalColor = selected.selectedColor;
        btn_Panels[0].Select();
        btn_Panels[0].onClick.Invoke();
        gameObject.SetActive(false);
    }


    public void ActivePanel(int num)
    {
        if(previousIndex != -1)
        {
            panels[previousIndex].SetActive(false);
            btn_Panels[previousIndex].colors = normal;
        }
        previousIndex = num;
        panels[num].SetActive(true);
        btn_Panels[num].colors = selected;
    }

    private void OnEnable()
    {
        K_UIManager.GetInstance().Enabled_UI();
        K_UIManager.GetInstance().OnCamFocusIn(virtualCam);
        GameUiCanvas.instance.gameObject.SetActive(false);
    }

    private void OnDisable()
    {
        K_UIManager.GetInstance().Disabled_UI();
        K_UIManager.GetInstance().OnCamFocusOut(virtualCam);
        GameUiCanvas.instance.gameObject.SetActive(true);
    }

}

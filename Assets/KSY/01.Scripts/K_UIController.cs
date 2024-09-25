using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class K_UIController : MonoBehaviour
{
    public Button[] btn_Panels;
    public GameObject[] panels;
    GameObject previousPanel = null;
    

    void Start()
    {
        btn_Panels[0].Select();
        btn_Panels[0].onClick.Invoke();
    }

    public void ActivePanel(int num)
    {
        previousPanel?.SetActive(false);
        panels[num].SetActive(true);
        previousPanel = panels[num];
    }

}

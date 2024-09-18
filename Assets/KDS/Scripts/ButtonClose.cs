using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonClose : MonoBehaviour
{
    public Button closeButton;
    public GameObject closePrefab;

    public void WindowClose()
    {
        closePrefab.SetActive(false);
    }
}

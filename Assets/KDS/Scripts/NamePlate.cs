using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class NamePlate : MonoBehaviour
{
    public TMP_Text nameText;

    public void NewNamePlate(string name)
    {
        nameText.text = name;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ButtonObj : MonoBehaviour
{
    public UnityEvent buttonAction;
    public void ActionButton()
    {
        buttonAction.Invoke();
    }
}

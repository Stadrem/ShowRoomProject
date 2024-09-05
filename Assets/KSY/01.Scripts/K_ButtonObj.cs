using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class K_ButtonObj : MonoBehaviour
{
    public UnityEvent buttonAction;
    public void ActionButton()
    {
        buttonAction.Invoke();
    }
}

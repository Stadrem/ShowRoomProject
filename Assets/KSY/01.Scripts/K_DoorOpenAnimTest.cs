using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class K_DoorOpenAnimTest : MonoBehaviour
{
    public bool isOpen = false;
    Vector3 openRot = new Vector3(0, 155, 0);
    Vector3 closeRot = new Vector3(0, 0, 0);
    Transform parent;


    void Start()
    {
        parent = transform.parent;
    }

    void Update()
    {
        if (isOpen)
        {
            parent.localEulerAngles = Vector3.Lerp(parent.localEulerAngles, openRot, Time.deltaTime);
            if (parent.localEulerAngles.y < 1 && parent.localEulerAngles.y >=0) parent.localEulerAngles = openRot;
        }
        else if (!isOpen)
        {
            parent.localEulerAngles = Vector3.Lerp(parent.localEulerAngles, closeRot, 1);
            if (parent.localEulerAngles.y > 154 && parent.localEulerAngles.y <= 155) parent.localEulerAngles = closeRot;
        }
    }
}

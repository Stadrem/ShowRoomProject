using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class K_PlayerEvent : MonoBehaviour
{
    public float maxDistance;


    void Start()
    {
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = new Ray(Camera.main.transform.position, Camera.main.transform.forward);
            RaycastHit hitInfo;
            if (Physics.Raycast(ray, out hitInfo, maxDistance, 1 << LayerMask.NameToLayer("Object")))
            {
                //OpenObjectUI(hitInfo.transform);
                K_DoorOpenAnimTest doa = hitInfo.transform.GetComponent<K_DoorOpenAnimTest>();
                if(doa != null) doa.isOpen = !doa.isOpen;
            }
        }
        
    }

    void OpenObjectUI(Transform go)
    {
        K_Object ob = go.GetComponent<K_Object>();
        ob.OpenUI();
    }

}

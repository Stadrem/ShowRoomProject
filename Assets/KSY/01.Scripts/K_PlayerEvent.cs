using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class K_PlayerEvent : MonoBehaviour
{
    public float maxDistance;
    public GameObject ItemPreviewUI;
    K_PlayerMove playerMove;

    void Start()
    {
        playerMove = GetComponent<K_PlayerMove>();
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            OpenUI();
        }
        
    }

    void OpenObjectUI(Transform go)
    {
        K_Object ob = go.GetComponent<K_Object>();
        ob.OpenUI();
    }

    void OpenUI()
    {
        if (playerMove.currState == K_PlayerMove.PlayerState.Click) return;
        Ray ray = new Ray(Camera.main.transform.position, Camera.main.transform.forward);
        RaycastHit hitInfo;
        if (Physics.Raycast(ray, out hitInfo, maxDistance, 1 << LayerMask.NameToLayer("Object")))
        {
            ItemPreviewUI.SetActive(true);
            playerMove.ChangeState(K_PlayerMove.PlayerState.Click);
            //Cursor.lockState = CursorLockMode.Confined;
            //OpenObjectUI(hitInfo.transform);
            //K_DoorOpenAnimTest doa = hitInfo.transform.GetComponent<K_DoorOpenAnimTest>();
            //if(doa != null) doa.isOpen = !doa.isOpen;
        }
    }

}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerEvent : MonoBehaviour
{
    public float maxDistance;
    public GameObject ItemPreviewUI;
    PlayerMove playerMove;

    void Start()
    {
        playerMove = GetComponent<PlayerMove>();
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
        ObjectsUI ob = go.GetComponent<ObjectsUI>();
        ob.OpenUI();
    }

    void OpenUI()
    {
        if (playerMove.currState == PlayerMove.PlayerState.Click) return;
        Ray ray = new Ray(Camera.main.transform.position, Camera.main.transform.forward);
        RaycastHit hitInfo;
        if (Physics.Raycast(ray, out hitInfo, maxDistance, 1 << LayerMask.NameToLayer("Object")))
        {
            ItemPreviewUI.SetActive(true);
            playerMove.ChangeState(PlayerMove.PlayerState.Click);
            //Cursor.lockState = CursorLockMode.Confined;
            //OpenObjectUI(hitInfo.transform);
            //K_DoorOpenAnimTest doa = hitInfo.transform.GetComponent<K_DoorOpenAnimTest>();
            //if(doa != null) doa.isOpen = !doa.isOpen;
        }
    }

}

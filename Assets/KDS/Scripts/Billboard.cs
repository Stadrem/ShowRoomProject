using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Billboard : MonoBehaviour
{
    private Camera mainCamera;
    PhotonView pv;

    public TMP_Text nameText;

    void Start()
    {
        mainCamera = Camera.main;
        pv = GetComponentInParent<PhotonView>();
        nameText.text = pv.Owner.NickName;
    }

    void LateUpdate()
    {
        transform.LookAt(transform.position + mainCamera.transform.rotation * Vector3.forward, mainCamera.transform.rotation * Vector3.up);
    }
}

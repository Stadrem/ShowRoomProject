using Photon.Pun;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class K_ObjectControl : MonoBehaviour
{
    public GameObject myUI;
    public bool inPlayer;
    GameObject player;
    public GameObject specUI;
    public GameObject rf1;
    public GameObject rf2;
    string rf1_ProductName = "양문형 냉장고 846L";
    string rf2_ProductName = "BESPOKE 냉장고 4도어 902L";

    void Start()
    {
        player = GameObject.FindWithTag("Player");
    }

    void Update()
    {
        if(player != null)
        {
            myUI.transform.forward = player.transform.forward;
            PhotonView photonView = player.GetPhotonView();
        }
        if (Input.GetKeyDown(KeyCode.F))
        {
            specUI.SetActive(true);
            if (rf1.activeSelf)
            {
                K_UIManager.GetInstance().SetData(rf1_ProductName);
            }
            else if (rf2.activeSelf)
            {
                K_UIManager.GetInstance().SetData(rf2_ProductName);
            }
        }
        if (Input.GetKeyDown(KeyCode.Q))
        {
            if (rf1.activeSelf)
            {
                rf1.SetActive(false);
                rf2.SetActive(true);
                K_UIManager.GetInstance().SetData(rf2_ProductName);
            }
            else if (rf2.activeSelf)
            {
                rf2.SetActive(false);
                rf1.SetActive(true);
                K_UIManager.GetInstance().SetData(rf1_ProductName);
            }
        }
        if (rf1.activeSelf)
        {
            if (Input.GetKeyDown(KeyCode.R))
            {
                rf1.GetComponent<Animator>().SetTrigger("Door");
            }
        }
        else if (rf2.activeSelf)
        {
            if (Input.GetKeyDown(KeyCode.R))
            {
                rf2.GetComponent<Animator>().SetTrigger("Door");
            }
        }
        if (player == null) player = GameObject.FindWithTag("Player");
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            inPlayer = true;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            inPlayer = false;
        }
    }

}

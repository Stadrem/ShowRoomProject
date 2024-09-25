using Photon.Pun;
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
        }
        if (Input.GetKeyDown(KeyCode.Q))
        {
            if (rf1.activeSelf)
            {
                rf1.SetActive(false);
                rf2.SetActive(true);
            }
            else if (rf2.activeSelf)
            {
                rf2.SetActive(false);
                rf1.SetActive(true);
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

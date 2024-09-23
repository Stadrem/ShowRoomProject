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

    public int animState_r1 = 0;
    public int animState_r2 = 0;

    void Start()
    {
        player = GameObject.FindWithTag("Player");

    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F))
        {
            specUI.SetActive(true);
        }
        if(player != null)
        {
            myUI.transform.forward = player.transform.forward;
            PhotonView photonView = player.GetPhotonView();
        }
        
        if (Input.GetKeyDown(KeyCode.R))
        {

            
        }
        if (rf1.activeSelf)
        {
            rf1.GetComponent<Animator>().SetFloat("OPENCONTROL", animState_r1, 0.1f, Time.deltaTime);
            if (Input.GetKeyDown(KeyCode.R))
            {
                GetAnimFloat(1);
            }
        }
        else if (rf2.activeSelf)
        {
            rf2.GetComponent<Animator>().SetFloat("OPENCONTROL", animState_r2, 0.1f, Time.deltaTime);
            if (Input.GetKeyDown(KeyCode.R))
            {
                GetAnimFloat(2);
            }
        }
        if (player == null) player = GameObject.FindWithTag("Player");
        if (inPlayer)
        {
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
            if (Input.GetKeyDown(KeyCode.F))
            {
                specUI.SetActive(true);
            }
        }
        else
        {

        }

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

    public void GetAnimFloat(int rf_num)
    {
        if(rf_num == 1)
        {
            animState_r1++;
            if(animState_r1 > 2)
            {
                animState_r1 = 0;
                rf1.GetComponent<Animator>().SetFloat("OPENCONTROL", 0);
            }
        }
        if(rf_num == 2)
        {
            animState_r2++;
            if (animState_r2 > 2)
            {
                animState_r2 = 0;
                rf2.GetComponent<Animator>().SetFloat("OPENCONTROL", 0);
            }
        }

    }

}

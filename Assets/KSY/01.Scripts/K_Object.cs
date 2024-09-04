using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class K_Object : MonoBehaviour
{
    Transform myUIPos;
    Transform player;
    Vector3 dir;
    public float dist;
    void Start()
    {
        myUIPos = transform.GetChild(0);
        myUIPos.gameObject.SetActive(false);
        player = GameObject.FindWithTag("Player").transform;
    }

    void Update()
    {
        myUIPos.forward = (player.position - transform.position) - (player.position - transform.position).y * Vector3.up;
        dir = player.position - transform.position;
        myUIPos.localPosition = dir.normalized * dist;
    }

    public void OpenUI()
    {
        myUIPos.gameObject.SetActive(true);
    }

}

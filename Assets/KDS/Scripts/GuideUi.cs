﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GuideUi : MonoBehaviour
{
    private void Start()
    {
        StartCoroutine(delay());
    }

    private void OnDisable()
    {
        AccountDate.GetInstance().SetPlayerState(K_PlayerMove.PlayerState.Move);
        Cursor.lockState = CursorLockMode.Locked;
    }

    IEnumerator delay()
    {
        yield return new WaitForSeconds(0.1f);
        AccountDate.GetInstance().SetPlayerState(K_PlayerMove.PlayerState.Click);
        Cursor.lockState = CursorLockMode.Confined;
    }

}

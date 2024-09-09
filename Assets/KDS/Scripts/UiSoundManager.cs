﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UiSoundManager : MonoBehaviour
{
    public static UiSoundManager instance;

    public AudioClip click;
    public AudioClip notification;
    public AudioClip keyboard;

    AudioSource audioSource;

    private void Awake()
    {
        //instance 값이 null이면
        if (instance == null)
        {
            //이 스크립트를 instance에 담음
            instance = this;

            //씬 전환해도 유지하는 코드
            DontDestroyOnLoad(gameObject);
        }
        //이미 instance에 무언가 값이 들어있다면?
        else
        {
            //의도치 않은 중복 적용일 태니 이 게임 오브젝트 파괴.
            Destroy(gameObject);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    private void Update()
    {
        if (Input.anyKeyDown && !Input.GetButtonDown("Fire1"))
        {
            KeyClick();
        }
    }

    void PlaySound(AudioClip audios, float volume)
    {
        audioSource.PlayOneShot(audios, volume);
    }

    public void AudioClick()
    {
        PlaySound(click, 0.2f);
    }

    public void NotificationClick()
    {
        PlaySound(notification, 0.6f);
    }

    public void KeyClick()
    {
        PlaySound(keyboard, 0.25f);
    }
}

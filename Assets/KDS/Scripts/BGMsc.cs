using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BGMsc : MonoBehaviour
{
    AudioSource bgmAudioSource;

    public Slider BGMVolume;

    public Slider SFXVolume;

    private static BGMsc instance;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        bgmAudioSource = GetComponent<AudioSource>();

        BGMVolume.value = bgmAudioSource.volume;

        SFXVolume.value = UiSoundManager.instance.audioSource.volume;

        BGMVolume.onValueChanged.AddListener(BGMSliderMove);

        SFXVolume.onValueChanged.AddListener(SFXSliderMove);
    }

    public void SFXSliderMove(float arg0)
    {
        UiSoundManager.instance.audioSource.volume = arg0;
    }

    public void BGMSliderMove(float arg0)
    {
        bgmAudioSource.volume = arg0;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BGM : MonoBehaviour
{
    AudioSource bgm;

    public Slider volume;

    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
    }

    // Start is called before the first frame update
    void Start()
    {
        bgm = GetComponent<AudioSource>();

        volume.value = bgm.volume;
    }

    void BGMSliderMove()
    {
        bgm.volume = volume.value;
    }
}

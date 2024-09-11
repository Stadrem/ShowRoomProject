using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Voice.Unity;
using Photon.Pun;

public class VoiceManager : MonoBehaviour
{
    Recorder recorder;

    // Start is called before the first frame update
    void Start()
    {
        recorder = GetComponent<Recorder>();
    }

    // Update is called once per frame
    void Update()
    {
        //만약 V키를 누르면 음성 활성화함
        if (Input.GetKeyDown(KeyCode.V))
        {
            recorder.TransmitEnabled = !recorder.TransmitEnabled;
        }
    }
}

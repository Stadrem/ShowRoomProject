using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class H_CameraFollow : MonoBehaviour
{
    public Transform followTarget; //카메라
    public Transform lookatTarget; //플레이어

    void Start()
    {
        
    }

    
    void Update()
    {
        transform.position = followTarget.position;
        transform.rotation = lookatTarget.rotation;

    }
}

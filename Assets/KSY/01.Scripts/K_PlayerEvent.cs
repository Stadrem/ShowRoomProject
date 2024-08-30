using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class K_PlayerEvent : MonoBehaviour
{
    public float maxDistance;
    void Start()
    {
        
    }

    void Update()
    {
        Ray ray = new Ray(Camera.main.transform.position, Camera.main.transform.forward);
        RaycastHit hitInfo;
        if(Physics.Raycast(ray, out hitInfo, maxDistance, 1 << LayerMask.NameToLayer("Object")))
        {

        }
    }
}

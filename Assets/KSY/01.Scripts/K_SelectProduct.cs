using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class K_SelectProduct : MonoBehaviour
{
    void Start()
    {
        
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hitInfo;
            if(Physics.Raycast(ray, out hitInfo))
            {
                if(hitInfo.transform.name == "Door1")
                {

                }
                else if(hitInfo.transform.name == "Door2")
                {

                }
                else if (hitInfo.transform.name == "Door3")
                {

                }
                else if (hitInfo.transform.name == "Door4")
                {

                }
            }
        }
    }
}

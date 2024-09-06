using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class matTest : MonoBehaviour
{
    public LayerMask layerMask;
    Material mat;
    bool isHit = false;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        RaycastHit hit;

        if (Physics.Raycast(Camera.main.transform.position, Camera.main.transform.forward, out hit, 30.0f, layerMask))
        {
            print(hit.transform.name + " 레이 캐스트 닿음");
            mat = hit.transform.GetComponent<Renderer>().materials[1];

            print(mat.name);

            mat.SetFloat("_OutlineThickness", 0.03f);

            isHit = true;
        }
        else
        {
            if (isHit)
            {
                mat.SetFloat("_OutlineThickness", 0);
                isHit = false;

                mat = null;
            }
        }
    }
}

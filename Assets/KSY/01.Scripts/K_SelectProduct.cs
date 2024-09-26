using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class K_SelectProduct : MonoBehaviour
{
    public GameObject rf1;
    public GameObject rf2;

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
                    if (rf1.activeSelf) K_UIManager.GetInstance().SetSelectedMat(1, 0);
                    else if(rf2.activeSelf) K_UIManager.GetInstance().SetSelectedMat(2, 0);
                }
                else if(hitInfo.transform.name == "Door2")
                {
                    if (rf1.activeSelf) K_UIManager.GetInstance().SetSelectedMat(1, 1);
                    else if(rf2.activeSelf) K_UIManager.GetInstance().SetSelectedMat(2, 1);
                }
                else if (hitInfo.transform.name == "Door3")
                {
                    if (rf1.activeSelf) K_UIManager.GetInstance().SetSelectedMat(1, 2);
                    else if(rf2.activeSelf) K_UIManager.GetInstance().SetSelectedMat(2, 2);
                }
                else if (hitInfo.transform.name == "Door4")
                {
                    if(rf2.activeSelf) K_UIManager.GetInstance().SetSelectedMat(2, 3);
                }

            }
            else
            {
                K_UIManager.GetInstance().selectedMat = null;
            }
            
        }
    }
}

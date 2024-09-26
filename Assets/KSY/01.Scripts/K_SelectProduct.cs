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

    private void OnDisable()
    {
        moving = null;
        if (isRf1)
        {
            SetPos(true);
        }
        else if (!isRf1)
        {
            SetPos(false);
        }
    }
    public void SetPos(bool isRf1)
    {
        if (isRf1)
        {
            this.isRf1 = true;
            go_img_rf1.localPosition = Vector3.zero;
            go_img_rf2.localPosition = Vector3.right * 700;
        }
        else if(!isRf1)
        {
            this.isRf1 = false;
            go_img_rf2.localPosition = Vector3.zero;
            go_img_rf1.localPosition = Vector3.right * 700;
        }
    }

    public RectTransform go_img_rf1;
    public RectTransform go_img_rf2;
    public float speed = 10f;
    bool isRf1 = true;
    Coroutine moving = null;
    public void MoveLeft()
    {
        if (moving != null) return;
        if (isRf1)
        {
            moving = StartCoroutine(C_MoveLeft(go_img_rf2,go_img_rf1));
            Active2();
            isRf1 = false;
        }
        else if (!isRf1)
        {
            isRf1 = true;
            moving = StartCoroutine(C_MoveLeft(go_img_rf1, go_img_rf2));
            Active1();
        }
    }
    public void MoveRight()
    {
        if (moving != null) return;
        if (isRf1)
        {
            moving = StartCoroutine(C_MoveRight(go_img_rf2, go_img_rf1));
            Active2();
            isRf1 = false;
        }
        else if (!isRf1)
        {
            isRf1 = true;
            moving = StartCoroutine(C_MoveRight(go_img_rf1, go_img_rf2));
            Active1();
        }
    }

    IEnumerator C_MoveLeft(RectTransform go, RectTransform go2)
    {
        go.localPosition = new Vector3(700, 0, 0);
        while(go.localPosition.x >0)
        {
            go.localPosition += -Vector3.right * Time.deltaTime * speed;
            go2.localPosition += -Vector3.right * Time.deltaTime * speed;
            yield return null;
        }
        go.localPosition = Vector3.zero;
        moving = null;
    }
    IEnumerator C_MoveRight(RectTransform go, RectTransform go2)
    {
        go.localPosition = new Vector3(-700, 0, 0);
        while (go.localPosition.x < 0)
        {
            go.localPosition += Vector3.right * Time.deltaTime * speed;
            go2.localPosition += Vector3.right * Time.deltaTime * speed;
            yield return null;
        }
        go.localPosition = Vector3.zero;
        moving = null;
    }

    public void Active1()
    {
        rf1.SetActive(true);
        rf2.SetActive(false);
    }

    public void Active2()
    {
        rf1.SetActive(false);
        rf2.SetActive(true);
    }

}

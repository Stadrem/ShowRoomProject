using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    public float speed = 5f;
    public Transform farPos;
    CharacterController cc;


    float mx;
    float my;

    void Start()
    {
        cc = GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {
        float h = Input.GetAxisRaw("Horizontal");
        float v = Input.GetAxisRaw("Vertical");
        Vector3 dir = new Vector3(h, 0, v);
        dir = transform.TransformDirection(dir);
        
        dir.Normalize();

        cc.Move(dir * speed * Time.deltaTime);
        //transform.position += dir * speed * Time.deltaTime;

        //마우스 좌우로 움직이자
        mx += Input.GetAxis("Mouse X") * 300 * Time.deltaTime;
        transform.eulerAngles = new Vector3(0, mx, 0);

        //마우스 상하 그런데 플레이어는 돌면 안돼
        my -= Input.GetAxis("Mouse Y") * 300 * Time.deltaTime;
        my = Mathf.Clamp(my, -70, 50);
        farPos.transform.localEulerAngles = new Vector3(my, 0, 0);

        
    }
}

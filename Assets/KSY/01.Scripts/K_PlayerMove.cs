using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class K_PlayerMove : MonoBehaviour
{
    public enum PlayerState
    {
        Move,
        Click
    }
    public PlayerState currState;
    public float moveSpeed;
    Animator myAnim;
    Transform myBody;
    public GameObject[] bodys;
    bool lockmode;

    Vector3 dir;
    float v;
    float h;

    void Start()
    {
        currState = PlayerState.Move;
        lockmode = true;
        myAnim = GetComponentInChildren<Animator>();
        myBody = transform.GetChild(0);
        SetCursorLock();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha0))
        {
            SetAvatar(bodys[0]);
        }
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            SetAvatar(bodys[1]);
        }
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            SetAvatar(bodys[2]);
        }
        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            SetAvatar(bodys[3]);
        }
        if (Input.GetKeyDown(KeyCode.K))
        {
            if (myBody.childCount > 0) Destroy(myBody.GetChild(0).gameObject);
            print(myBody.childCount);
        }
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            lockmode = !lockmode;
            SetCursorLock();
        }
    }

    private void FixedUpdate()
    {
        switch (currState)
        {
            case PlayerState.Move:
                Move();
                break;
            case PlayerState.Click:
                break;
        }
    }


    public void ChangeState(PlayerState state)
    {
        currState = state;
        switch (currState)
        {
            case PlayerState.Move:
                break;
            case PlayerState.Click:
            myAnim.SetBool("Move", false);
                break;
            default:
                break;
        }
    }

    void Move()
    {
        v = Input.GetAxisRaw("Vertical");
        h = Input.GetAxisRaw("Horizontal");
        dir = new Vector3(h, 0, v);
        if(dir.magnitude > 0)
        {
            myAnim?.SetBool("Move", true);
        }
        else
        {
            myAnim?.SetBool("Move", false);
        }
        transform.Translate(dir.normalized * moveSpeed * Time.deltaTime);
    }

    void SetCursorLock()
    {
        if (lockmode)
        {
            Cursor.lockState = CursorLockMode.Locked;
        }
        else
        {
            Cursor.lockState = CursorLockMode.Confined;
        }
    }


    public void SetAvatar(GameObject bodyObject)
    {
        if (myBody.childCount > 0) Destroy(myBody.GetChild(0).gameObject);
        GameObject go = Instantiate(bodyObject, myBody);
        go.transform.localPosition = Vector3.zero;
        myAnim = GetComponentInChildren<Animator>();
    }
}

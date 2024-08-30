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

    Vector3 dir;
    float v;
    float h;

    void Start()
    {
        currState = PlayerState.Move;
    }

    void Update()
    {
        
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
        transform.Translate(dir.normalized * moveSpeed * Time.deltaTime);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class InputManager_CF : MonoBehaviour {

    public event Action escClickEvent;
    public event Action wClickEvent;
    public event Action sClickEvent;
    public event Action aClickEvent;
    public event Action dClickEvent;
    public event Action qClickEvent;
    public event Action eClickEvent;
    public event Action zClickEvent;
    public event Action cClickEvent;
    public event Action rMouseDownEvent;
    public event Action rMouseUpEvent;
    public event Action tabClickEvent;



    float mouseX = 0;
    public float MouseX
    {
        get { return mouseX; }
    }
    float mouseY = 0;
    public float MouseY
    {
        get { return mouseY; }
    }
    float horizontal = 0;
    public float HorizontalAxis
    {
        get { return horizontal; }
    }
    float vertical = 0;
    public float VerticalAxis
    {
        get { return vertical; }
    }
    float upAndDown = 0;
    public float UpAndDownAxis
    {
        get { return upAndDown; }
    }
    float keyRotate = 0;
    public float KeyRotateAxis
    {
        get { return keyRotate; }
    }

    /// <summary>
    /// 鼠标指针上一帧位置
    /// </summary>
    Vector2 mouselastFramePosition;
    /// <summary>
    /// 鼠标指针当前帧位置
    /// </summary>
    Vector2 mouseCurrentPosition;



    private void Awake()
    {
        DontDestroyOnLoad(this.gameObject);
    }

    // Use this for initialization
    void Start () {
		
	}
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            escClickEvent?.Invoke();
        }
        if (Input.GetKeyDown(KeyCode.W))
        {
            wClickEvent?.Invoke();
        }
        if (Input.GetKeyDown(KeyCode.S))
        {
            sClickEvent?.Invoke();
        }
        if (Input.GetKeyDown(KeyCode.A))
        {
            aClickEvent?.Invoke();
        }
        if (Input.GetKeyDown(KeyCode.D))
        {
            dClickEvent?.Invoke();
        }
        if (Input.GetKeyDown(KeyCode.Q))
        {
            qClickEvent?.Invoke();
        }
        if (Input.GetKeyDown(KeyCode.E))
        {
            eClickEvent?.Invoke();
        }
        if (Input.GetKeyDown(KeyCode.Z))
        {
            zClickEvent?.Invoke();
        }
        if (Input.GetKeyDown(KeyCode.C))
        {
            cClickEvent?.Invoke();
        }
        if (Input.GetKeyDown(KeyCode.Mouse1))
        {
            rMouseDownEvent?.Invoke();
        }
        if (Input.GetKeyUp(KeyCode.Mouse1))
        {
            rMouseUpEvent?.Invoke();
        }
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            tabClickEvent?.Invoke();
        }
        SetHorizontalAxis();
        SetVerticalAxis();
        SetUpAndDownAxis();
        SetKeyRotateAxis();
        SetMouseXAxis();
        SetMouseYAxis();
    }
    /// <summary>
    /// 获取键盘A、D移动的按键轴值
    /// </summary>
    /// <returns></returns>
    public void SetHorizontalAxis()
    {
       if((Input.GetKey(KeyCode.A)|| Input.GetKey(KeyCode.D)).Equals(false))
        {
            if(horizontal>0)
            {
                horizontal -= Time.fixedDeltaTime*4;
                horizontal = Mathf.Clamp(horizontal, 0, 1);
            }
            else if(horizontal<0)
            {
                horizontal += Time.fixedDeltaTime*4;
                horizontal = Mathf.Clamp(horizontal, -1, 0);
            }
        }
       else
        {
            if(Input.GetKey(KeyCode.A))
            {
                if(horizontal>-1)
                {
                    horizontal-= Time.fixedDeltaTime*3;
                    horizontal = Mathf.Clamp(horizontal, -1, 0);
                }
            }
            if(Input.GetKey(KeyCode.D))
            {
                if (horizontal < 1)
                {
                    horizontal += Time.fixedDeltaTime*3;
                    horizontal = Mathf.Clamp(horizontal, 0, 1);
                }
            }
        }
    }
    /// <summary>
    /// 获取键盘W、S移动的按键轴值
    /// </summary>
    /// <returns></returns>
    public void SetVerticalAxis()
    {
        if ((Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.S)).Equals(false))
        {
            if (vertical > 0)
            {
                vertical -= Time.fixedDeltaTime*4;
                vertical = Mathf.Clamp(vertical, 0, 1);
            }
            else if (vertical < 0)
            {
                vertical += Time.fixedDeltaTime*4;
                vertical = Mathf.Clamp(vertical, -1, 0);
            }
        }
        else
        {
            if (Input.GetKey(KeyCode.S))
            {
                if (vertical > -1)
                {
                    vertical -= Time.fixedDeltaTime*3;
                    vertical = Mathf.Clamp(vertical, -1, 0);
                }
            }
            if (Input.GetKey(KeyCode.W))
            {
                if (vertical < 1)
                {
                    vertical += Time.fixedDeltaTime*3;
                    vertical = Mathf.Clamp(vertical, 0, 1);
                }
            }
        }
    }
    /// <summary>
    /// 获取键盘Z、C移动的按键轴值
    /// </summary>
    /// <returns></returns>
    public void SetUpAndDownAxis()
    {
        if ((Input.GetKey(KeyCode.Z) || Input.GetKey(KeyCode.C)).Equals(false))
        {
            if (upAndDown > 0)
            {
                upAndDown -= Time.fixedDeltaTime*8;
                upAndDown = Mathf.Clamp(upAndDown, 0, 1);
            }
            else if (upAndDown < 0)
            {
                upAndDown += Time.fixedDeltaTime*8;
                upAndDown = Mathf.Clamp(upAndDown, -1, 0);
            }
        }
        else
        {
            if (Input.GetKey(KeyCode.C))
            {
                if (upAndDown > -1)
                {
                    upAndDown -= Time.fixedDeltaTime*6;
                    upAndDown = Mathf.Clamp(upAndDown, -1, 0);
                }
            }
            if (Input.GetKey(KeyCode.Z))
            {
                if (upAndDown < 1)
                {
                    upAndDown += Time.fixedDeltaTime*6;
                    upAndDown = Mathf.Clamp(upAndDown, 0, 1);
                }
            }
        }
    }
    /// <summary>
    /// 获取键盘Q、E旋转角度的按键轴值
    /// </summary>
    /// <returns></returns>
    public void SetKeyRotateAxis()
    {
        if ((Input.GetKey(KeyCode.Q) || Input.GetKey(KeyCode.E)).Equals(false))
        {
            if (keyRotate > 0)
            {
                keyRotate -= 0.2f;
                keyRotate = Mathf.Clamp(keyRotate, 0, 10);
            }
            else if (keyRotate < 0)
            {
                keyRotate += 0.2f;
                keyRotate = Mathf.Clamp(keyRotate, -10, 0);
            }
        }
        else
        {
            if (Input.GetKey(KeyCode.Q))
            {
                if (keyRotate > -10)
                {
                    keyRotate -= 0.05f;
                    keyRotate = Mathf.Clamp(keyRotate, -10, 0);
                }
            }
            if (Input.GetKey(KeyCode.E))
            {
                if (keyRotate < 10)
                {
                    keyRotate += 0.05f;
                    keyRotate = Mathf.Clamp(keyRotate, 0, 10);
                }
            }
        }
    }
    /// <summary>
    /// 获取鼠标水平移动值
    /// </summary>
    /// <returns></returns>
    public void SetMouseXAxis()
    {
        mouseX = Mathf.Clamp(Input.GetAxis("Mouse X"),-10,10);
    }
    /// <summary>
    /// 获取鼠标垂直移动值
    /// </summary>
    /// <returns></returns>
    public void SetMouseYAxis()
    {
        mouseY = Mathf.Clamp(Input.GetAxis("Mouse Y"), -10, 10);
    }




}

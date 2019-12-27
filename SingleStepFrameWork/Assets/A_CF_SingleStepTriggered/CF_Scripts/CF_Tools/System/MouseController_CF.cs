using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

/// <summary>
/// 控制鼠标指针的控制器
/// </summary>
public class MouseController_CF : MonoBehaviour {

    bool pauseGame = false;
    bool mouseStateProperty = true;
    public bool MouseStateProperty
    {
        get { return mouseStateProperty; }
    }
    InputManager_CF inputManager_CF;
    EscapePanelScript_CF escape;

    private void Awake()
    {
        escape= FindObjectOfType<EscapePanelScript_CF>();
        if (escape == null)
        {
            Debug.LogError("退出界面不存在或者获取失败");
        }
        else
        {
            escape.panelActiveEvent += PauseGameFunc;
            escape.panelDisActiveEvent += ReplayGameFunc;
        }
        inputManager_CF = FindObjectOfType<InputManager_CF>();
        if (inputManager_CF == null)
        {
            Debug.LogError("输入管理器不存在或者获取失败");
        }
        else
        {
            inputManager_CF.rMouseDownEvent += DisableCursor;
            inputManager_CF.rMouseUpEvent += EnableCursor;
        }
    }

    // Use this for initialization
    void Start () {
        Cursor.lockState = CursorLockMode.None;
    }
	
	// Update is called once per frame
	void Update () {
        if (pauseGame)
        {
            return;
        }
        else
        {
            Cursor.visible = mouseStateProperty;
        }
    }


    void EnableCursor()
    {
        mouseStateProperty = true;
    }

    void DisableCursor()
    {
        mouseStateProperty = false;
    }


    void PauseGameFunc()
    {
        pauseGame = true;
        Cursor.visible = true;
    }

    void ReplayGameFunc()
    {
        pauseGame = false;
    }


    private void OnDestroy()
    {
        escape.panelActiveEvent -= PauseGameFunc;
        escape.panelDisActiveEvent -= ReplayGameFunc;
        inputManager_CF.rMouseDownEvent -= DisableCursor;
        inputManager_CF.rMouseUpEvent -= EnableCursor;
    }


}

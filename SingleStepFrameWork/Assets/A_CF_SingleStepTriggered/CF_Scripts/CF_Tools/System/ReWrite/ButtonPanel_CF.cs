using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using UnityEngine.EventSystems;

public class ButtonPanel_CF : ReWriteGameControl_CF
{
    /// <summary>
    /// 一次性点击事件列表
    /// </summary>
    List<Action> disposableClickList = new List<Action>();

    /// <summary>
    /// 本界面的按钮组件
    /// </summary>
    public Button_CF button;
    /// <summary>
    /// 点击按钮事件
    /// </summary>
    public event Action onClickButtonEvent;

    /// <summary>
    /// 按钮松开事件
    /// </summary>
    public event Action onPressButtonEvent;
    /// <summary>
    /// 按钮按下事件
    /// </summary>
    public event Action onReleaseButtonEvent;
    /// <summary>
    /// 鼠标进入按钮事件
    /// </summary>
    public event Action onEnterButtonEvent;
    /// <summary>
    /// 鼠标移出按钮事件
    /// </summary>
    public event Action onExitButtonEvent;




    public override void ActiveGameObject()
    {
        base.ActiveGameObject();
        EventCenter_CF.Instance.CloseInteractiveControl();
    }

    public override void DisActiveGameObject()
    {
        base.DisActiveGameObject();
        EventCenter_CF.Instance.OpenInteractiveControl();
    }

    protected override void Awake()
    {
        base.Awake();
        if (button == null)
        {
            throw new Exception("未找到该界面的按钮组件");
        }
        button.onClick.AddListener(OnClickFunc);
        button.onEnterButtonEvent += OnEnterFunc;
        button.onExitButtonEvent += OnExitFunc;
        button.onPressButtonEvent += OnPressFunc;
        button.onReleaseButtonEvent += OnReleaseFunc;

    }

    protected override void OnDestroy()
    {
        base.OnDestroy();
    }

    protected override void OnDisable()
    {
        base.OnDisable();
    }

    protected override void OnEnable()
    {
        base.OnEnable();
    }

    protected override void Start()
    {
        base.Start();
    }
    void OnClickFunc()
    {
        foreach (Action item in disposableClickList)
        {
            onClickButtonEvent += item;
        }
        onClickButtonEvent?.Invoke();
        foreach (Action item in disposableClickList)
        {
            onClickButtonEvent -= item;
        }
        disposableClickList.Clear();

        DisActiveGameObject();
    }
    void OnEnterFunc()
    {
        onEnterButtonEvent?.Invoke();
    }
    void OnExitFunc()
    {
        onExitButtonEvent?.Invoke();
    }
    void OnPressFunc()
    {
        onPressButtonEvent?.Invoke();
    }
    void OnReleaseFunc()
    {
        onReleaseButtonEvent?.Invoke();
    }

    public void AddDisposableClickEvent(Action ac)
    {
        disposableClickList.Add(ac);
    }


}

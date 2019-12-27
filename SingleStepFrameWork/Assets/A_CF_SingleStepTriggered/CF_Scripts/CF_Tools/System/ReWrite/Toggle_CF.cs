using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using UnityEngine.EventSystems;

public class Toggle_CF : ReWriteGameControl_CF,IPointerEnterHandler,IPointerExitHandler
{
    /// <summary>
    /// 本按钮的按钮组件
    /// </summary>
    Toggle toggle;
    /// <summary>
    /// 点击按钮事件
    /// </summary>
    public event Action<bool> onValueChangedEvent;
    /// <summary>
    /// 一次性点击按钮事件列表
    /// </summary>
    List<Action<bool>> disposableonValueChangedEventList = new List<Action<bool>>();
    /// <summary>
    /// 一次性点击按钮事件列表
    /// </summary>
    public void AddDisposableonValueChangedEvent(Action<bool> ac)
    {
        disposableonValueChangedEventList.Add(ac);
    }

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
    }

    public override void DisActiveGameObject()
    {
        base.DisActiveGameObject();
    }


    public void OnPointerEnter(PointerEventData eventData)
    {
        onEnterButtonEvent?.Invoke();
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        onExitButtonEvent?.Invoke();
    }


    protected override void Awake()
    {
        base.Awake();
        toggle = thistransform.GetComponent<Toggle>();
        if (toggle == null)
        {
            throw new Exception("未找到该Toggle的按钮组件");
        }
        toggle.onValueChanged.AddListener(OnValueChangeFunc);
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
    void OnValueChangeFunc(bool b)
    {
        foreach (Action<bool> item in disposableonValueChangedEventList)
        {
            onValueChangedEvent += item;
        }
        onValueChangedEvent?.Invoke(b);
        foreach (Action<bool> item in disposableonValueChangedEventList)
        {
            onValueChangedEvent -= item;
        }
        disposableonValueChangedEventList.Clear();

    }


}

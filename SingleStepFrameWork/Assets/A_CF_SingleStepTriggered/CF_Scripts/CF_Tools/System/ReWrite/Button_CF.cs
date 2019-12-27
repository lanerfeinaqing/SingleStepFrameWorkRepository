using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using UnityEngine.EventSystems;

public class Button_CF : Button
{
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

    public override bool Equals(object other)
    {
        return base.Equals(other);
    }

    public override Selectable FindSelectableOnDown()
    {
        return base.FindSelectableOnDown();
    }

    public override Selectable FindSelectableOnLeft()
    {
        return base.FindSelectableOnLeft();
    }

    public override Selectable FindSelectableOnRight()
    {
        return base.FindSelectableOnRight();
    }

    public override Selectable FindSelectableOnUp()
    {
        return base.FindSelectableOnUp();
    }

    public override int GetHashCode()
    {
        return base.GetHashCode();
    }

    public override bool IsActive()
    {
        return base.IsActive();
    }

    public override bool IsInteractable()
    {
        return base.IsInteractable();
    }

    public override void OnDeselect(BaseEventData eventData)
    {
        base.OnDeselect(eventData);
    }

    public override void OnMove(AxisEventData eventData)
    {
        base.OnMove(eventData);
    }

    public override void OnPointerClick(PointerEventData eventData)
    {
        base.OnPointerClick(eventData);
    }

    public override void OnPointerDown(PointerEventData eventData)
    {
        base.OnPointerDown(eventData);
        onPressButtonEvent?.Invoke();
    }

    public override void OnPointerEnter(PointerEventData eventData)
    {
        base.OnPointerEnter(eventData);
        onEnterButtonEvent?.Invoke();
    }

    public override void OnPointerExit(PointerEventData eventData)
    {
        base.OnPointerExit(eventData);
        onExitButtonEvent?.Invoke();
    }

    public override void OnPointerUp(PointerEventData eventData)
    {
        base.OnPointerUp(eventData);
        onReleaseButtonEvent?.Invoke();
    }

    public override void OnSelect(BaseEventData eventData)
    {
        base.OnSelect(eventData);
    }

    public override void OnSubmit(BaseEventData eventData)
    {
        base.OnSubmit(eventData);
    }

    public override void Select()
    {
        base.Select();
    }

    public override string ToString()
    {
        return base.ToString();
    }

    protected override void Awake()
    {
        base.Awake();
    }

    protected override void DoStateTransition(SelectionState state, bool instant)
    {
        base.DoStateTransition(state, instant);
    }

    protected override void InstantClearState()
    {
        base.InstantClearState();
    }

    protected override void OnBeforeTransformParentChanged()
    {
        base.OnBeforeTransformParentChanged();
    }

    protected override void OnCanvasGroupChanged()
    {
        base.OnCanvasGroupChanged();
    }

    protected override void OnCanvasHierarchyChanged()
    {
        base.OnCanvasHierarchyChanged();
    }

    protected override void OnDestroy()
    {
        base.OnDestroy();
    }

    protected override void OnDidApplyAnimationProperties()
    {
        base.OnDidApplyAnimationProperties();
    }

    protected override void OnDisable()
    {
        base.OnDisable();
    }

    protected override void OnEnable()
    {
        base.OnEnable();
    }

    protected override void OnRectTransformDimensionsChange()
    {
        base.OnRectTransformDimensionsChange();
    }

    protected override void OnTransformParentChanged()
    {
        base.OnTransformParentChanged();
    }

    protected override void OnValidate()
    {
        base.OnValidate();
    }

    protected override void Reset()
    {
        base.Reset();
    }

    protected override void Start()
    {
        base.Start();
    }
}

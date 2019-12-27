using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class AnimatorManager : ReWriteGameControl_CF {
    /// <summary>
    /// 当前物体的animator组件
    /// </summary>
    Animator anim;
    /// <summary>
    /// 确定当前组件动画时trigger模式还是bool模式
    /// </summary>
     AnimatorComponentPlayMode animatorComponentPlay=AnimatorComponentPlayMode.Trriger;
    /// <summary>
    /// trigger模式下动画开始播放事件
    /// </summary>
    public event Action triggerAnimationStarEvent;
    /// <summary>
    /// 一次性trigger模式下动画开始播放事件列表
    /// </summary>
    List<Action> disposabletriggerAnimationStarEventList = new List<Action>();
    /// <summary>
    /// 一次性trigger模式下动画开始播放事件列表
    /// </summary>
    public void AddDisposabletriggerAnimationStarEvent(Action ac)
    {
        disposabletriggerAnimationStarEventList.Add(ac);
    }
    /// <summary>
    /// trigger模式下动画结束播放事件
    /// </summary>
    public event Action triggerAnimationEndEvent;
    /// <summary>
    /// 一次性trigger模式下动画结束播放事件列表
    /// </summary>
    List<Action> disposabletriggerAnimationEndEventList = new List<Action>();
    /// <summary>
    /// 一次性trigger模式下动画结束播放事件列表
    /// </summary>
    public void AddDisposabletriggerAnimationEndEvent(Action ac)
    {
        disposabletriggerAnimationEndEventList.Add(ac);
    }
    /// <summary>
    /// bool模式下动画开始播放事件
    /// </summary>
    public event Action boolAnimationStarEvent;
    /// <summary>
    /// 一次性bool模式下动画开始播放事件列表
    /// </summary>
    List<Action> disposableboolAnimationStarEventList = new List<Action>();
    /// <summary>
    /// 一次性bool模式下动画开始播放事件列表
    /// </summary>
    public void AddDisposableboolAnimationStarEvent(Action ac)
    {
        disposableboolAnimationStarEventList.Add(ac);
    }
    /// <summary>
    /// bool模式下动画结束播放事件
    /// </summary>
    public event Action boolAnimationEndEvent;
    /// <summary>
    /// 一次性bool模式下动画结束播放事件列表
    /// </summary>
    List<Action> disposableboolAnimationEndEventList = new List<Action>();
    /// <summary>
    /// 一次性bool模式下动画结束播放事件列表
    /// </summary>
    public void AddDisposableboolAnimationEndEvent(Action ac)
    {
        disposableboolAnimationEndEventList.Add(ac);
    }




    /// <summary>
    /// bool模式的动画参数名
    /// </summary>
    string boolName = "bool";
    /// <summary>
    /// bool模式的动画参数值
    /// </summary>
    bool boolValue = false;

    /// <summary>
    /// trigger模式的动画参数名
    /// </summary>
    string triggerName = "trigger";


    /// <summary>
    /// 播放一条龙的trigger动画回到初始姿态
    /// </summary>
    public void PlayTriggerAnimation()
    {
        animatorComponentPlay = AnimatorComponentPlayMode.Trriger;

        foreach (Action item in disposabletriggerAnimationStarEventList)
        {
            triggerAnimationStarEvent += item;
        }
        triggerAnimationStarEvent?.Invoke();
        foreach (Action item in disposabletriggerAnimationStarEventList)
        {
            triggerAnimationStarEvent -= item;
        }
        disposabletriggerAnimationStarEventList.Clear();

        anim.SetTrigger(triggerName);
        EventCenter_CF.Instance.CloseInteractiveControl();
    }

    /// <summary>
    /// trigger动画播放完成事件，被动画片段自己调用
    /// </summary>
    public void CompleteTriggerAnimation()
    {
        animatorComponentPlay = AnimatorComponentPlayMode.Trriger;

        foreach (Action item in disposabletriggerAnimationEndEventList)
        {
            triggerAnimationEndEvent += item;
        }
        triggerAnimationEndEvent?.Invoke();
        foreach (Action item in disposabletriggerAnimationEndEventList)
        {
            triggerAnimationEndEvent -= item;
        }
        disposabletriggerAnimationEndEventList.Clear();

        EventCenter_CF.Instance.OpenInteractiveControl();
    }

    /// <summary>
    /// 播放bool动画
    /// </summary>
    public void PlayBoolAnimation()
    {
        animatorComponentPlay = AnimatorComponentPlayMode.Bool;
        boolValue = true;

        foreach (Action item in disposableboolAnimationStarEventList)
        {
            boolAnimationStarEvent += item;
        }
        boolAnimationStarEvent?.Invoke();
        foreach (Action item in disposableboolAnimationStarEventList)
        {
            boolAnimationStarEvent -= item;
        }
        disposableboolAnimationStarEventList.Clear();

        anim.SetBool(boolName, boolValue);
        EventCenter_CF.Instance.CloseInteractiveControl();
    }

    /// <summary>
    /// 停止bool动画
    /// </summary>
    public void StopBoolAnimation()
    {
        animatorComponentPlay = AnimatorComponentPlayMode.Bool;
        boolValue = false;

        foreach (Action item in disposableboolAnimationEndEventList)
        {
            boolAnimationEndEvent += item;
        }
        boolAnimationEndEvent?.Invoke();
        foreach (Action item in disposableboolAnimationEndEventList)
        {
            boolAnimationEndEvent -= item;
        }
        disposableboolAnimationEndEventList.Clear();

        anim.SetBool(boolName, boolValue);
        EventCenter_CF.Instance.OpenInteractiveControl();
    }




    public override void ActiveGameObject()
    {
        base.ActiveGameObject();
    }

    public override void DisActiveGameObject()
    {
        base.DisActiveGameObject();
    }

    protected override void Awake()
    {
        base.Awake();
        anim=thistransform.GetComponent<Animator>();
        if(anim==null)
        {
            throw new Exception("当前物体的Animator组件获取失败");
        }
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

    private void Update()
    {
        if(pauseGame)
        {
            anim.enabled = false;
        }
        else
        {
            anim.enabled = true;
        }
    }





}

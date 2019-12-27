using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using UnityEngine.EventSystems;


public class PictureTimer_CF : ReWriteGameControl_CF, IPointerEnterHandler, IPointerExitHandler
{
    /// <summary>
    /// 计时器图片组件
    /// </summary>
    public Image timer;
    /// <summary>
    /// 计时器所需要用到的时间
    /// </summary>
    float time = 5;
    /// <summary>
    /// 计时器开始事件
    /// </summary>
    public event Action imageTimerStartEvent;
    /// <summary>
    /// 一次性计时器开始事件列表
    /// </summary>
    List<Action> disposableimageTimerStarEventList = new List<Action>();
    /// <summary>
    /// 一次性计时器开始事件列表
    /// </summary>
    public void AddDisposableimageTimerStarEvent(Action ac)
    {
        disposableimageTimerStarEventList.Add(ac);
    }

    /// <summary>
    /// 计时器结束事件
    /// </summary>
    public event Action imageTimerEndEvent;
    /// <summary>
    /// 一次性计时器结束事件列表
    /// </summary>
    List<Action> disposableimageTimerEndEventList = new List<Action>();
    /// <summary>
    ///一次性计时器结束事件列表
    /// </summary>
    public void AddDisposableimageTimerEndEvent(Action ac)
    {
        disposableimageTimerEndEventList.Add(ac);
    }

    /// <summary>
    /// 鼠标进入计时器区域事件
    /// </summary>
    public event Action onEnterImageTimerEvent;
    /// <summary>
    /// 鼠标移出计时器区域事件
    /// </summary>
    public event Action onExitImageTimerEvent;


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
        onEnterImageTimerEvent?.Invoke();
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        onExitImageTimerEvent?.Invoke();
    }

    protected override void Awake()
    {
        base.Awake();
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
    /// <summary>
    /// 开始播放计时器动画
    /// </summary>
    /// <param name="progressTime"></param>
    public void StartTimer(float progressTime=5)
    {
        if(thisgameobject.activeInHierarchy==false)
        {
            ActiveGameObject();
        }
        time = progressTime;

        foreach (Action item in disposableimageTimerStarEventList)
        {
            imageTimerStartEvent += item;
        }
        imageTimerStartEvent?.Invoke();
        foreach (Action item in disposableimageTimerStarEventList)
        {
            imageTimerStartEvent -= item;
        }
        disposableimageTimerStarEventList.Clear();


        EventCenter_CF.Instance.CloseInteractiveControl();
        StartCoroutine("TimerAnimation");

    }

   /// <summary>
   /// 完成计时器动画
   /// </summary>
     void CompleteTimer()
    {
        StopCoroutine("TimerAnimation");

        foreach (Action item in disposableimageTimerEndEventList)
        {
            imageTimerEndEvent += item;
        }
        imageTimerEndEvent?.Invoke();
        foreach (Action item in disposableimageTimerEndEventList)
        {
            imageTimerEndEvent -= item;
        }
        disposableimageTimerEndEventList.Clear();


        EventCenter_CF.Instance.OpenInteractiveControl();
        DisActiveGameObject();
    }


    /// <summary>
    /// 帧数时间计时器
    /// </summary>
    /// <returns></returns>
    IEnumerator TimerAnimation()
    {
        float counter = 0;
        float rate = 0;
        while (counter<=time)
        {
            while (pauseGame)
            {
                yield return new WaitForSeconds(0.1f);
            }
            //timeshi在类上面已经声明过的变量，是指定的一个时间哦
            rate = counter / time;
            //这是运用图片组件填充来制作进度条哦
            timer.fillAmount = rate;
            //这就是计时器加时间了
            counter += Time.deltaTime;
            //这是计时器等待
            yield return new WaitForEndOfFrame();
        }
        CompleteTimer();
    }






}

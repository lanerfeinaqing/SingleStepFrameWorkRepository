using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class DelayEventBuffe : MonoBehaviour
{
    /// <summary>
    /// 计时器所需要用到的时间
    /// </summary>
    float time = 0.2f;
    /// <summary>
    /// 延迟事件
    /// </summary>
    public event Action delayInvokeEvent;
    /// <summary>
    /// 一次性计时器开始事件列表
    /// </summary>
    List<Action> delayInvokeEventList = new List<Action>();
    /// <summary>
    /// 一次性计时器开始事件列表
    /// </summary>
    public void AdddelayInvokeEvent(Action ac)
    {
        delayInvokeEventList.Add(ac);
    }



    /// <summary>
    /// 开始播放计时器动画
    /// </summary>
    /// <param name="progressTime"></param>
    public void StartTimer(float progressTime = 0.2f)
    {
        time = progressTime;
        EventCenter_CF.Instance.CloseInteractiveControl();
        StartCoroutine("TimerAnimation");
    }

    /// <summary>
    /// 完成计时器动画
    /// </summary>
    void CompleteTimer()
    {
        StopCoroutine("TimerAnimation");
        EventCenter_CF.Instance.OpenInteractiveControl();
        foreach (Action item in delayInvokeEventList)
        {
            delayInvokeEvent += item;
        }
        delayInvokeEvent?.Invoke();
        foreach (Action item in delayInvokeEventList)
        {
            delayInvokeEvent -= item;
        }
        delayInvokeEventList.Clear();
    }




    /// <summary>
    /// 帧数时间计时器
    /// </summary>
    /// <returns></returns>
    IEnumerator TimerAnimation()
    {
        float counter = 0;
        while (counter <= time)
        {
            counter += Time.deltaTime;
            yield return new WaitForEndOfFrame();
        }
        CompleteTimer();
    }




}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class ScaleAnimation_CF : ReWriteGameControl_CF {

    
    Vector3 dest;
    bool start = false;
    Vector3 speed ;
    Vector3 current ;
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
    /// 液面动画结束事件
    /// </summary>
    public Action scriptEvent;
    float time = 1;
    // Update is called once per frame
    void Update()
    {
        if (start)
        {
            if (pauseGame == false)
            {
                if (thistransform.localScale.x>=dest.x&& thistransform.localScale.y >= dest.y&& thistransform.localScale.z >= dest.z)
                {
                    start = false;
                    foreach (Action item in delayInvokeEventList)
                    {
                        scriptEvent += item;
                    }
                    scriptEvent?.Invoke();
                    foreach (Action item in delayInvokeEventList)
                    {
                        scriptEvent -= item;
                    }
                    return;
                }
                current += Time.deltaTime * speed / time;
                thistransform.localScale = current;
            }
        }
    }


    public void Open(Vector3 _dest, float _time = 1)
    {
        dest = _dest;
        time = _time;
        speed = dest - thistransform.localScale;
        current = thistransform.localScale;
        start = true;
    }
}

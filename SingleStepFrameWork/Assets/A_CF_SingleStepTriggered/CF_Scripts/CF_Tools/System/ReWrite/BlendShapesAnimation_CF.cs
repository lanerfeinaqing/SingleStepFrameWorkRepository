using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class BlendShapesAnimation_CF : ReWriteGameControl_CF {

    public SkinnedMeshRenderer skin;
    public float dest = 100;
    bool start = false;
    float speed = 0;
    float current = 0;
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
                if (skin.GetBlendShapeWeight(0) >= dest)
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
                    //gameObject.SetActive(false);
                    return;
                }
                current += Time.deltaTime * speed / time;
                skin.SetBlendShapeWeight(0, current);
            }
        }
    }


    public void Open(float _dest = 100, float _time = 1)
    {
        dest = _dest;
        time = _time;
        speed = dest - skin.GetBlendShapeWeight(0);
        current = skin.GetBlendShapeWeight(0);
        start = true;
    }
}

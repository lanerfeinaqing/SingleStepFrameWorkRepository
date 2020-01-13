using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class ScaleAnimation_CF : ReWriteGameControl_CF {

    
    Vector3 dest;
    Vector3 speed ;
    Vector3 Valeurinitiale;
    int destTransformCounter = 0;
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
   



    public void Open(Vector3 _dest, float _time = 1)
    {
        dest = _dest;
        time = _time;
        speed = dest - thistransform.localScale;
        Valeurinitiale = thistransform.localScale;
        StartCoroutine("ScaleAnimationFunc");
    }

    void Close()
    {
        StopCoroutine("ScaleAnimationFunc");
        foreach (Action item in delayInvokeEventList)
        {
            scriptEvent += item;
        }
        scriptEvent?.Invoke();
        foreach (Action item in delayInvokeEventList)
        {
            scriptEvent -= item;
        }
        delayInvokeEventList.Clear();
    }

    IEnumerator ScaleAnimationFunc()
    {
        float waitRate = 0.1f;
        destTransformCounter = (int)(time / Time.fixedDeltaTime);
        for (int j = 0; j < destTransformCounter; j++)
        {
            while (pauseGame)
            {
                yield return new WaitForSeconds(waitRate);
            }
            if (j == 0)
            {
                thistransform.localScale = Valeurinitiale;
            }
            else if (j == destTransformCounter - 1)
            {
                thistransform.localScale = dest;
            }
            else
            {
                float a = j;
                float b = destTransformCounter;
                float Proportion = a / b;
                thistransform.localScale = Proportion * speed + Valeurinitiale;
            }
            yield return new WaitForFixedUpdate();
        }
        Close();
    }

}


 
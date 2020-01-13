using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class BlendShapesAnimation_CF : ReWriteGameControl_CF {

    public SkinnedMeshRenderer skin;
    public float dest = 100;
    float speed = 0;
    float Valeurinitiale = 0;
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
    


    public void Open(float _dest = 100, float _time = 1)
    {
        dest = _dest;
        time = _time;
        speed = dest - skin.GetBlendShapeWeight(0);
        Valeurinitiale = skin.GetBlendShapeWeight(0);
        StartCoroutine("BlendShapeAnimationFunc");
    }

    void Close()
    {
        StopCoroutine("BlendShapeAnimationFunc");
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

    IEnumerator BlendShapeAnimationFunc()
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
                skin.SetBlendShapeWeight(0, Valeurinitiale);
            }
            else if (j == destTransformCounter - 1)
            {
                skin.SetBlendShapeWeight(0, dest);
            }
            else
            {
                float a = j;
                float b = destTransformCounter;
                float Proportion = a / b;
                float result = Proportion * speed + Valeurinitiale;
                skin.SetBlendShapeWeight(0, result);
            }
            yield return new WaitForFixedUpdate();
        }
        Close();
    }


}

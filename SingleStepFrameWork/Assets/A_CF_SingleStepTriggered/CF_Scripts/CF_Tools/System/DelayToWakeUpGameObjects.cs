using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class DelayToWakeUpGameObjects : ReWriteGameControl_CF {
    /// <summary>
    /// 一次性事件列表
    /// </summary>
    List<Action> disposableList = new List<Action>();

    public void AddDisposableEvent(Action ac)
    {
        disposableList.Add(ac);
    }
    public Action CompleteEvent;

    float fadeTime = 0.1f;
    bool objState = true;
    List<Transform> destTransform=new List<Transform>();

    /// <summary>
    /// 激活物体并将其位移到指定位置
    /// </summary>
    /// <param name="objs">目标物体列表</param>
    /// <param name="t"> 间隔时间</param>
    /// <param name="destList">目标矩阵组件列表</param>
    /// <param name="state">设置物体的激活状态</param>
    public void Play(List<GameObject>objs,float t,List<Transform>destList,bool state=true)
    {
        destTransform.Clear();
        destTransform = destList;
        EventCenter_CF.Instance.CloseInteractiveControl();
        fadeTime = t;
        objState = state;
        StartCoroutine("PlayIe",objs);
    }
    /// <summary>
    /// 激活物体并将其位移到指定位置
    /// </summary>
    /// <param name="transforms">目标物体列表</param>
    /// <param name="t">间隔时间</param>
    /// <param name="destList">目标矩阵组件列表</param>
    /// <param name="state">设置物体的激活状态</param>
    public void Play(List<Transform> transforms, float t, List<Transform> destList, bool state = true)
    {
        destTransform.Clear();
        destTransform = destList;
        EventCenter_CF.Instance.CloseInteractiveControl();
        fadeTime = t;
        objState = state;
        List<GameObject> objs = new List<GameObject>();
        foreach (Transform item in transforms)
        {
            objs.Add(item.gameObject);
        }
        StartCoroutine("PlayIe",objs);
    }

    void Stop()
    {
        StopCoroutine("PlayIe");
        foreach (Action item in disposableList)
        {
            CompleteEvent += item;
        }
        CompleteEvent?.Invoke();
        EventCenter_CF.Instance.OpenInteractiveControl();
        foreach (Action item in disposableList)
        {
            CompleteEvent -= item;
        }
        disposableList.Clear();
    }



    IEnumerator PlayIe(List<GameObject> objs)
    {
        for (int i = 0; i < objs.Count; i++)
        {
            while (pauseGame)
            {
                yield return new WaitForSeconds(0.1f);
            }
            yield return new WaitForSeconds(fadeTime);
            objs[i].SetActive(objState);
            objs[i].transform.SetParent(null);
            objs[i].transform.localPosition = destTransform[i].localPosition;
            objs[i].transform.localEulerAngles = destTransform[i].localEulerAngles;
            objs[i].transform.localScale = destTransform[i].localScale;
        }
        Stop();
    }


}

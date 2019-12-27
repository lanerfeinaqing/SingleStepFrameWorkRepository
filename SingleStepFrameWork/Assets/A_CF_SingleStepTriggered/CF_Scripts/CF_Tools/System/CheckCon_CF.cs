using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[System.Serializable]
public class CheckObjectClass
{
    public  HighlightableObject hightLight;
    public GameObject namePanel;
    public bool setTransform = false;
    public Transform dest;
}

public class CheckCon_CF : ReWriteGameControl_CF {

    /// <summary>
    /// 一次性事件列表
    /// </summary>
    List<Action> disposableList = new List<Action>();

    public void AddDisposableEvent(Action ac)
    {
        disposableList.Add(ac);
    }

    public Action CompleteEvent;

    public CheckObjectClass[] checkObjs;
    public float fadeTime = 2;

    float frequence = 0.3f;
    HighlightableObject tempHigh;
    GameObject tempPanel;


    public void Play()
    {
        EventCenter_CF.Instance.CloseInteractiveControl();
        StartCoroutine("PlayIe");
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


    IEnumerator PlayIe()
    {
        for (int i = 0; i < checkObjs.Length; i++)
        {
            if(checkObjs[i].setTransform)
            {
                playerMoveMentController_CF.SetPlayerTransform(checkObjs[i].dest.localPosition, checkObjs[i].dest.localEulerAngles);
            }
            checkObjs[i].hightLight.FlashingOn();
            checkObjs[i].namePanel.SetActive(true);
            tempHigh = checkObjs[i].hightLight;
            tempPanel = checkObjs[i].namePanel;
            while (pauseGame)
            {
                yield return new WaitForSeconds(0.1f);
            }
            yield return new WaitForSeconds(fadeTime);
            if(tempHigh!=null)
            {
                tempHigh.FlashingOff();
                tempPanel.SetActive(false);
            }
        }
        Stop();
    }

    protected override void Awake()
    {
        base.Awake();
    }

    protected override void OnEnable()
    {
        base.OnEnable();
    }

    protected override void Start()
    {
        base.Start();
        foreach (CheckObjectClass item in checkObjs)
        {
            if(item.namePanel.activeInHierarchy)
            {
                item.namePanel.SetActive(false);
            }
        }
    }

    protected override void OnDisable()
    {
        base.OnDisable();
    }

    protected override void OnDestroy()
    {
        base.OnDestroy();
    }
}

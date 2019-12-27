using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;

[RequireComponent(typeof(HighlightableObject))]
public class EdgeHighlightController_CF : MonoBehaviour,IInteractiveControl {

    /// <summary>
    /// 点击事件
    /// </summary>
    public event Action clickObjectEvent;
    /// <summary>
    /// 一次性点击事件列表
    /// </summary>
    List<Action> disposableclickObjectEventList = new List<Action>();
    /// <summary>
    /// 一次性点击事件列表
    /// </summary>
    public void AddDisposableclickObjectEvent(Action ac)
    {
        disposableclickObjectEventList.Add(ac);
    }

    /// <summary>
    /// 发光事件
    /// </summary>
    public event Action turnOnEvent;
    /// <summary>
    /// 灭光事件
    /// </summary>
    public event Action turnOffEvent;
    /// <summary>
    /// 物体本身矩阵变换组件
    /// </summary>
    Transform thistransform;
    /// <summary>
    /// 物体对应名称3D界面
    /// </summary>
    GameObject nameCanvas;
    /// <summary>
    /// 物体高亮组件
    /// </summary>
    HighlightableObject highlightable;
    /// <summary>
    /// 暂停游戏标志位
    /// </summary>
    bool pauseGame = false;
    //物体的碰撞器组件们
    Collider[] collider;

    /// <summary>
    /// 退出界面引用
    /// </summary>
    EscapePanelScript_CF escape;
    /// <summary>
    /// 标记当前物体能否被点击
    /// </summary>
    public bool canBeClicked = false;

    public Color canBeClickedColor = Color.green;
    public Color canNotBeClickedColor = Color.red;

    private void Awake()
    {
        thistransform = transform;
        highlightable = GetComponent<HighlightableObject>();
        escape = FindObjectOfType<EscapePanelScript_CF>();
        if(thistransform.GetComponentInChildren<Canvas>()!=null)
        {
            nameCanvas = thistransform.GetComponentInChildren<Canvas>().gameObject;
            nameCanvas.SetActive(false);
        }
        else
        {
            nameCanvas = null;
            throw new Exception("获取名称失败!");
        }
        if (escape == null)
        {
            Debug.LogError("退出界面不存在或者获取失败");
        }
        else
        {
            escape.panelActiveEvent += PauseGameFunc;
            escape.panelDisActiveEvent += ReplayGameFunc;
        }
        collider = GetComponents<Collider>();
        if (collider == null)
        {
            Debug.LogError("本物体不存在碰撞器");
        }

    }
    // Use this for initialization
    void Start () {
        EventCenter_CF.Instance.AddInteractiveControl(this);
        CloseInteractive();
    }
	

    private void OnMouseEnter()
    {
        if(pauseGame)
        {
            return;
        }
        else
        {
            TurnOnFunc();
        }
    }

    private void OnMouseExit()
    {
        if (pauseGame)
        {
            return;
        }
        else
        {
            TurnOffFunc();
        }
    }
    

    private void OnMouseDown()
    {
        if (pauseGame)
        {
            return;
        }
        else
        {
            if (canBeClicked)
            {
                foreach (Action item in disposableclickObjectEventList)
                {
                    clickObjectEvent += item;
                }
                clickObjectEvent?.Invoke();
                foreach (Action item in disposableclickObjectEventList)
                {
                    clickObjectEvent -= item;
                }
                disposableclickObjectEventList.Clear();
                canBeClicked = false;
                highlightable.ConstantOnImmediate(canNotBeClickedColor);
                if(nameCanvas!=null)
                {
                    nameCanvas.SetActive(false);
                }
            }
        }
    }
    private void OnDestroy()
    {
        EventCenter_CF.Instance.RemoveInteractiveControl(this);
        escape.panelActiveEvent -= PauseGameFunc;
        escape.panelDisActiveEvent -= ReplayGameFunc;
    }

    void PauseGameFunc()
    {
        pauseGame = true;
        highlightable.ConstantOffImmediate();
        if(nameCanvas!=null)
        {
            nameCanvas.SetActive(false);
        }
    }

    void ReplayGameFunc()
    {
        pauseGame = false;
    }


    void TurnOnFunc()
    {
        if (Cursor.visible)
        {
            if(canBeClicked)
            {
                highlightable.ConstantOnImmediate(canBeClickedColor);
            }
            else
            {
                highlightable.ConstantOnImmediate(canNotBeClickedColor);
            }
            if (nameCanvas != null)
            {
                nameCanvas.SetActive(true);
            }
            turnOnEvent?.Invoke();
        }
    }

    void TurnOffFunc()
    {
        highlightable.ConstantOffImmediate();
        if (nameCanvas != null)
        {
            nameCanvas.SetActive(false);
        }
        turnOnEvent?.Invoke();
    }

    public void OpenInteractive()
    {
        foreach (Collider item in collider)
        {
            item.enabled = true;
        }
    }

    public void CloseInteractive()
    {
        foreach (Collider item in collider)
        {
            item.enabled = false;
        }
    }
    /// <summary>
    /// 开启此物体的点击触发事件
    /// </summary>
    public void SetCall()
    {
        canBeClicked = true;
    }

}

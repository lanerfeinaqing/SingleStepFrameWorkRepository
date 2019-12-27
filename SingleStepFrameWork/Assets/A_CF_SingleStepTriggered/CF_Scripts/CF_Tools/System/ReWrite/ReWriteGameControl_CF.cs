using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class ReWriteGameControl_CF : MonoBehaviour
{
    protected Transform thistransform;
    protected GameObject thisgameobject;
    public Transform ThisTransform
    {
        get { return thistransform; }
    }
    public GameObject ThisGameObject
    {
        get { return thisgameobject; }
    }
    /// <summary>
    /// 游戏物体默认激活标志位
    /// </summary>
    public bool defaultActivation = true;
    /// <summary>
    /// 非首次脚本激活
    /// </summary>
    protected bool notFirstEnable = false;
    /// <summary>
    /// 非首次脚本关闭
    /// </summary>
    protected bool notFirstDisable = false;
    /// <summary>
    /// 退出界面兼暂停游戏控制器
    /// </summary>
    protected EscapePanelScript_CF escapePanelScript_CF;
    /// <summary>
    /// 输入管理器
    /// </summary>
    protected InputManager_CF inputManager_CF;
    /// <summary>
    /// 鼠标控制器
    /// </summary>
    protected MouseController_CF mouseController_CF;
    /// <summary>
    /// 玩家控制器
    /// </summary>
    protected PlayerMoveMentController_CF playerMoveMentController_CF;
    /// <summary>
    /// 暂停游戏标志位
    /// </summary>
    protected bool pauseGame = false;




    //一些必用到的事件
    /// <summary>
    /// 在Awake中调用的调度器
    /// </summary>
    public event Action scripInitializeEvent;
    /// <summary>
    /// start中使用的调度器
    /// </summary>
    public event Action scripFirstActiveEvent;
    /// <summary>
    /// OnEnable中使用的调度器
    /// </summary>
    public event Action scriptEnableEvent;
    /// <summary>
    /// Disable中使用的调度器
    /// </summary>
    public event Action scriptDisableEvent;
    /// <summary>
    /// Destroy中使用的调度器
    /// </summary>
    public event Action scripDestroyEvent;


    protected virtual void Awake()
    {
        thistransform = transform;
        thisgameobject = gameObject;
        scripInitializeEvent?.Invoke();
        escapePanelScript_CF = FindObjectOfType<EscapePanelScript_CF>();
        if (escapePanelScript_CF == null)
        {
            Debug.LogError(thisgameobject.name+": 退出界面不存在或者获取失败");
        }
        else
        {
            escapePanelScript_CF.panelActiveEvent += PauseGameFunc;
            escapePanelScript_CF.panelDisActiveEvent += ReplayGameFunc;
        }
        inputManager_CF = FindObjectOfType<InputManager_CF>();
        if (inputManager_CF == null)
        {
            Debug.LogError("输入管理器不存在或者获取失败");
        }
        mouseController_CF = FindObjectOfType<MouseController_CF>();
        if (mouseController_CF == null)
        {
            Debug.LogError("鼠标管理器不存在或者获取失败");
        }
        playerMoveMentController_CF = FindObjectOfType<PlayerMoveMentController_CF>();
        if (playerMoveMentController_CF == null)
        {
            Debug.LogError("玩家控制器不存在或者获取失败");
        }
        else
        {
        }
        thisgameobject.SetActive(defaultActivation);
    }

    protected virtual void OnEnable()
    {
        if(notFirstEnable)
        {
            scriptEnableEvent?.Invoke();
        }
    }

    protected virtual void Start()
    {
        scripFirstActiveEvent?.Invoke();
        notFirstDisable = true;
        notFirstEnable = true;
    }

    protected virtual void OnDisable()
    {
        if(notFirstDisable)
        {
            scriptDisableEvent?.Invoke();
        }
    }

    protected virtual void OnDestroy()
    {
        escapePanelScript_CF.panelActiveEvent -= PauseGameFunc;
        escapePanelScript_CF.panelDisActiveEvent -= ReplayGameFunc;
        scripDestroyEvent?.Invoke();
    }

    /// <summary>
    /// 激活当前游戏物体
    /// </summary>
    public virtual void ActiveGameObject()
    {
        thisgameobject.SetActive(true);
    }
    /// <summary>
    /// 关闭当前游戏物体
    /// </summary>
    public virtual void DisActiveGameObject()
    {
        thisgameobject.SetActive(false);
    }
    /// <summary>
    /// 暂停
    /// </summary>
    public virtual void PauseGameFunc()
    {
        pauseGame = true;
    }
    /// <summary>
    /// 恢复
    /// </summary>
    public virtual void ReplayGameFunc()
    {
        pauseGame = false;
    }



    /// <summary>
    /// 设置物体位置和角度
    /// </summary>
    /// <param name="Position"></param>
    /// <param name="Rotation"></param>
    public virtual void SetPlayerTransform(Vector3 Position, Vector3 Rotation)
    {
        thistransform.localPosition = Position;
        thistransform.localEulerAngles = Rotation;
    }
    /// <summary>
    /// 设置物体位置和角度
    /// </summary>
    /// <param name="Position"></param>
    /// <param name="Rotation"></param>
    public virtual void SetPlayerTransform(Transform t)
    {
        thistransform.localPosition = t.position;
        thistransform.localEulerAngles = t.localEulerAngles;
    }





}

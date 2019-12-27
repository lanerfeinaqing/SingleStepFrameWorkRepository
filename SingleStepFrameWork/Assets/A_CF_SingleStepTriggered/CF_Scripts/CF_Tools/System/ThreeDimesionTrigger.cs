using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ThreeDimesionTrigger : MonoBehaviour {

    // <summary>
    /// 当前三维触发器是否启用
    /// </summary>
    public bool state = false;

    /// <summary>
    /// 初始时交互功能是否关闭
    /// </summary>
    public bool cancelInteraction = true;

    public UnityEvent scriptEventIn = new UnityEvent();
    public UnityEvent scriptEventOut = new UnityEvent();


    private void Awake()
    {
        if (cancelInteraction)
        {
            CloseInterface();
        }
        else
        {
            OpenInterface();
        }

    }

    // Use this for initialization
    void Start () {
		
	}


    /// <summary>
    /// 开启三维触发交互功能
    /// </summary>
    public virtual void OpenInterface()
    {
        state = true;
    }


    /// <summary>
    /// 关闭三维触发交互功能
    /// </summary>
    public virtual void CloseInterface()
    {
        state = false;
    }





}

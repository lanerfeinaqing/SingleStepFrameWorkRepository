using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventCenter_CF:MonoBehaviour
{
    /// <summary>
    /// 交互控制计数器
    /// </summary>
   int counter = 0;
    /// <summary>
    /// 交互标志位，默认不可交互
    /// </summary>
    bool currentInteractive = false; 
    /// <summary>
    /// 交互控制接口列表
    /// </summary>
    List<IInteractiveControl> InteractiveControlList = new List<IInteractiveControl>();
    /// <summary>
    /// 添加交互控制计数器
    /// </summary>
    public void OpenInteractiveControl()
    {
        counter += 1;
        if (counter>0&& currentInteractive==false)
        {
            if (Application.platform == RuntimePlatform.WindowsEditor)
            {
              //  print("开启高亮物体交互功能");
            }
            currentInteractive = true;
            foreach (IInteractiveControl item in InteractiveControlList)
            {
                item.OpenInteractive();
            }
            return;
        }
    }
    /// <summary>
    /// 移除交互控制计数器
    /// </summary>
    public void CloseInteractiveControl()
    {
        counter -= 1;
        if(counter<=0 && currentInteractive == true)
        {
            if (Application.platform == RuntimePlatform.WindowsEditor)
            {
                //print("关闭高亮物体交互功能");
            }
            currentInteractive = false;
            foreach (IInteractiveControl item in InteractiveControlList)
            {
                item.CloseInteractive();
            }
        }
    }
    /// <summary>
    /// 添加实现了接口InteractiveControl的对象进列表
    /// </summary>
    public void AddInteractiveControl(IInteractiveControl i)
    {
        if (!InteractiveControlList.Contains(i))
        {
            InteractiveControlList.Add(i);
        }
    }
    /// <summary>
    /// 移除实现了接口InteractiveControl的对象进列表
    /// </summary>
    public void RemoveInteractiveControl(IInteractiveControl i)
    {
        if (InteractiveControlList.Contains(i))
        {
            InteractiveControlList.Remove(i);
        }
    }







    static EventCenter_CF eventCenter;
    private void Awake()
    {
        eventCenter = this;
    }
    public static EventCenter_CF Instance
    {
        get { return eventCenter; }
    }

}

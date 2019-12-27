using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System;
using System.Text;
using System.Text.RegularExpressions;
using System.Net.NetworkInformation;
/// <summary>
/// 主界面一级按钮界面控制器，每点击一个Toggle，调出对应的二级界面。
/// </summary>
public class MainMeunToggleController_CF : MonoBehaviour {

    [Header("主界面按钮路径")]
    public string MainButtonPath = "Frame/UI/MainUIScene/MainButton";
    [Header("二级界面路径")]
    public string SecondPanelPath = "Frame/UI/MainUIScene/SeconSizedPanel";

    /// <summary>
    /// 主界面UI配置表
    /// </summary>
    TextAsset table;
    /// <summary>
    /// 一级界面下的所有toggle列表
    /// </summary>
    List<Toggle> toggles;
    /// <summary>
    /// 本身的transform组件
    /// </summary>
    RectTransform this_transform;
    /// <summary>
    /// 本身的ToggleGroup组件
    /// </summary>
    ToggleGroup group;
    /// <summary>
    /// 二级界面transform组件
    /// </summary>
    RectTransform secondSizePanelTransform;
    /// <summary>
    /// 二级界面游戏物体
    /// </summary>
    GameObject secondSizePanelGameObject;
    /// <summary>
    /// 二级界面在按钮层级下的本地坐标
    /// </summary>
    Vector3 secondSizePanelPosition = new Vector3(0, 0, 0);
	// Use this for initialization
	void Awake () {
        Init();
    }

    /// <summary>
    /// 获取本机的物理地址
    /// </summary>
    /// <returns></returns>
    string GetMacAddress()
    {
        string physicalAddress = "";

        NetworkInterface[] nice = NetworkInterface.GetAllNetworkInterfaces();

        foreach (NetworkInterface adaper in nice)
        {
            if (adaper.Description == "en0")
            {
                physicalAddress = adaper.GetPhysicalAddress().ToString();
                break;
            }
            else
            {
                physicalAddress = adaper.GetPhysicalAddress().ToString();

                if (physicalAddress != "")
                {
                    break;
                };
            }
        }

        return physicalAddress;
    }



    /// <summary>
    /// 初始化界面,读取主界面UI的数据信息并且声称相应按钮，再生成二级界面
    /// </summary>
    public void Init()
    {

        //获取本身transform和生成二级界面
        this_transform = transform as RectTransform;
        group = this_transform.GetComponent<ToggleGroup>();
        secondSizePanelGameObject = GameObject.Instantiate<GameObject>(Resources.Load<GameObject>(SecondPanelPath),this_transform,false);
        secondSizePanelTransform = secondSizePanelGameObject.transform as RectTransform;

        //生成主界面按钮们
        GameObject sourcesMainButton=Resources.Load<GameObject>(MainButtonPath);
        toggles = new List<Toggle>();
        for (int j = 0; j < GlobalConfigurationInformation_CF.MainPanelToggleObjName.Count; j++)
        {
            //临时的按钮位置组件
            RectTransform tempToggleTrans = null;
            //临时的按钮toggle组件
            Toggle t = null;
            tempToggleTrans = GameObject.Instantiate<GameObject>(sourcesMainButton, this_transform, false).transform as RectTransform;
            t = tempToggleTrans.GetComponent<Toggle>();
            tempToggleTrans.gameObject.name = GlobalConfigurationInformation_CF.MainPanelToggleObjName[j];
            tempToggleTrans.Find("lable").GetComponent<Text>().text = GlobalConfigurationInformation_CF.MainPanelToggleTextName[j];
            t.group = group;
            toggles.Add(t);
            ////运用lamada表达式为每个toggl添加值变化的事件，这样在保证参数正确的情况下，又能让toggle本身知道当前点击的是不是自己。自己监听自己，然后作出反应。
            t.onValueChanged.AddListener(value => { ClickToggleFunc(value, tempToggleTrans); });
        }
        sourcesMainButton = null;
    }



    public void ClickToggleFunc(bool value, RectTransform t_transform)
    {
        if(value)
        {
            secondSizePanelTransform.SetParent(t_transform.Find("ChildNode") as RectTransform, false);
            secondSizePanelTransform.localPosition = secondSizePanelPosition;
            secondSizePanelGameObject.SetActive(true);
        }
        else
        {
            if (t_transform.Find("ChildNode").childCount != 0)
            {
                secondSizePanelGameObject.SetActive(false);
            }
        }
    }

   


}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Text;
using System.Text.RegularExpressions;

public class GlobalConfigurationInformation_CF
{
    /// <summary>
    /// 存储主界面配置信息的文本资源对象，读取完毕后记得清空
    /// </summary>
    static TextAsset MainUISceneTable;
    /// <summary>
    /// 临时存储各个场景配置信息的文本资源对象，读取完毕后记得清空
    /// </summary>
    static TextAsset sceneTable;
    /// <summary>
    /// 主界面一级按钮内存对象名称列表
    /// </summary>
     static List<string> mainPanelToggleObjName = new List<string>();

    public static List<string> MainPanelToggleObjName
    {
        get { return mainPanelToggleObjName; }
    }


    /// <summary>
    /// 主界面一级按钮屏幕显示名称列表
    /// </summary>
    static List<string> mainPanelToggleTextName = new List<string>();

    public static List<string> MainPanelToggleTextName
    {
        get {return mainPanelToggleTextName; }
    }

    /// <summary>
    /// 步骤文本信息和场景名相对应的键值对的字典集合
    /// </summary>
    static Dictionary<string, List<ConfiguratioStruct_CF>> stepInformationDic = new Dictionary<string, List<ConfiguratioStruct_CF>>();

    public static Dictionary<string, List<ConfiguratioStruct_CF>> StepInformationDic
    {
        get {return stepInformationDic; }
    }

    /// <summary>
    /// 初始化配置信息
    /// </summary>
    public static void InitializationConfiguratonInformation(string s1,string s2)
    {
        LoadMainUISceneTable(s1);
        LoadAllSceneTable(s2);
    }
    /// <summary>
    /// 加载设置主界面按钮的中英文名称
    /// </summary>
    static void LoadMainUISceneTable(string fileName)
    {
        MainUISceneTable = Resources.Load<TextAsset>(fileName);
        string fileContent = MainUISceneTable.text;
        List<string> fileContentList = new List<string>(fileContent.Split('\n'));
        fileContentList.RemoveAt(0);
        var reg = new Regex("\\s+", RegexOptions.IgnoreCase);
        List<string> tempList = new List<string>();
        for (int j = 0; j < fileContentList.Count; j++)
        {
            fileContentList[j] = fileContentList[j].Trim();
            if (fileContentList[j].Length <= 0)
            {
                continue;
            }
            tempList.Clear();
            tempList = null;
            tempList = new List<string>(reg.Split(fileContentList[j]));
            tempList.Remove("");
            mainPanelToggleTextName.Add(tempList[0]);
            mainPanelToggleObjName.Add(tempList[1]);
        }
        MainUISceneTable = null;
    }

    /// <summary>
    /// 加载所有场景的文本配置信息
    /// </summary>
    static void LoadAllSceneTable(string filePath)
    {
        for (int i = 0; i < mainPanelToggleObjName.Count; i++)
        {
            string fileName = filePath;
            fileName += mainPanelToggleObjName[i];
            sceneTable= Resources.Load<TextAsset>(fileName);
            string fileContent = sceneTable.text;
            List<string> fileContentList = new List<string>(fileContent.Split('\n'));
            List<ConfiguratioStruct_CF> configuratioStructList = new List<ConfiguratioStruct_CF>();
            List<string> tempList = new List<string>();
            for (int j = 0; j < fileContentList.Count; j++)
            {
                fileContentList[j] = fileContentList[j].Trim();
                if (fileContentList[j].Length <= 0)
                {
                    continue;
                }
                tempList.Clear();
                tempList = null;
                tempList = new List<string>(fileContentList[j].Split('|'));
                tempList.Remove("");
                configuratioStructList.Add(new ConfiguratioStruct_CF(tempList[0], tempList[1], tempList[2]));
            }
            stepInformationDic[mainPanelToggleObjName[i]] = configuratioStructList;
        }
        sceneTable = null;
        //foreach (KeyValuePair<string, List<ConfiguratioStruct_CF>> item in stepInformationDic)
        //{
        //    Debug.Log(item);
        //    foreach (ConfiguratioStruct_CF item2 in item.Value)
        //    {
        //        Debug.Log(item2.ToString());
        //    }
        //}
    }



}

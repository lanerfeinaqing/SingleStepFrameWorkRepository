using System.Collections;
using System.Collections.Generic;
using UnityEngine;





/// <summary>
/// 项目中的一些全局变量放在这个类中
/// </summary>
public class GlobalProperty_CF
{
    /// <summary>
    /// 演示场景对应需要播放的视频名
    /// </summary>
    public static string videoName = null;
    /// <summary>
    /// 成绩查询场景对应的场景名
    /// </summary>
    public static string chengJiChaXunSceneName = null;
    /// <summary>
    /// 成绩列表
    /// </summary>
    public static List<int> scoreList = new List<int>();
    /// <summary>
    /// 当前游戏场景模式(注意是游戏场景，不是界面场景)
    /// </summary>
    public static SceneMode sceneMode = SceneMode.empty;

}

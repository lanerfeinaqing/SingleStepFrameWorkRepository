using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// 此类是一个大致的工具类，主要是应用于整个程序系统中，一些系统级的回退功能，比如全屏/分辨率设置/回主菜单/退出游戏等等。
/// </summary>
public class SystemBackController_CF : MonoBehaviour {

	public void ReturnMainUISceneFunc()
    {
        GlobalProperty_CF.sceneMode = SceneMode.empty;
        SceneManager.LoadScene(1);
    }

    public void ExitGame()
    {
        if(Application.platform == RuntimePlatform.WebGLPlayer)
        {
            if(Screen.fullScreen)
            {
                Screen.fullScreen = false;
            }
            else
            {
                Application.Quit();
            }
        }
        else
        {
            Application.Quit();
        }
    }

}

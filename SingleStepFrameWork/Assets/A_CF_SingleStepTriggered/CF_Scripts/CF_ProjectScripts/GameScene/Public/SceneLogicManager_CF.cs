using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using UnityEngine.SceneManagement;


public class SceneLogicManager_CF : MonoBehaviour {

    /// <summary>
    /// 场景名
    /// </summary>
     string sceneName;
    Transform Hud;
    Transform TiShiImage;
    Transform StepImage;
    Transform ExitShiYanBtn;
    StepBigImageScript stepBigImageScript;
    EscapePanelScript_CF escapePanel;

    /// <summary>
    /// 提示信息文本框
    /// </summary>
     Text tipsText;
    /// <summary>
    /// 步骤信息文本框
    /// </summary>
     Text stepText;
    /// <summary>
    /// 步骤计数器文本框
    /// </summary>
     Text stepCountText;
    /// <summary>
    /// 步骤信息计数器
    /// </summary>
    int stepsCounter = 0;
    /// <summary>
    /// 提示信息计数器
    /// </summary>
    int tipsCounter = 0;
    /// <summary>
    /// 步骤进度计数器
    /// </summary>
    int stepsRateCounter = 0;
    /// <summary>
    /// 考核模式下得分计数器
    /// </summary>
    int scoreCounter = 0;



    private void Awake()
    {
        escapePanel = FindObjectOfType<EscapePanelScript_CF>();
        escapePanel.yesEvent += Completionscore;
        sceneName = SceneManager.GetActiveScene().name;
        Hud= transform.parent.Find("HUD").transform;
        stepBigImageScript = FindObjectOfType<StepBigImageScript>();
        TiShiImage = transform.parent.Find("HUD/TiShiImage").transform;
        StepImage = transform.parent.Find("HUD/StepImage").transform;
        ExitShiYanBtn = transform.parent.Find("HUD/ExitShiYanBtn").transform;
        tipsText = TiShiImage.Find("TiShiText").GetComponent<Text>();
        stepText = StepImage.Find("Scroll View/Viewport/Content2Size/StepMessage").GetComponent<Text>();
        stepCountText = StepImage.Find("StepCount").GetComponent<Text>();
    }

    // Use this for initialization
    void  OnEnable ( ){
        //根据当前场景的不同模式初始化脚本
        if (GlobalProperty_CF.sceneMode == SceneMode.lianxi)
        {
            string content = "进度（" + "0%）：" + 0 + "/" + GlobalConfigurationInformation_CF.StepInformationDic[sceneName].Count;
            stepCountText.text = content;
            TiShiImage.gameObject.SetActive(false);
            StepImage.gameObject.SetActive(false);
            stepBigImageScript.gameObject.SetActive(false);
        }
        else if (GlobalProperty_CF.sceneMode == SceneMode.kaohe)
        {
            GlobalProperty_CF.scoreList.Clear();
            TiShiImage.gameObject.SetActive(false);
            StepImage.gameObject.SetActive(false);
            stepBigImageScript.gameObject.SetActive(false);
        }
        ExitShiYanBtn.gameObject.SetActive(false);
        Hud.gameObject.SetActive(false);

    }

    public void ActiveHud()
    {
        Hud.gameObject.SetActive(true);
    }

    public void DisActiveHud()
    {
        Hud.gameObject.SetActive(false);
    }


    /// <summary>
    /// 更新步骤信息文本框文字有动画
    /// </summary>
    public void UpdateStepsTextFunc()
    {
        if (GlobalProperty_CF.sceneMode == SceneMode.lianxi)
        {
            if(!StepImage.gameObject.activeInHierarchy)
            {
                StepImage.gameObject.SetActive(true);
            }
            string sss = "当前步骤:" + GlobalConfigurationInformation_CF.StepInformationDic[sceneName][stepsCounter].BuZhou;
            stepText.text = sss;
            UpdateTipsFunc(GlobalConfigurationInformation_CF.StepInformationDic[sceneName][stepsCounter].TiShi);
            if (!stepBigImageScript.gameObject.activeInHierarchy)
            {
                stepBigImageScript.gameObject.SetActive(true);
            }
            stepBigImageScript.PlayAnimation(sss);
            if (stepsCounter + 1 < GlobalConfigurationInformation_CF.StepInformationDic[sceneName].Count)
            {
                stepsCounter += 1;
            }
        }

    }

    /// <summary>
    /// 通过外部传参数来设置某一个步骤的步骤文本信息
    /// </summary>
    /// <param name="message"></param>
    public void UpdateStepsTextFunc(string message)
    {
        if (GlobalProperty_CF.sceneMode == SceneMode.lianxi)
        {
            if (!StepImage.gameObject.activeInHierarchy)
            {
                StepImage.gameObject.SetActive(true);
            }
            string sss = "当前步骤:" + message;
            stepText.text = sss;
            UpdateTipsFunc(GlobalConfigurationInformation_CF.StepInformationDic[sceneName][stepsCounter].TiShi);
            if (!stepBigImageScript.gameObject.activeInHierarchy)
            {
                stepBigImageScript.gameObject.SetActive(true);
            }
            stepBigImageScript.PlayAnimation(sss);
            if (stepsCounter + 1 < GlobalConfigurationInformation_CF.StepInformationDic[sceneName].Count)
            {
                stepsCounter += 1;
            }
        }

    }






    /// 更新步骤信息文本框文字无动画
    /// </summary>
    public void UpdateStepsTextNoAnimationFunc(string message)
    {
        if (GlobalProperty_CF.sceneMode == SceneMode.lianxi)
        {
            if (!StepImage.gameObject.activeInHierarchy)
            {
                StepImage.gameObject.SetActive(true);
            }
            string sss = "当前步骤:" + message;
            stepText.text = sss;
            UpdateTipsFunc(GlobalConfigurationInformation_CF.StepInformationDic[sceneName][stepsCounter].TiShi);
            if (stepsCounter + 1 < GlobalConfigurationInformation_CF.StepInformationDic[sceneName].Count)
            {
                stepsCounter += 1;
            }
        }
    }
    /// <summary>
    /// 通过外部传参数来设置某一个步骤的步骤文本信息
    /// </summary>
    public void UpdateStepsTextNoAnimationFunc()
    {
        if (GlobalProperty_CF.sceneMode == SceneMode.lianxi)
        {
            if (!StepImage.gameObject.activeInHierarchy)
            {
                StepImage.gameObject.SetActive(true);
            }
            string sss = "当前步骤:" + GlobalConfigurationInformation_CF.StepInformationDic[sceneName][stepsCounter].BuZhou;
            stepText.text = sss;
            UpdateTipsFunc(GlobalConfigurationInformation_CF.StepInformationDic[sceneName][stepsCounter].TiShi);
            if (stepsCounter + 1 < GlobalConfigurationInformation_CF.StepInformationDic[sceneName].Count)
            {
                stepsCounter += 1;
            }
        }

    }















    /// <summary>
    /// 更新提示信息文本框
    /// </summary>
    void UpdateTipsFunc(string s)
    {
        if(!s.Equals("nothing"))
        {
            if (!TiShiImage.gameObject.activeInHierarchy)
            {
                TiShiImage.gameObject.SetActive(true);
            }
            tipsText.text = s;
        }
    }




/// <summary>
/// 更新步骤信息文本框进度
/// </summary>
public void UpdateStepsCountFunc()
    {
        if (GlobalProperty_CF.sceneMode == SceneMode.lianxi)
        {
            if (stepsRateCounter + 1 <= GlobalConfigurationInformation_CF.StepInformationDic[sceneName].Count)
            {
                stepsRateCounter += 1;
            }
            if (!StepImage.gameObject.activeInHierarchy)
            {
                StepImage.gameObject.SetActive(true);
            }
            float a = (float)stepsRateCounter;
            float b = (float)GlobalConfigurationInformation_CF.StepInformationDic[sceneName].Count;
            float n = (float)(a / b);
            int m = (int)(n * 100);
            string content = "进度（" + m + "%）：" + stepsRateCounter + "/" + GlobalConfigurationInformation_CF.StepInformationDic[sceneName].Count;
            stepCountText.text = content;
            if (stepsRateCounter == GlobalConfigurationInformation_CF.StepInformationDic[sceneName].Count)
            {
                stepCountText.color = Color.green;
            }
        }

    }

    /// <summary>
    /// 考核模式下进行得分
    /// </summary>
    public void GetFractions()
    {
        if (GlobalProperty_CF.sceneMode == SceneMode.kaohe)
        {
            GlobalProperty_CF.scoreList.Add(GlobalConfigurationInformation_CF.StepInformationDic[sceneName][scoreCounter].Score);
            if (scoreCounter + 1 <= GlobalConfigurationInformation_CF.StepInformationDic[sceneName].Count)
            {
                scoreCounter += 1;
            }
        }

    }


    /// <summary>
    /// 完成所有步骤
    /// </summary>
    public void CompleteAllSteps()
    {
        if (GlobalProperty_CF.sceneMode == SceneMode.lianxi)
        {
            stepText.text = "当前步骤:实验结束，请退出。";
            tipsText.gameObject.SetActive(false);
            TiShiImage.gameObject.SetActive(false);
            ExitShiYanBtn.gameObject.SetActive(true);
        }
        else if (GlobalProperty_CF.sceneMode == SceneMode.kaohe)
        {
            ExitShiYanBtn.gameObject.SetActive(true);
        }

    }

    /// <summary>
    /// 开始执行操作步骤,练习模式在此步骤改变步骤进度，考核模式在此设置得分
    /// </summary>
    public void StartExecutionOperation()
    {
        if (GlobalProperty_CF.sceneMode == SceneMode.lianxi)
        {
            UpdateStepsCountFunc();
            stepBigImageScript.StopAnimation();
        }
        else if (GlobalProperty_CF.sceneMode == SceneMode.kaohe)
        {
            GetFractions();
        }
    }


    private void OnDestroy()
    {
        GlobalProperty_CF.sceneMode = SceneMode.empty;
    }

    /// <summary>
    /// 在中途退回主关卡时，填充数量不足的分数为0
    /// </summary>
    private void Completionscore()
    {
        if (GlobalProperty_CF.sceneMode == SceneMode.kaohe)
        {
            int temp = GlobalProperty_CF.scoreList.Count;
            for (int i = temp; i < GlobalConfigurationInformation_CF.StepInformationDic[sceneName].Count; i++)
            {
                GlobalProperty_CF.scoreList.Add(0);
            }
        }
    }
}

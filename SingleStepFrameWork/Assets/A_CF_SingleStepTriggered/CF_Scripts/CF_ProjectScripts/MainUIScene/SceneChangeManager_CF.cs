using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

/// <summary>
/// 主界面跳转场景的场景管理器
/// </summary>
public class SceneChangeManager_CF : MonoBehaviour
{
     Transform thistransform;
     Transform mainUIPanel;
     Transform SceneProgressBarPanel;
     Image scrollbar;
     Text rate;

    private AsyncOperation operation;

    private void Awake()
    {
        thistransform = transform;
        mainUIPanel = GameObject.Find("MainUIPanel").transform;
        SceneProgressBarPanel = thistransform.GetChild(thistransform.childCount - 1);
        Transform sliderBack = SceneProgressBarPanel.Find("SizeBar/sliderBack");
        scrollbar = sliderBack.Find("sliderArea").GetComponent<Image>();
        rate = sliderBack.Find("sliderText").GetComponent<Text>();
    }


    /// <summary>
    /// 异步加载场景
    /// </summary>
    /// <param name="_name"></param>
    /// <returns></returns>
    IEnumerator AsyncLoading(string _name)
    {
        int displayProgress = 0;
        int toProgress = 0;
        //异步加载关卡
        operation = SceneManager.LoadSceneAsync(_name);
        //阻止当加载完成自动切换
        operation.allowSceneActivation = false;
        while(operation.progress<0.9f)
        {
            toProgress = (int)operation.progress * 100;
            while(displayProgress<toProgress)
            {
                ++displayProgress;
                SetLoadingPercentage(displayProgress);
                yield return new WaitForFixedUpdate();
            }
        }
        toProgress = 100;
        while(displayProgress<toProgress)
        {
            ++displayProgress;
            SetLoadingPercentage(displayProgress);
            yield return new WaitForFixedUpdate();
        }
        yield return new WaitForSeconds(0.5f);
        CompleteLoadScene();
    }

    void SetLoadingPercentage(int f1)
    {
        rate.text = f1 + "%";
        float a = f1;
        float b = 100;
        float c = a / b;
        scrollbar.fillAmount = c;
    }
    /// <summary>
    /// 开始加载关卡
    /// </summary>
    /// <param name="name_"></param>
    void StartLoadScene(string name_)
    {
        mainUIPanel.gameObject.SetActive(false);
        SceneProgressBarPanel.gameObject.SetActive(true);
        StartCoroutine("AsyncLoading",name_);
    }
    /// <summary>
    /// 关卡加载完成
    /// </summary>
    void CompleteLoadScene()
    {
        StopCoroutine("AsyncLoading");
        operation.allowSceneActivation = true;
    }


    /// <summary>
    /// 跳转到练习场景
    /// </summary>
    /// <param name="targetSceneName"></param>
    public void GoToLianxiScene(string targetSceneName)
    {
        if (targetSceneName!=null&&targetSceneName!="")
        {
            GlobalProperty_CF.sceneMode = SceneMode.lianxi;
            StartLoadScene(targetSceneName);
        }
    }


    /// <summary>
    /// 跳转到考核场景
    /// </summary>
    /// <param name="targetSceneName"></param>
    public void GoToKaoHeScene(string targetSceneName)
    {
        GlobalProperty_CF.sceneMode = SceneMode.kaohe;
        StartLoadScene(targetSceneName);
    }

    /// <summary>
    /// 跳转到演示场景
    /// </summary>
    /// <param name="targetSceneName"></param>
    public void GoToYanshiScene(string targetSceneName)
    {
        if (targetSceneName != null && targetSceneName != "")
        {
            GlobalProperty_CF.videoName = null;
            GlobalProperty_CF.videoName = targetSceneName + ".mp4";
            StartLoadScene("YanShiScene");
        }
    }

    /// <summary>
    /// 跳转到成绩查询场景
    /// </summary>
    /// <param name="targetSceneName"></param>
    public void GoToChengJiScene(string targetSceneName)
    {
        if (targetSceneName != null && targetSceneName != "")
        {
            GlobalProperty_CF.chengJiChaXunSceneName = null;
            GlobalProperty_CF.chengJiChaXunSceneName = targetSceneName;
            StartLoadScene("ChaXunScene");
        }
    }






}

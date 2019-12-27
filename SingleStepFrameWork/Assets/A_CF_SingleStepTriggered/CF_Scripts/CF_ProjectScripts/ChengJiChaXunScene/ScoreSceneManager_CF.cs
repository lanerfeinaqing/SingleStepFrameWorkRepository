using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreSceneManager_CF : MonoBehaviour {

    [Header("分数预制体路径")]
    public string ScoreMessageObjPath = "Frame/UI/ChengJiChaXunScene/ScoreMessage";

    /// <summary>
    /// 列表容器
    /// </summary>
    RectTransform content;
    private void Awake()
    {
        content = transform.Find("ScrollView/Viewport/Content") as RectTransform;
       
    }

    // Use this for initialization
    void Start () {
        Init();
    }

    private void Init()
    {
        int allScore = 0;
        GameObject createObj = Resources.Load<GameObject>(ScoreMessageObjPath);
        if(GlobalConfigurationInformation_CF.StepInformationDic[GlobalProperty_CF.chengJiChaXunSceneName].Count!= GlobalProperty_CF.scoreList.Count)
        {
            for (int i = 0; i < GlobalConfigurationInformation_CF.StepInformationDic[GlobalProperty_CF.chengJiChaXunSceneName].Count; i++)
            {
                //创建列表物体，并且设置其位置和大小，最后把对应的字典数据填充在物体中
                GameObject tempObj = GameObject.Instantiate<GameObject>(createObj, content, false);
                ScoreMessageObjManager_CF sore = tempObj.GetComponent<ScoreMessageObjManager_CF>();
                ConfiguratioStruct_CF cf = GlobalConfigurationInformation_CF.StepInformationDic[GlobalProperty_CF.chengJiChaXunSceneName][i];
                sore.SetDescribeMessage(cf.BuZhou, 0);
            }
        }
        else
        {
            for (int i = 0; i < GlobalConfigurationInformation_CF.StepInformationDic[GlobalProperty_CF.chengJiChaXunSceneName].Count; i++)
            {
                //创建列表物体，并且设置其位置和大小，最后把对应的字典数据填充在物体中
                GameObject tempObj = GameObject.Instantiate<GameObject>(createObj, content, false);
                ScoreMessageObjManager_CF sore = tempObj.GetComponent<ScoreMessageObjManager_CF>();
                ConfiguratioStruct_CF cf = GlobalConfigurationInformation_CF.StepInformationDic[GlobalProperty_CF.chengJiChaXunSceneName][i];
                sore.SetDescribeMessage(cf.BuZhou, GlobalProperty_CF.scoreList[i]);
                allScore += GlobalProperty_CF.scoreList[i];
            }
        }
        GameObject tempObj2 = GameObject.Instantiate<GameObject>(createObj, content);
        ScoreMessageObjManager_CF sore2 = tempObj2.GetComponent<ScoreMessageObjManager_CF>();
        sore2.SetDescribeMessage("总分", allScore);
        createObj = null;
    }



}

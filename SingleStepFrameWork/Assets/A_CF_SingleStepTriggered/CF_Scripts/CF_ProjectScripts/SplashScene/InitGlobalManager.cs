using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class InitGlobalManager : MonoBehaviour {


    [Header("主界面配置表路径")]
    public string MainTablePath = "Frame/TxtFile/MainUISceneTable";
    [Header("场景配置表路径")]
    public string SceneTablePath = "Frame/TxtFile/";


    private void Awake()
    {
        GlobalConfigurationInformation_CF.InitializationConfiguratonInformation(MainTablePath, SceneTablePath);
    }

    // Use this for initialization
    void Start () {
        SceneManager.LoadScene(1);
	}
	
	
}

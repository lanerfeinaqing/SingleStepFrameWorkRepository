using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// 主界面二级按钮界面控制器，每点击一个对应按钮，进入到对应的场景模式中。
/// </summary>
public class MainMeunTwoButtonController_CF : MonoBehaviour {

    string sceneName = "";
    Transform thisTrasnform;
    SceneChangeManager_CF sceneChangeManager;

    private void Awake()
    {
        thisTrasnform = transform;
        sceneChangeManager = FindObjectOfType<SceneChangeManager_CF>();
        thisTrasnform.Find("YanShi").GetComponent<Button>().onClick.AddListener(YanShi);
        thisTrasnform.Find("LianXi").GetComponent<Button>().onClick.AddListener(LianXi);
        thisTrasnform.Find("KaoHe").GetComponent<Button>().onClick.AddListener(KaoHe);
        thisTrasnform.Find("ChengJiChaXun").GetComponent<Button>().onClick.AddListener(ChengJiChaXun);
    }

    // Use this for initialization
    void Start () {
		
	}

    public void SetSceneName(string s)
    {
        s = s.Trim();
        sceneName = s;
    }

    public string GetSceneName()
    {
        return sceneName;
    }

    /// <summary>
    /// 寻找对应场景名称
    /// </summary>
    public void FindCorrespondingSceneName()
    {
        SetSceneName(thisTrasnform.parent.parent.name);
    }

    public void YanShi()
    {
        FindCorrespondingSceneName();
        //TODO
        sceneChangeManager.GoToYanshiScene(sceneName);
    }

    public void LianXi()
    {
        FindCorrespondingSceneName();
        //TODO
        sceneChangeManager.GoToLianxiScene(sceneName);
    }

    public void KaoHe()
    {
        FindCorrespondingSceneName();
        //TODO
        sceneChangeManager.GoToKaoHeScene(sceneName);
    }

    public void ChengJiChaXun()
    {
        FindCorrespondingSceneName();
        //TODO
        sceneChangeManager.GoToChengJiScene(sceneName);
    }


}

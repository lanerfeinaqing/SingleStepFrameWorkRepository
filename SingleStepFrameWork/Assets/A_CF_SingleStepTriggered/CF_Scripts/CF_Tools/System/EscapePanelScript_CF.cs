using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System;
using UnityEngine.UI;

public class EscapePanelScript_CF : MonoBehaviour {


    GameObject self = null;
    Transform selftransform=null;
    Button yes;
    Button no;
    InputManager_CF inputManager;
    /// <summary>
    /// 点击否按钮时，触发的监听事件
    /// </summary>
    public event Action panelActiveEvent;
    public event Action panelDisActiveEvent;
    public event Action yesEvent;
    private void Awake()
    {
        self = gameObject;
        selftransform = transform;
        inputManager = FindObjectOfType<InputManager_CF>();
        Transform temp = selftransform.Find("Mask/back");
        yes = temp.Find("yesBtn").GetComponent<Button>();
        no = temp.Find("noBtn").GetComponent<Button>();
        yes.onClick.AddListener(YesBtn);
        no.onClick.AddListener(NoBtn);
        if(inputManager==null)
        {
            Debug.LogError("输入管理器不存在或者获取失败");
        }
        else
        {
            if (Application.platform == RuntimePlatform.WindowsEditor)
            {
                inputManager.tabClickEvent += ESCFunc;
            }
            else
            {
                inputManager.escClickEvent += ESCFunc;
            }
        }
    }

    // Use this for initialization
    void Start () {
        self.SetActive(false);
	}
	

     void YesBtn()
    {
        yesEvent?.Invoke();
        GlobalProperty_CF.sceneMode = SceneMode.empty;
        SceneManager.LoadScene(1);
    }

      void NoBtn()
    {
        DisActive();
    }

     void Active()
    {
        self.SetActive(true);
        panelActiveEvent?.Invoke();
    }

    void DisActive()
    {
        panelDisActiveEvent?.Invoke();
        self.SetActive(false);
    }

    void ESCFunc()
    {
        if(self.activeInHierarchy)
        {
            DisActive();
        }
        else
        {
            Active();
        }
    }

    private void OnDestroy()
    {
        if (Application.platform == RuntimePlatform.WindowsEditor)
        {
            inputManager.tabClickEvent -= ESCFunc;
        }
        else
        {
            inputManager.escClickEvent -= ESCFunc;
        }
    }

}

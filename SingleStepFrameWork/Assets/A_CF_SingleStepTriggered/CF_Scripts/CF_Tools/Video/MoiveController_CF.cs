
using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.Video;
using UnityEngine.EventSystems;

public class MoiveController_CF : MonoBehaviour,IPointerClickHandler
{
    Transform thistrasnform;
    /// <summary>
    /// 系统退出界面的脚本引用
    /// </summary>
    EscapePanelScript_CF escapePanel;

    //视频控制器
     VideoPlayer _moiveCon;
    //视频进度条
     GameObject sliderParentObj;
     Slider _slider;

    //退出按钮
     GameObject quitObj;

    //上一帧鼠标指针位置
    Vector3 lastFrameMousePosition = new Vector3();
    //本帧鼠标指针位置
    Vector3 currentFrameMousePosition=new Vector3();
    /// <summary>
    /// 鼠标指针自动消失计时器，放至不动查看过两秒就会自动消失
    /// </summary>
    float counter=0;
    bool openCounter = false;

    string url = "";
    [HideInInspector]
    public bool isSelfCon = true;

    private void Awake()
    {
        thistrasnform = transform;
        _moiveCon = thistrasnform.Find("Moive").GetComponent<VideoPlayer>();
        sliderParentObj = _moiveCon.transform.Find("DetectionImage").gameObject;
        _slider = sliderParentObj.transform.Find("ProcessSlider").GetComponent<Slider>();
        quitObj = _moiveCon.transform.Find("Back").gameObject;
        GameObject obj = GameObject.Find("EscapePanel");
        if(obj == null)
        {
            Debug.LogError("获取ESC退出界面引用失败");
        }
        else
        {
            escapePanel= obj.GetComponent<EscapePanelScript_CF>();
        }
        escapePanel.panelDisActiveEvent += MoviePlay;
        escapePanel.panelActiveEvent += MoviePause;


    }

    void Start()
    {
        _slider.minValue = 0;
        _slider.maxValue = 1;
        _slider.value = 0;
        quitObj.transform.GetChild(0).GetComponent<Button>().onClick.AddListener(BackBtn);
        quitObj.SetActive(false);
        _slider.gameObject.SetActive(false);
        sliderParentObj.SetActive(false);
        if(string.Empty== GlobalProperty_CF.videoName)
        {
            Debug.LogError("请从主场景进入，目前得不到视频名称");
            return;
        }
         if(Application.platform == RuntimePlatform.WebGLPlayer)
        {
            url = Application.streamingAssetsPath +"/"+ GlobalProperty_CF.videoName ;
            url = url.Replace("file:/","");
        }
        else 
        {
            url = Application.streamingAssetsPath + "/" + GlobalProperty_CF.videoName;
        }
        _moiveCon.loopPointReached += EndMoive;
        _moiveCon.url = url;
        Cursor.visible = false;
        StartPlayMoive(_moiveCon);
    }


    

    void BackBtn()
    {
       SceneManager.LoadScene(1);
    }

    void MoviePause()
    {
        _moiveCon.Pause();
    }

    void MoviePlay()
    {
        _moiveCon.Play();
    }

    void StartPlayMoive(VideoPlayer con)
    {
        sliderParentObj.SetActive(true);
        _moiveCon.Play();
    }

    void EndMoive(VideoPlayer con)
    {
        sliderParentObj.SetActive(false);
        SceneManager.LoadScene(1);
    }


    private void Update()
    {

        CaulateDelta();
        Timer();

        if (isSelfCon)
        {
            _slider.value = ((float)_moiveCon.frame) / ((float)_moiveCon.frameCount);
        }
        else
        {
            _moiveCon.frame =(long)(_moiveCon.frameCount * _slider.value);
        }
    }

    public VideoPlayer MovieConFunc()
    {
        return _moiveCon;
    }

    public void SliderState(bool state)
    {
        _slider.gameObject.SetActive(state);
    }

     /// <summary>
     /// 计算目前鼠标是否移动，如果正在移动而且指针不显示，那就开启。反之亦然。
     /// </summary>
    void CaulateDelta()
    {
        currentFrameMousePosition = Input.mousePosition;
        bool moving=currentFrameMousePosition != lastFrameMousePosition;
        if(moving)
        {
            if(Cursor.visible==false)
            {
                Cursor.visible = true;
                openCounter = false;
                counter = 0;
            }
        }
        else
        {
            if (Cursor.visible == true)
            {
                openCounter = true;
            }
        }
        lastFrameMousePosition = Input.mousePosition;

    }

    /// <summary>
    /// 鼠标指针自动消失计时器
    /// </summary>
    void Timer()
    {
        if(openCounter)
        {
            if(counter>=3)
            {
                Cursor.visible = false;
                openCounter = false;
                counter = 0;
            }
            else
            {
                counter += Time.deltaTime;
            }  
        }
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if(eventData.pointerId==-2)
        {
            if (quitObj.activeInHierarchy)
            {
                quitObj.SetActive(false);
            }
            else
            {
                quitObj.SetActive(true);
            }
        }
    }


    private void OnDestroy()
    {
        escapePanel.panelDisActiveEvent -= MoviePlay;
        escapePanel.panelActiveEvent -= MoviePause;
        Cursor.visible = true;
    }





}

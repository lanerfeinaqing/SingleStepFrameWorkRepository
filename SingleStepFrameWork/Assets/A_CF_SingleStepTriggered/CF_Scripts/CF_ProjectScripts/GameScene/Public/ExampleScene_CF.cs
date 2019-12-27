using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Reflection;
using System;

public class ExampleScene_CF : MonoBehaviour {

    List<MethodInfo> bindingFuncList = new List<MethodInfo>();
    int counter = 0;

    [Header("首页弹窗")]
    public ButtonPanel_CF HomeWindowPanel;
    [Header("玩家控制器")]
    public PlayerMoveMentController_CF player ;
    [Header("场景逻辑控制器")]
    public SceneLogicManager_CF sceneLogicManager;
    [Header("任务引导")]
    public TaskDirectArrow_CF taskDirectArrow;
    [Header("检查物体面板")]
    public ButtonPanel_CF checkPanel;
    [Header("检查物体控制器")]
    public CheckCon_CF checkCon;
    [Header("延时事件缓冲器")]
    public DelayEventBuffe delayEventBuffer;
    [Header("延时唤醒游戏物体")]
    public DelayToWakeUpGameObjects delayToWakeUpGameObjectsCon;
    [Header("分割符")]
    public string _____________;
    [Header("试管架")]
    public EdgeHighlightController_CF ShiGuanJia;
    [Header("工作台门")]
    public ReWriteGameControl_CF WorkDeskDoor;
    [Header("脱脂乳")]
    public PathTransformAnimationClass_CF TuoZhiRu;
    [Header("脱脂乳高亮")]
    public EdgeHighlightController_CF TuoZhiRuLight;
    [Header("移液器支架")]
    public EdgeHighlightController_CF YiYeQiJia;
    [Header("移液器")]
    public PathTransformAnimationClass_CF YiYeQi;
    [Header("移液器扳机")]
    public PathTransformAnimationClass_CF BanJi;
    [Header("移液器枪头")]
    public GameObject QiangTou;
    [Header("离心管1")]
    public PathTransformAnimationClass_CF LiXinGuan1;
    [Header("离心管盖子1")]
    public GameObject LiXinGuanGaiZi1;
    [Header("离心管1液面1号")]
    public BlendShapesAnimation_CF Guan1_YeMian_1;
    [Header("离心管1液面2号")]
    public BlendShapesAnimation_CF Guan1_YeMian_2;
    [Header("烧杯装三氯乙酸溶液")]
    public PathTransformAnimationClass_CF ShaoBei_SanLvYiSuanRongYe;
    [Header("烧杯装三氯乙酸溶液高亮")]
    public EdgeHighlightController_CF ShaoBei_SanLvYiSuanLight;
    [Header("烧杯装乙腈溶液")]
    public PathTransformAnimationClass_CF ShaoBei_YiJing;
    [Header("烧杯装乙腈溶液高亮")]
    public EdgeHighlightController_CF ShaoBei_YiJingLight;
    [Header("玻璃纤维滤纸高亮")]
    public EdgeHighlightController_CF BoLiXianWeiLvZhiLight;
    [Header("玻璃纤维滤纸")]
    public PathTransformAnimationClass_CF BoLiXianWeiLvZhi;
    [Header("玻璃纤维漏斗")]
    public ReWriteGameControl_CF BoLiXianWeiLouDou;
    [Header("离心管2")]
    public PathTransformAnimationClass_CF LiXinGuan2;
    [Header("离心管盖子2")]
    public GameObject LiXinGuanGaiZi2;
    [Header("离心管2液面1号")]
    public BlendShapesAnimation_CF Guan2_YeMian_1;
    [Header("离心管2液面2号")]
    public BlendShapesAnimation_CF Guan2_YeMian_2;
    [Header("离心管3")]
    public PathTransformAnimationClass_CF LiXinGuan3;
    [Header("离心管盖子3")]
    public GameObject LiXinGuanGaiZi3;
    [Header("离心管3液面1号")]
    public BlendShapesAnimation_CF Guan3_YeMian_1;
    [Header("离心管3液面2号")]
    public BlendShapesAnimation_CF Guan3_YeMian_2;
    [Header("倒牛奶特效")]
    public GameObject milkWater;
    [Header("溶液过滤计时器")]
    public PictureTimer_CF RongYeGuoLvTimer;
    [Header("过滤滴液特效")]
    public ParticleSystem RongYeGuoLvDiYeParticle;
    [Header("超声器箱盖")]
    public PathTransformAnimationClass_CF chaoShengYiGaiZi;
    [Header("超声提取计时器")]
    public PictureTimer_CF ChaoShengTiQuTimer;
    [Header("震荡提取计时器")]
    public PictureTimer_CF ZhenDangTiQuTimer;
    [Header("超声仪高亮")]
    public EdgeHighlightController_CF ChaoShengYiLight;
    [Header("高速离心机高亮")]
    public EdgeHighlightController_CF GaoSuLiXinLight;
    [Header("离心机箱盖")]
    public PathTransformAnimationClass_CF LiXinJiGaiZi;
    [Header("离心机机芯")]
    public PathTransformAnimationClass_CF LiXinJiJiXin;
    [Header("高速离心计时器")]
    public PictureTimer_CF LiXinJiTimer;
    [Header("无菌吸管")]
    public PathTransformAnimationClass_CF WuJunXiGuan;
    [Header("纯净水动画组件")]
    public PathTransformAnimationClass_CF Water;
    [Header("固相萃取柱架子高亮")]
    public EdgeHighlightController_CF CuiQuZhuJiaZiLight;
    [Header("固相萃取柱架子")]
    public PathTransformAnimationClass_CF CuiQuZhuJiaZi;
    [Header("倒滤液进亲和柱特效")]
    public ParticleSystem lvYeGoToImmunoaffinity;
    [Header("固相萃取仪高亮")]
    public EdgeHighlightController_CF GuXiangCuiQuYiLight;
    [Header("固相萃取仪门")]
    public PathTransformAnimationClass_CF GuXiangCuiQuYiDoor;
    [Header("固相萃取仪发亮屏幕")]
    public GameObject lightScreen;
    [Header("从左至右第一根免疫亲和柱")]
    public PathTransformAnimationClass_CF firstImmunoaffinity;
    [Header("免疫亲和柱们")]
    public List<Transform> immunoaffinityArray;
    [Header("免疫亲和柱们在萃取仪上的位置")]
    public List<Transform> immunoaffinityDestTransformArray;
    [Header("固相萃取仪计时器")]
    public PictureTimer_CF CuiQuTimer;
    [Header("液相色谱仪高亮")]
    public EdgeHighlightController_CF HPLCLight;
    [Header("分析结果面板")]
    public ButtonPanel_CF resultPanel;









    // Use this for initialization
    void Start () {
        AddBindingFuncToList();
    }

    /// <summary>
    /// 自动绑定本脚本中的所有绑定方法到绑定列表中去(每个不同的脚本类型名一定要修改)
    /// </summary>
    private void AddBindingFuncToList()
    {
        Type t = this.GetType();
        IEnumerable<MethodInfo> m = t.GetRuntimeMethods();
        foreach (MethodInfo item in m)
        {
            if (item.Name.Contains("_"))
            {
                string[] nameTag = item.Name.Split('_');
                if (nameTag[1].Equals("niko"))
                {
                    bindingFuncList.Add(item);
                }
            }
        }

        //foreach (var item in bindingFuncList)
        //{
        //    print(item.Name);
        //}
        ReadFuncListToBinding();

    }

    /// <summary>
    /// 更新步骤函数，将几个常用的函数归纳到一起
    /// </summary>
    /// <param name="animation"></param>
    /// <param name="t"></param>
    /// <param name="edgeHighlight"></param>
    void UpdateStepFunc(bool animation = true, string t = null, EdgeHighlightController_CF edgeHighlight = null)
    {
        if (animation)
        {
            sceneLogicManager.UpdateStepsTextFunc();
        }
        else
        {
            sceneLogicManager.UpdateStepsTextNoAnimationFunc();
        }
        if (t != null)
        {
            taskDirectArrow.Open(t);
        }
        if (edgeHighlight != null)
        {
            edgeHighlight.SetCall();
        }
    }

    /// <summary>
    /// 执行步骤函数
    /// </summary>
    void ExcuteStepFunc()
    {
        sceneLogicManager.StartExecutionOperation();
        taskDirectArrow.Close();
    }

    /// <summary>
    /// 读取函数列表中的方法，将其绑定
    /// </summary>
    void ReadFuncListToBinding()
    {
        if (counter < bindingFuncList.Count)
        {
            bindingFuncList[counter].Invoke(this, null);
            // print("绑定事件!");
            counter += 1;
        }
        else
        {
            throw new Exception("自动绑定事件次数过多，超出列表索引！");
        }

    }

    /// <summary>
    /// 播放移液器开始动画并将针头出现绑定在相应事件上      //移液器开始动画针头出现索引：3          
    /// </summary>
    void StartYiYeQiPathAnimation()
    {
        YiYeQi.nodeStartEvent.Add(3, () => QiangTou.SetActive(true));
        YiYeQi.nodeStartEvent[3]+=PlayForwardBanji;
        YiYeQi.PlayOncePathAnimation(ObjectTransformAnimationMode.P_R, AnimationStopMode.Keep, "Project/PathAnimation/Antibiotic/YiYeQiStartWork", new PathAnimationParamete(value: true));
    }
    /// <summary>
    /// 播放移液器下来注射药剂再上去动画
    /// </summary>
    void YiYeQiDownAndUpZhuSheAnimation()
    {
        YiYeQi.nodeStartEvent.Add(1, PlayForwardBanji);
        YiYeQi.PlayOncePathAnimation(ObjectTransformAnimationMode.P_R, AnimationStopMode.Keep, "Project/PathAnimation/Antibiotic/YiYeQiDownAndUpAnim");
    }
    /// <summary>
    /// 播放移液器下来吸取药剂再上去动画
    /// </summary>
    void YiYeQiDownAndUpXiQuAnimation()
    {
        YiYeQi.nodeStartEvent.Add(1, PlayBackBanji);
        YiYeQi.PlayOncePathAnimation(ObjectTransformAnimationMode.P_R, AnimationStopMode.Keep, "Project/PathAnimation/Antibiotic/YiYeQiDownAndUpAnim");
    }

    /// <summary>
    /// 播放移液器结束动画并将针头隐藏绑定在相应事件上       移液器结束动画针头隐藏索引：2
    /// </summary>
    void EndtYiYeQiPathAnimation()
    {
        YiYeQi.nodeStartEvent.Add(2, () => QiangTou.SetActive(false));
        YiYeQi.PlayOncePathAnimation(ObjectTransformAnimationMode.P_R, AnimationStopMode.Keep, "Project/PathAnimation/Antibiotic/YiYeQiEndWork");
    }
    /// <summary>
    /// 按下扳机动画   注射药剂时调用  在吸取溶液之前调用
    /// </summary>
    void PlayForwardBanji()
    {
        BanJi.PlayOncePathAnimation(ObjectTransformAnimationMode.P_R, AnimationStopMode.Keep, "Project/PathAnimation/Antibiotic/BanJiAnXia");
    }
    /// <summary>
    /// 抬起扳机动画  扳机弹回时稍作等待   注射药剂完毕，返回移液器架子上调用   吸取溶液时调用
    /// </summary>
    void PlayBackBanji()
    {
        BanJi.PlayOncePathAnimation(ObjectTransformAnimationMode.P_R, AnimationStopMode.Keep, "Project/PathAnimation/Antibiotic/BanJTanHui");
    }



    /// <summary>
    /// 过滤溶液滴液特效播放动画   
    /// </summary>
    void GuoLvRongYeDiYePlayFunc()
    {
        RongYeGuoLvDiYeParticle.Play();
    }


    void One_niko()
    {
        //给首页弹窗绑定按钮事件
        HomeWindowPanel.AddDisposableClickEvent(ReadFuncListToBinding);
        HomeWindowPanel.AddDisposableClickEvent(sceneLogicManager.ActiveHud);
        HomeWindowPanel.AddDisposableClickEvent(() => { UpdateStepFunc(t: "JianChaZhiShi"); });
        HomeWindowPanel.AddDisposableClickEvent(player.Life);
        HomeWindowPanel.AddDisposableClickEvent(checkPanel.ActiveGameObject);
    }


    void Two_niko()
    {
        //给检查按钮绑定事件
        checkPanel.AddDisposableClickEvent(ReadFuncListToBinding);
        checkPanel.AddDisposableClickEvent(ExcuteStepFunc);
        checkPanel.AddDisposableClickEvent(player.Dead);
        checkPanel.AddDisposableClickEvent(checkCon.Play);
    }

    void Three_niko()
    {
        //给检查结束绑定事件
        checkCon.AddDisposableEvent(ReadFuncListToBinding);
        checkCon.AddDisposableEvent(player.Life);
        checkCon.AddDisposableEvent(() => { UpdateStepFunc(t: "TuoZhiRuZhiShi", edgeHighlight: TuoZhiRuLight); });
        checkCon.AddDisposableEvent(() => { player.SetPlayerTransform("StartDoWorkZhiShi_player"); });
        checkCon.AddDisposableEvent(() => { WorkDeskDoor.DisActiveGameObject(); });
    }

    void Four_niko()
    {
        //给点击脱脂乳高亮绑定事件
        TuoZhiRuLight.AddDisposableclickObjectEvent(ReadFuncListToBinding);
        TuoZhiRuLight.AddDisposableclickObjectEvent(ExcuteStepFunc);
        TuoZhiRuLight.AddDisposableclickObjectEvent(() => TuoZhiRu.ThisTransform.SetParent(null));
        TuoZhiRuLight.AddDisposableclickObjectEvent(() => { TuoZhiRu.PlayOncePathAnimation(ObjectTransformAnimationMode.P_R, AnimationStopMode.Keep, "Project/PathAnimation/Antibiotic/TuoZhiRuPathAnim", new PathAnimationParamete(0, true)); });
    }

    void Five_niko()
    {
        //给脱脂乳到达指定地点动画结束绑定事件
        TuoZhiRu.AddDisposablepathAnimationEndEvent(ReadFuncListToBinding);
        TuoZhiRu.AddDisposablepathAnimationEndEvent(() => YiYeQi.ThisTransform.SetParent(null));
        TuoZhiRu.AddDisposablepathAnimationEndEvent(StartYiYeQiPathAnimation);
    }

    void Six_niko()
    {
        //给移液器离开原始架子，到达合适的指点地点动画结束绑定事件         
        YiYeQi.AddDisposablepathAnimationEndEvent(ReadFuncListToBinding);
        YiYeQi.AddDisposablepathAnimationEndEvent(() => { delayEventBuffer.StartTimer(); });
    }

    void Seven_niko()
    {
        delayEventBuffer.AdddelayInvokeEvent(ReadFuncListToBinding);
        delayEventBuffer.AdddelayInvokeEvent(YiYeQiDownAndUpXiQuAnimation);
    }

    void Eight_niko()
    {
        //给吸取牛奶扳机抬起后动画结束绑定事件
        YiYeQi.AddDisposablepathAnimationEndEvent(ReadFuncListToBinding);
        YiYeQi.AddDisposablepathAnimationEndEvent(() => { TuoZhiRu.InvertPlayOncePathAnimation(ObjectTransformAnimationMode.P_R, AnimationStopMode.Keep, "Project/PathAnimation/Antibiotic/TuoZhiRuPathAnim"); });
        YiYeQi.AddDisposablepathAnimationEndEvent(() => { LiXinGuanGaiZi1.SetActive(false); LiXinGuan1.ThisTransform.SetParent(null); LiXinGuan1.PlayOncePathAnimation(ObjectTransformAnimationMode.P_R, AnimationStopMode.Keep, "Project/PathAnimation/Melamine/LiXinGuan1YichuYuanLaiJiaZi", new PathAnimationParamete(0, true)); });
    }
    void Nine_niko()
    {
        //给离心管1号到达移液器下方动画绑定事件
        LiXinGuan1.AddDisposablepathAnimationEndEvent(ReadFuncListToBinding);
        LiXinGuan1.AddDisposablepathAnimationEndEvent(YiYeQiDownAndUpZhuSheAnimation);
        LiXinGuan1.AddDisposablepathAnimationEndEvent(() => { YiYeQi.nodeStartEvent[1] += (() => { Guan1_YeMian_1.ActiveGameObject(); Guan1_YeMian_1.Open(30); }); });
    }

    void NineHalf_niko()
    {
        //缓冲，避免移液器两个绑定事件接连在一起出现问题.
        YiYeQi.AddDisposablepathAnimationEndEvent(ReadFuncListToBinding);
        YiYeQi.AddDisposablepathAnimationEndEvent(() => delayEventBuffer.StartTimer());
    }

    void Ten_niko()
    {
        //给扳机按下注射牛奶到试管动画绑定事件
        delayEventBuffer.AdddelayInvokeEvent(ReadFuncListToBinding);
        delayEventBuffer.AdddelayInvokeEvent(EndtYiYeQiPathAnimation);
        delayEventBuffer.AdddelayInvokeEvent(() => YiYeQi.nodeStartEvent.Add(1, PlayBackBanji));
    }

    void Eleven_niko()
    {
        //给移液器返回移液器架子上动画结束绑定事件
        YiYeQi.AddDisposablepathAnimationEndEvent(ReadFuncListToBinding);
        YiYeQi.AddDisposablepathAnimationEndEvent(() => LiXinGuan1.InvertPlayOncePathAnimation(ObjectTransformAnimationMode.P_R, AnimationStopMode.Keep, "Project/PathAnimation/Melamine/LiXinGuan1YichuYuanLaiJiaZi"));
    }

    void Twelve_niko()
    {
        //给离心管1号返回原始架子动画结束绑定事件
        LiXinGuan1.AddDisposablepathAnimationEndEvent(ReadFuncListToBinding);
        LiXinGuan1.AddDisposablepathAnimationEndEvent(() => { UpdateStepFunc(t: "ShaoBeiSanLvYiSuanLightZhiShi", edgeHighlight: ShaoBei_SanLvYiSuanLight); });
    }
    //-----------------------------------------------------------------------------------------
    void Thirteen_niko()
    {
        //给点击烧杯装三氯乙酸溶液绑定事件
        ShaoBei_SanLvYiSuanLight.AddDisposableclickObjectEvent(ReadFuncListToBinding);
        ShaoBei_SanLvYiSuanLight.AddDisposableclickObjectEvent(ExcuteStepFunc);
        ShaoBei_SanLvYiSuanLight.AddDisposableclickObjectEvent(() => ShaoBei_SanLvYiSuanRongYe.ThisTransform.SetParent(null));
        ShaoBei_SanLvYiSuanLight.AddDisposableclickObjectEvent(() => { ShaoBei_SanLvYiSuanRongYe.PlayOncePathAnimation(ObjectTransformAnimationMode.P_R, AnimationStopMode.Keep, "Project/PathAnimation/Melamine/ShaBei_SanLvYiSuan_PathAnim", new PathAnimationParamete(0, true)); });
    }

    void Fourteen_niko()
    {
        //给烧杯装三氯乙酸溶液到达指定地点动画结束绑定事件
        ShaoBei_SanLvYiSuanRongYe.AddDisposablepathAnimationEndEvent(ReadFuncListToBinding);
        ShaoBei_SanLvYiSuanRongYe.AddDisposablepathAnimationEndEvent(StartYiYeQiPathAnimation);
    }

    void Fifteen_niko()
    {
        //给移液器离开原始架子，到达合适的指点地点动画结束绑定事件         
        YiYeQi.AddDisposablepathAnimationEndEvent(ReadFuncListToBinding);
        YiYeQi.AddDisposablepathAnimationEndEvent(() => { delayEventBuffer.StartTimer(); });
    }

    void FifteenHalf_niko()
    {
        delayEventBuffer.AdddelayInvokeEvent(ReadFuncListToBinding);
        delayEventBuffer.AdddelayInvokeEvent(YiYeQiDownAndUpXiQuAnimation);
    }

    void Sixteen_niko()
    {
        //给吸取三氯乙酸溶液扳机抬起后动画结束绑定事件
        YiYeQi.AddDisposablepathAnimationEndEvent(ReadFuncListToBinding);
        YiYeQi.AddDisposablepathAnimationEndEvent(() => { ShaoBei_SanLvYiSuanRongYe.InvertPlayOncePathAnimation(ObjectTransformAnimationMode.P_R, AnimationStopMode.Keep, "Project/PathAnimation/Melamine/ShaBei_SanLvYiSuan_PathAnim"); });
        YiYeQi.AddDisposablepathAnimationEndEvent(() => {LiXinGuan1.PlayOncePathAnimation(ObjectTransformAnimationMode.P_R, AnimationStopMode.Keep, "Project/PathAnimation/Melamine/LiXinGuan1YichuYuanLaiJiaZi", new PathAnimationParamete(0, true)); });
    }
    void Seventeen_niko()
    {
        //给离心管1号到达移液器下方动画绑定事件
        LiXinGuan1.AddDisposablepathAnimationEndEvent(ReadFuncListToBinding);
        LiXinGuan1.AddDisposablepathAnimationEndEvent(YiYeQiDownAndUpZhuSheAnimation);
        Guan1_YeMian_1.AdddelayInvokeEvent(() => { Guan1_YeMian_2.ActiveGameObject(); Guan1_YeMian_2.Open(23, 0.6f); });
        LiXinGuan1.AddDisposablepathAnimationEndEvent(() => { YiYeQi.nodeStartEvent[1] += (() => { Guan1_YeMian_1.ActiveGameObject(); Guan1_YeMian_1.Open(100,0.4f); }); });
    }

    void SeventeenHalf_niko()
    {
        //缓冲，避免移液器两个绑定事件接连在一起出现问题.
        YiYeQi.AddDisposablepathAnimationEndEvent(ReadFuncListToBinding);
        YiYeQi.AddDisposablepathAnimationEndEvent(() => delayEventBuffer.StartTimer());
    }

    void Eighteen_niko()
    {
        //给扳机按下注射三氯乙酸溶液到试管动画绑定事件
        delayEventBuffer.AdddelayInvokeEvent(ReadFuncListToBinding);
        delayEventBuffer.AdddelayInvokeEvent(EndtYiYeQiPathAnimation);
        delayEventBuffer.AdddelayInvokeEvent(() => YiYeQi.nodeStartEvent.Add(1, PlayBackBanji));
    }

    void Nineteen_niko()
    {
        //给移液器返回移液器架子上动画结束绑定事件
        YiYeQi.AddDisposablepathAnimationEndEvent(ReadFuncListToBinding);
        YiYeQi.AddDisposablepathAnimationEndEvent(() => LiXinGuan1.InvertPlayOncePathAnimation(ObjectTransformAnimationMode.P_R, AnimationStopMode.Keep, "Project/PathAnimation/Melamine/LiXinGuan1YichuYuanLaiJiaZi"));
    }

    void Twenty_niko()
    {
        //给离心管1号返回原始架子动画结束绑定事件
        LiXinGuan1.AddDisposablepathAnimationEndEvent(ReadFuncListToBinding);
        LiXinGuan1.AddDisposablepathAnimationEndEvent(() => { UpdateStepFunc(t: "YiJingLightZhiShi", edgeHighlight: ShaoBei_YiJingLight); });
    }
    //-------------------------------------------
    void TwentyOne_niko()
    {
        //给点击烧杯装乙腈溶液绑定事件
        ShaoBei_YiJingLight.AddDisposableclickObjectEvent(ReadFuncListToBinding);
        ShaoBei_YiJingLight.AddDisposableclickObjectEvent(ExcuteStepFunc);
        ShaoBei_YiJingLight.AddDisposableclickObjectEvent(() => ShaoBei_YiJing.ThisTransform.SetParent(null));
        ShaoBei_YiJingLight.AddDisposableclickObjectEvent(() => { ShaoBei_YiJing.PlayOncePathAnimation(ObjectTransformAnimationMode.P_R, AnimationStopMode.Keep, "Project/PathAnimation/Melamine/ShaBeiYiJingToYiYeQi", new PathAnimationParamete(0, true)); });
    }

    void TwentyTwo_niko()
    {
        //给烧杯装乙腈溶液到达指定地点动画结束绑定事件
        ShaoBei_YiJing.AddDisposablepathAnimationEndEvent(ReadFuncListToBinding);
        ShaoBei_YiJing.AddDisposablepathAnimationEndEvent(StartYiYeQiPathAnimation);
    }

    void TwentyTwoHalf_niko()
    {
        //给移液器离开原始架子，到达合适的指点地点动画结束绑定事件         
        YiYeQi.AddDisposablepathAnimationEndEvent(ReadFuncListToBinding);
        YiYeQi.AddDisposablepathAnimationEndEvent(() => { delayEventBuffer.StartTimer(); });
    }

    void TwentyThree_niko()
    {
        delayEventBuffer.AdddelayInvokeEvent(ReadFuncListToBinding);
        delayEventBuffer.AdddelayInvokeEvent(YiYeQiDownAndUpXiQuAnimation);
    }

    void TwentyFour_niko()
    {
        //给吸取乙腈溶液扳机抬起后动画结束绑定事件
        YiYeQi.AddDisposablepathAnimationEndEvent(ReadFuncListToBinding);
        YiYeQi.AddDisposablepathAnimationEndEvent(() => { ShaoBei_YiJing.InvertPlayOncePathAnimation(ObjectTransformAnimationMode.P_R, AnimationStopMode.Keep, "Project/PathAnimation/Melamine/ShaBeiYiJingToYiYeQi"); });
        YiYeQi.AddDisposablepathAnimationEndEvent(() => { LiXinGuan1.PlayOncePathAnimation(ObjectTransformAnimationMode.P_R, AnimationStopMode.Keep, "Project/PathAnimation/Melamine/LiXinGuan1YichuYuanLaiJiaZi", new PathAnimationParamete(0, true)); });
    }
    void TwentyFive_niko()
    {
        //给离心管1号到达移液器下方动画绑定事件
        LiXinGuan1.AddDisposablepathAnimationEndEvent(ReadFuncListToBinding);
        LiXinGuan1.AddDisposablepathAnimationEndEvent(YiYeQiDownAndUpZhuSheAnimation);
        LiXinGuan1.AddDisposablepathAnimationEndEvent(() => { YiYeQi.nodeStartEvent[1] += (() => {Guan1_YeMian_2.Open(35); }); });
    }

    void TwentyFiveHalf_niko()
    {
        //缓冲，避免移液器两个绑定事件接连在一起出现问题.
        YiYeQi.AddDisposablepathAnimationEndEvent(ReadFuncListToBinding);
        YiYeQi.AddDisposablepathAnimationEndEvent(() => delayEventBuffer.StartTimer());
    }

    void TwentySix_niko()
    {
        //给扳机按下注射乙腈溶液到试管动画绑定事件
        delayEventBuffer.AdddelayInvokeEvent(ReadFuncListToBinding);
        delayEventBuffer.AdddelayInvokeEvent(EndtYiYeQiPathAnimation);
        delayEventBuffer.AdddelayInvokeEvent(() => YiYeQi.nodeStartEvent.Add(1, PlayBackBanji));
    }

    void TwentySeven_niko()
    {
        //给移液器返回移液器架子上动画结束绑定事件
        YiYeQi.AddDisposablepathAnimationEndEvent(ReadFuncListToBinding);
        YiYeQi.AddDisposablepathAnimationEndEvent(() => { LiXinGuanGaiZi1.SetActive(true); LiXinGuan1.ThisTransform.SetParent(player.PlayerEyes); LiXinGuan1.ThisTransform.localPosition = new Vector3(-0.03f, -0.0826f, 0.3765f); player.Dead(); player.PlayPathAnimationFromStart("Project/PathAnimation/Melamine/PlayerCloseChaoShengYi"); });
    }


    void TwentyEight_niko()
    {
        //玩家旋转到超声波提取器面前动画结束绑定事件
        player.AddDisposablepathAnimationEndEvent(ReadFuncListToBinding);
        player.AddDisposablepathAnimationEndEvent(() => { LiXinGuan1.ThisTransform.SetParent(null); UpdateStepFunc(t: "ChaoShengYiZhiShi", edgeHighlight: ChaoShengYiLight); });
    }

    void TwentyEightHalf_niko()
    {
        //给点击超声仪绑定事件
        ChaoShengYiLight.AddDisposableclickObjectEvent(ReadFuncListToBinding);
        ChaoShengYiLight.AddDisposableclickObjectEvent(ExcuteStepFunc);
        ChaoShengYiLight.AddDisposableclickObjectEvent(() => { chaoShengYiGaiZi.PlayOncePathAnimation(ObjectTransformAnimationMode.R, AnimationStopMode.Keep, "Project/PathAnimation/Melamine/ChaoShengYiDoorAnim", new PathAnimationParamete(0, true)); });

    }

    void TwentyNine_niko()
    {
        //超声波提取器盖子打开动画结束绑定事件
        chaoShengYiGaiZi.AddDisposablepathAnimationEndEvent(ReadFuncListToBinding);
        chaoShengYiGaiZi.AddDisposablepathAnimationEndEvent(() => { LiXinGuan1.PlayOncePathAnimation(ObjectTransformAnimationMode.P_R, AnimationStopMode.Keep, "Project/PathAnimation/Melamine/LiXinGuan1InChaoShengYi"); });
    }

    void Thirty_niko()
    {
        //给离心管1号进入到超声器动画结束绑定事件
        LiXinGuan1.AddDisposablepathAnimationEndEvent(ReadFuncListToBinding);
        LiXinGuan1.AddDisposablepathAnimationEndEvent(() => { chaoShengYiGaiZi.InvertPlayOncePathAnimation(ObjectTransformAnimationMode.R, AnimationStopMode.Keep, "Project/PathAnimation/Melamine/ChaoShengYiDoorAnim", new PathAnimationParamete(0, true));});
    }

    void ThirtyOne_niko()
    {
        //给超声仪盖子关闭上动画结束绑定事件
        chaoShengYiGaiZi.AddDisposablepathAnimationEndEvent(ReadFuncListToBinding);
        chaoShengYiGaiZi.AddDisposablepathAnimationEndEvent(() => { ChaoShengTiQuTimer.StartTimer(); });
    }

    void ThirtyTwo_niko()
    {
        //给超声提取动画结束绑定事件
        ChaoShengTiQuTimer.AddDisposableimageTimerEndEvent(ReadFuncListToBinding);
        ChaoShengTiQuTimer.AddDisposableimageTimerEndEvent(()=>ZhenDangTiQuTimer.StartTimer());
    }

    void ThirtyThree_niko()
    {
        //给震荡提取动画结束绑定事件    
        ZhenDangTiQuTimer.AddDisposableimageTimerEndEvent(ReadFuncListToBinding);
        ZhenDangTiQuTimer.AddDisposableimageTimerEndEvent(() => { player.PlayPathAnimationFromStart("Project/PathAnimation/Melamine/PlayerFromCSToLX"); });
    }

    void ThirtyThreeHalf1_niko()
    {
        //给玩家移动视角到高速离心机动画结束绑定事件
        player.AddDisposablepathAnimationEndEvent(ReadFuncListToBinding);
        player.AddDisposablepathAnimationEndEvent(() => { UpdateStepFunc(t: "GaoSuLiXinZhiShi", edgeHighlight: GaoSuLiXinLight); });
    }

    void ThirtyThreeHalf2_niko()
    {
        //给点击高速离心机绑定事件    打开离心机盖子   
        GaoSuLiXinLight.AddDisposableclickObjectEvent(ReadFuncListToBinding);
        GaoSuLiXinLight.AddDisposableclickObjectEvent(ExcuteStepFunc);
        GaoSuLiXinLight.AddDisposableclickObjectEvent(() => {  LiXinJiGaiZi.PlayOncePathAnimation(ObjectTransformAnimationMode.R, AnimationStopMode.Keep, "Project/PathAnimation/Melamine/LiXinJiGaiZiAnim",new PathAnimationParamete(0,true)); });
    }

    void ThirtyFour_niko()
    {
        //给离心机盖子打开动画结束绑定事件
        LiXinJiGaiZi.AddDisposablepathAnimationEndEvent(ReadFuncListToBinding);
        LiXinJiGaiZi.AddDisposablepathAnimationEndEvent(() => { LiXinJiJiXin.PlayOncePathAnimation(ObjectTransformAnimationMode.P_R, AnimationStopMode.Keep, "Project/PathAnimation/Melamine/JiXinAnim"); });
    }

    void ThirtyFive_niko()
    {
        //给机芯动画结束绑定事件
        LiXinJiJiXin.AddDisposablepathAnimationEndEvent(ReadFuncListToBinding);
        LiXinJiJiXin.AddDisposablepathAnimationEndEvent(()=> { chaoShengYiGaiZi.PlayOncePathAnimation(ObjectTransformAnimationMode.R, AnimationStopMode.Keep, "Project/PathAnimation/Melamine/ChaoShengYiDoorAnim"); }); 
    }

    void ThirtySix_niko()
    {
        //超声波提取器盖子打开动画结束绑定事件
        chaoShengYiGaiZi.AddDisposablepathAnimationEndEvent(ReadFuncListToBinding);
        chaoShengYiGaiZi.AddDisposablepathAnimationEndEvent(() => { LiXinGuan1.PlayOncePathAnimation(ObjectTransformAnimationMode.P_R, AnimationStopMode.Keep, "Project/PathAnimation/Melamine/LiXinGuan1FromCSToLX"); });
    }

    void ThirtySeven_niko()
    {
        //给离心管1号离开超声器插入离心机动画结束绑定事件    关闭超声仪盖子，关闭机芯
        LiXinGuan1.AddDisposablepathAnimationEndEvent(ReadFuncListToBinding);
        LiXinGuan1.AddDisposablepathAnimationEndEvent(() => { chaoShengYiGaiZi.InvertPlayOncePathAnimation(ObjectTransformAnimationMode.R, AnimationStopMode.Keep, "Project/PathAnimation/Melamine/ChaoShengYiDoorAnim"); });
        LiXinGuan1.AddDisposablepathAnimationEndEvent(() => { LiXinJiJiXin.InvertPlayOncePathAnimation(ObjectTransformAnimationMode.P_R, AnimationStopMode.Keep, "Project/PathAnimation/Melamine/JiXinAnim"); });
    }

    void ThirtyEight_niko()
    {
        //给机芯落下动画结束绑定事件
        LiXinJiJiXin.AddDisposablepathAnimationEndEvent(ReadFuncListToBinding);
        LiXinJiJiXin.AddDisposablepathAnimationEndEvent(() => { LiXinJiGaiZi.InvertPlayOncePathAnimation(ObjectTransformAnimationMode.R, AnimationStopMode.Keep, "Project/PathAnimation/Melamine/LiXinJiGaiZiAnim"); });
    }


    void ThirtyNine_niko()
    {
        //给离心机盖子关闭动画结束绑定事件
        LiXinJiGaiZi.AddDisposablepathAnimationEndEvent(ReadFuncListToBinding);
        LiXinJiGaiZi.AddDisposablepathAnimationEndEvent(()=>LiXinJiTimer.StartTimer());
    }

    void Forty_niko()
    {
        //给离心机计时器结束动画绑定事件
        LiXinJiTimer.AddDisposableimageTimerEndEvent(ReadFuncListToBinding);
        LiXinJiTimer.AddDisposableimageTimerEndEvent(() => { LiXinJiGaiZi.PlayOncePathAnimation(ObjectTransformAnimationMode.R, AnimationStopMode.Keep, "Project/PathAnimation/Melamine/LiXinJiGaiZiAnim"); });
    }

    void FortyOne_niko()
    {
        //给离心机盖子打开动画结束绑定事件
        LiXinJiGaiZi.AddDisposablepathAnimationEndEvent(ReadFuncListToBinding);
        LiXinJiGaiZi.AddDisposablepathAnimationEndEvent(() => { LiXinJiJiXin.PlayOncePathAnimation(ObjectTransformAnimationMode.P_R, AnimationStopMode.Keep, "Project/PathAnimation/Melamine/JiXinAnim"); });
    }

    void FortyTwo_niko()
    {
        //给机芯动画结束绑定事件     将离心管1号移动至玩家眼前来
        LiXinJiJiXin.AddDisposablepathAnimationEndEvent(ReadFuncListToBinding);
        LiXinJiJiXin.AddDisposablepathAnimationEndEvent(() => { LiXinGuan1.PlayOncePathAnimation(ObjectTransformAnimationMode.P_R, AnimationStopMode.Keep, "Project/PathAnimation/Melamine/LiXinGuan1FromLXJTOP"); }); 
    }

    void FortyThree_niko()
    {
        //给离心管1号移动至玩家眼前动画结束     将离心管1号设置为玩家的子物体、播放机芯下降动画
        LiXinGuan1.AddDisposablepathAnimationEndEvent(ReadFuncListToBinding);
        LiXinGuan1.AddDisposablepathAnimationEndEvent(()=> 
        {
            LiXinGuan1.ThisTransform.SetParent(player.PlayerEyes);
            LiXinJiJiXin.InvertPlayOncePathAnimation(ObjectTransformAnimationMode.P_R, AnimationStopMode.Keep, "Project/PathAnimation/Melamine/JiXinAnim");
        });

    }

    void FortyFour_niko()
    {
        //给机芯落下动画结束绑定事件
        LiXinJiJiXin.AddDisposablepathAnimationEndEvent(ReadFuncListToBinding);
        LiXinJiJiXin.AddDisposablepathAnimationEndEvent(() => { LiXinJiGaiZi.InvertPlayOncePathAnimation(ObjectTransformAnimationMode.R, AnimationStopMode.Keep, "Project/PathAnimation/Melamine/LiXinJiGaiZiAnim"); });
    }

    void FortyFive_niko()
    {
        //给离心机盖子关闭动画结束绑定事件      
        LiXinJiGaiZi.AddDisposablepathAnimationEndEvent(ReadFuncListToBinding);
        LiXinJiGaiZi.AddDisposablepathAnimationEndEvent(() => 
        {
            player.PlayPathAnimationFromStart("Project/PathAnimation/Melamine/PlayerFromLXToTable");
        });
    }

    void FortySix_niko()
    {
        //给玩家视角回到实验台动画结束绑定事件
        player.AddDisposablepathAnimationEndEvent(ReadFuncListToBinding);
        player.AddDisposablepathAnimationEndEvent(() => { LiXinGuan1.ThisTransform.SetParent(null); LiXinGuan1.PlayOncePathAnimation(ObjectTransformAnimationMode.P_R, AnimationStopMode.Keep, "Project/PathAnimation/Melamine/LiXinGuan1daoNaiYuBeiAnim"); });
    }

    void FortySeven_niko()
    {
        //给离心管1从玩家面前位移到倒牛奶动画的预备位置动画结束绑定事件
        LiXinGuan1.AddDisposablepathAnimationEndEvent(ReadFuncListToBinding);
        LiXinGuan1.AddDisposablepathAnimationEndEvent(() => { UpdateStepFunc(t: "BoLiXianWeiLvZhiZhiShi", edgeHighlight: BoLiXianWeiLvZhiLight); player.Life(); });
    }

    void FortyEight_niko()
    {
        //给点击玻璃纤维滤纸绑定事件
        BoLiXianWeiLvZhiLight.AddDisposableclickObjectEvent(ReadFuncListToBinding);
        BoLiXianWeiLvZhiLight.AddDisposableclickObjectEvent(ExcuteStepFunc);
        BoLiXianWeiLvZhiLight.AddDisposableclickObjectEvent(() => { BoLiXianWeiLvZhi.transform.SetParent(null); BoLiXianWeiLvZhi.PlayOncePathAnimation(ObjectTransformAnimationMode.P_R, AnimationStopMode.Keep, "Project/PathAnimation/Melamine/LvZhiBianLouDou", new PathAnimationParamete(0, true)); });
    }
    void FortyNine_niko()
    {
        //给玻璃纤维滤纸到达合适地点变成漏斗动画结束绑定事件
        BoLiXianWeiLvZhi.AddDisposablepathAnimationEndEvent(ReadFuncListToBinding);
        BoLiXianWeiLvZhi.AddDisposablepathAnimationEndEvent(() => { BoLiXianWeiLouDou.ActiveGameObject(); BoLiXianWeiLvZhi.DisActiveGameObject(); });
        BoLiXianWeiLvZhi.AddDisposablepathAnimationEndEvent(() => { LiXinGuanGaiZi2.SetActive(false); LiXinGuan2.transform.SetParent(null); LiXinGuan2.PlayOncePathAnimation(ObjectTransformAnimationMode.P_R, AnimationStopMode.Keep, "Project/PathAnimation/Melamine/LiXinGuan2YiChuJiaZi", new PathAnimationParamete(0, true)); });
    }

    void Fifty_niko()
    {
        //给离心管2到达合适地点动画结束绑定事件
        LiXinGuan2.AddDisposablepathAnimationEndEvent(ReadFuncListToBinding);
        LiXinGuan2.AddDisposablepathAnimationEndEvent(() => { LiXinGuanGaiZi1.SetActive(false); LiXinGuan1.PlayOncePathAnimation(ObjectTransformAnimationMode.P_R, AnimationStopMode.Keep, "Project/PathAnimation/Melamine/LiXinGuan1DaoShui"); });
    }

    void FiftyOne_niko()
    {
        //给离心管1倒水动画结束后帮定事件
        LiXinGuan1.AddDisposablepathAnimationEndEvent(ReadFuncListToBinding);
        LiXinGuan1.AddDisposablepathAnimationEndEvent(() => { Guan1_YeMian_1.DisActiveGameObject(); Guan1_YeMian_2.DisActiveGameObject(); milkWater.SetActive(true); });
        LiXinGuan1.AddDisposablepathAnimationEndEvent(() => { delayEventBuffer.StartTimer(4.0f); });
    }

    void FiftyTwo_niko()
    {
        //给计时器动画结束绑定事件    //也就是给倒牛奶的流水特效运行四秒后结束绑定事件
        delayEventBuffer.AdddelayInvokeEvent(ReadFuncListToBinding);
        delayEventBuffer.AdddelayInvokeEvent(() => { milkWater.SetActive(false); LiXinGuan1.PlayOncePathAnimation(ObjectTransformAnimationMode.P_R, AnimationStopMode.Keep, "Project/PathAnimation/Melamine/LiXinGuan1TuiHuiJiaZi"); });
    }

    void FiftyThree_niko()
    {
        //离心管1退回到原来架子的角落动画结束事件
        LiXinGuan1.AddDisposablepathAnimationEndEvent(ReadFuncListToBinding);
        LiXinGuan1.AddDisposablepathAnimationEndEvent(() => { LiXinGuanGaiZi1.SetActive(true); BoLiXianWeiLouDou.transform.SetParent(LiXinGuan2.transform); LiXinGuan2.InvertPlayOncePathAnimation(ObjectTransformAnimationMode.P_R, AnimationStopMode.Keep, "Project/PathAnimation/Melamine/LiXinGuan2YiChuJiaZi"); });
    }

    void FiftyFour_niko()
    {
        //给离心管2退回到原来位置动画结束绑定事件  //对应的是溶液过滤滴落动画
        LiXinGuan2.AddDisposablepathAnimationEndEvent(ReadFuncListToBinding);
        LiXinGuan2.AddDisposablepathAnimationEndEvent(() => RongYeGuoLvTimer.StartTimer());
        LiXinGuan2.AddDisposablepathAnimationEndEvent(() => { Guan2_YeMian_1.ActiveGameObject(); Guan2_YeMian_1.Open(100, 5); });
        LiXinGuan2.AddDisposablepathAnimationEndEvent(() => { RongYeGuoLvDiYeParticle.gameObject.SetActive(true); InvokeRepeating("GuoLvRongYeDiYePlayFunc", 0, 1); });
    }

    void FiftyFive_niko()
    {
        //给溶液过滤低落动画结束绑定事件      
        RongYeGuoLvTimer.AddDisposableimageTimerEndEvent(ReadFuncListToBinding);
        RongYeGuoLvTimer.AddDisposableimageTimerEndEvent(() => { CancelInvoke("GuoLvRongYeDiYePlayFunc"); RongYeGuoLvDiYeParticle.gameObject.SetActive(false); });
        RongYeGuoLvTimer.AddDisposableimageTimerEndEvent(()=> UpdateStepFunc(t: "ShaoBeiSanLvYiSuanLightZhiShi", edgeHighlight: ShaoBei_SanLvYiSuanLight));
    }


    void FiftySix_niko()
    {
        //给点击三氯乙酸溶液绑定事件，对应的是将上清液定容至25ml。
        ShaoBei_SanLvYiSuanLight.AddDisposableclickObjectEvent(ReadFuncListToBinding);
        ShaoBei_SanLvYiSuanLight.AddDisposableclickObjectEvent(ExcuteStepFunc);
        ShaoBei_SanLvYiSuanLight.AddDisposableclickObjectEvent(() => { ShaoBei_SanLvYiSuanRongYe.PlayOncePathAnimation(ObjectTransformAnimationMode.P_R, AnimationStopMode.Keep, "Project/PathAnimation/Melamine/ShaBei_SanLvYiSuan_PathAnim", new PathAnimationParamete(0, true)); });
    }


    void FiftySeven_niko()
    {
        //给烧杯装三氯乙酸溶液到达指定地点动画结束绑定事件
        ShaoBei_SanLvYiSuanRongYe.AddDisposablepathAnimationEndEvent(ReadFuncListToBinding);
        ShaoBei_SanLvYiSuanRongYe.AddDisposablepathAnimationEndEvent(StartYiYeQiPathAnimation);
    }

    void FiftyEight_niko()
    {
        //给移液器离开原始架子，到达合适的指点地点动画结束绑定事件         
        YiYeQi.AddDisposablepathAnimationEndEvent(ReadFuncListToBinding);
        YiYeQi.AddDisposablepathAnimationEndEvent(() => { delayEventBuffer.StartTimer(); });
    }

    void FiftyEightHalf_niko()
    {
        delayEventBuffer.AdddelayInvokeEvent(ReadFuncListToBinding);
        delayEventBuffer.AdddelayInvokeEvent(YiYeQiDownAndUpXiQuAnimation);
    }

    void FiftyNine_niko()
    {
        //给吸取三氯乙酸溶液扳机抬起后动画结束绑定事件
        YiYeQi.AddDisposablepathAnimationEndEvent(ReadFuncListToBinding);
        YiYeQi.AddDisposablepathAnimationEndEvent(() => { ShaoBei_SanLvYiSuanRongYe.InvertPlayOncePathAnimation(ObjectTransformAnimationMode.P_R, AnimationStopMode.Keep, "Project/PathAnimation/Melamine/ShaBei_SanLvYiSuan_PathAnim"); });
        YiYeQi.AddDisposablepathAnimationEndEvent(() => 
        {
            BoLiXianWeiLouDou.transform.SetParent(null);
            BoLiXianWeiLouDou.gameObject.SetActive(false);
            LiXinGuan2.PlayOncePathAnimation(ObjectTransformAnimationMode.P_R, AnimationStopMode.Keep, "Project/PathAnimation/Melamine/LiXinGuan2YiChuJiaZi", new PathAnimationParamete(0, true));
        });
    }
    void Sixty_niko()
    {
        //给离心管2号到达移液器下方动画绑定事件
        LiXinGuan2.AddDisposablepathAnimationEndEvent(ReadFuncListToBinding);
        LiXinGuan2.AddDisposablepathAnimationEndEvent(YiYeQiDownAndUpZhuSheAnimation);
        LiXinGuan2.AddDisposablepathAnimationEndEvent(() => { YiYeQi.nodeStartEvent[1] += (() => { Guan2_YeMian_2.ActiveGameObject(); Guan2_YeMian_2.Open(45); }); });
    }

    void SixtyHalf_niko()
    {
        //缓冲，避免移液器两个绑定事件接连在一起出现问题.
        YiYeQi.AddDisposablepathAnimationEndEvent(ReadFuncListToBinding);
        YiYeQi.AddDisposablepathAnimationEndEvent(() => delayEventBuffer.StartTimer());
    }

    void SixtyOne_niko()
    {
        //给扳机按下注射三氯乙酸溶液到离心管2号动画绑定事件
        delayEventBuffer.AdddelayInvokeEvent(ReadFuncListToBinding);
        delayEventBuffer.AdddelayInvokeEvent(EndtYiYeQiPathAnimation);
        delayEventBuffer.AdddelayInvokeEvent(() => YiYeQi.nodeStartEvent.Add(1, PlayBackBanji));
    }

    void SixtyTwo_niko()
    {
        //给移液器返回移液器架子上动画结束绑定事件    对应的是激活试管架的点击功能
        YiYeQi.AddDisposablepathAnimationEndEvent(ReadFuncListToBinding);
        YiYeQi.AddDisposablepathAnimationEndEvent(() => { UpdateStepFunc(t: "ShiGuanJiaZhiShi", edgeHighlight: ShiGuanJia); });
    }

    void SixtyThree_niko()
    {
        //给点击试管架绑定事件    对应的是离心管3移动出来，离心管2给离心管3中倒试剂  LiXinGuan3YiChuJiaZi
        ShiGuanJia.AddDisposableclickObjectEvent(ReadFuncListToBinding);
        ShiGuanJia.AddDisposableclickObjectEvent(ExcuteStepFunc);
        ShiGuanJia.AddDisposableclickObjectEvent(() =>
        {
            LiXinGuanGaiZi3.SetActive(false);
            LiXinGuan3.ThisTransform.SetParent(null);
            LiXinGuan3.PlayOncePathAnimation(ObjectTransformAnimationMode.P_R, AnimationStopMode.Keep, "Project/PathAnimation/Melamine/LiXinGuan3YiChuJiaZi", new PathAnimationParamete(0, true));
        });

    }

    void SixtyFour_niko()
    {
        //给离心管3号到达合适位置绑定事件
        LiXinGuan3.AddDisposablepathAnimationEndEvent(ReadFuncListToBinding);
        LiXinGuan3.AddDisposablepathAnimationEndEvent(() => 
        {
            //在这里给无菌吸管动画绑定对应的液面上升动画和下降动画
            WuJunXiGuan.nodeStartEvent.Add(3,()=> { Guan2_YeMian_2.Open(30); });
            WuJunXiGuan.nodeStartEvent.Add(7, () => { Guan3_YeMian_1.ActiveGameObject(); Guan3_YeMian_1.Open(100); });
            WuJunXiGuan.PlayOncePathAnimation(ObjectTransformAnimationMode.P_R, AnimationStopMode.Keep, "Project/PathAnimation/Melamine/WuJunXiGuanPathAnim", new PathAnimationParamete(0, true));
        });
    }


    void SixtyFive_niko()
    {
        //给无菌吸管移取滤液完毕动画绑定事件        对应的将离心管2退回到无菌试管架上去
        WuJunXiGuan.AddDisposablepathAnimationEndEvent(ReadFuncListToBinding);
        WuJunXiGuan.AddDisposablepathAnimationEndEvent(()=> 
        {
            LiXinGuanGaiZi2.SetActive(true);
            LiXinGuan2.PlayOncePathAnimation(ObjectTransformAnimationMode.P_R, AnimationStopMode.Keep, "Project/PathAnimation/Melamine/LiXinGuan2TuiHuiJiaZi");
        });

    }

    void SixtySix_niko()
    {
        //给离心管2退回到无菌试管架上动画结束绑定事件    对应的是纯净水动画
        LiXinGuan2.AddDisposablepathAnimationEndEvent(ReadFuncListToBinding);
        LiXinGuan2.AddDisposablepathAnimationEndEvent(()=> 
        {
            Water.PlayOncePathAnimation(ObjectTransformAnimationMode.P_R, AnimationStopMode.Keep, "Project/PathAnimation/Melamine/ShaBei_Water_PathAnim");
        });

    }

    void SixtySeven_niko()
    {
        //给纯水水杯移动到指定地点动画结束绑定事件
        Water.AddDisposablepathAnimationEndEvent(ReadFuncListToBinding);
        Water.AddDisposablepathAnimationEndEvent(StartYiYeQiPathAnimation);
    }


    void SixtyEight_niko()
    {
        //给移液器离开原始架子，到达合适的指点地点动画结束绑定事件         
        YiYeQi.AddDisposablepathAnimationEndEvent(ReadFuncListToBinding);
        YiYeQi.AddDisposablepathAnimationEndEvent(() => { delayEventBuffer.StartTimer(); });
    }

    void SixtyEightHalf_niko()
    {
        delayEventBuffer.AdddelayInvokeEvent(ReadFuncListToBinding);
        delayEventBuffer.AdddelayInvokeEvent(YiYeQiDownAndUpXiQuAnimation);
    }

    void SixtyNine_niko()
    {
        //给吸取纯水扳机抬起后动画结束绑定事件
        YiYeQi.AddDisposablepathAnimationEndEvent(ReadFuncListToBinding);
        YiYeQi.AddDisposablepathAnimationEndEvent(() => { Water.InvertPlayOncePathAnimation(ObjectTransformAnimationMode.P_R, AnimationStopMode.Keep, "Project/PathAnimation/Melamine/ShaBei_Water_PathAnim"); });
    }
    void Seventy_niko()
    {
        //给烧杯装纯回到原来位置动画绑定事件
        Water.AddDisposablepathAnimationEndEvent(ReadFuncListToBinding);
        Water.AddDisposablepathAnimationEndEvent(() => { LiXinGuan3.PlayOncePathAnimation(ObjectTransformAnimationMode.P_R, AnimationStopMode.Keep, "Project/PathAnimation/Melamine/LiXinGuan3ToYiYeQi"); });
    }

    void SeventyOne_niko()
    {
        //给离心管3移动到移液器下方绑定事件
        LiXinGuan3.AddDisposablepathAnimationEndEvent(ReadFuncListToBinding);
        LiXinGuan3.AddDisposablepathAnimationEndEvent(YiYeQiDownAndUpZhuSheAnimation);
        LiXinGuan3.AddDisposablepathAnimationEndEvent(() => { YiYeQi.nodeStartEvent[1] += (() => { Guan3_YeMian_2.ActiveGameObject(); Guan3_YeMian_2.Open(20); }); });
    }

    void SeventyTwo_niko()
    {
        //缓冲，避免移液器两个绑定事件接连在一起出现问题.
        YiYeQi.AddDisposablepathAnimationEndEvent(ReadFuncListToBinding);
        YiYeQi.AddDisposablepathAnimationEndEvent(() => delayEventBuffer.StartTimer());
    }

    void SeventyThree_niko()
    {
        //给扳机按下注射纯水到离心管3号动画绑定事件
        delayEventBuffer.AdddelayInvokeEvent(ReadFuncListToBinding);
        delayEventBuffer.AdddelayInvokeEvent(EndtYiYeQiPathAnimation);
        delayEventBuffer.AdddelayInvokeEvent(() => YiYeQi.nodeStartEvent.Add(1, PlayBackBanji));
    }

    void SeventyFour_niko()
    {
        //给移液器返回移液器架子上动画结束绑定事件    对应的是激活固相萃取柱架子的点击功能
        YiYeQi.AddDisposablepathAnimationEndEvent(ReadFuncListToBinding);
        YiYeQi.AddDisposablepathAnimationEndEvent(() => { UpdateStepFunc(t: "CuiQuZhuJiaZiZhiShi", edgeHighlight: CuiQuZhuJiaZiLight); });
    }

    void SeventyFive_niko()
    {
        //给点击固相萃取柱架子绑定事件
        CuiQuZhuJiaZiLight.AddDisposableclickObjectEvent(ReadFuncListToBinding);
        CuiQuZhuJiaZiLight.AddDisposableclickObjectEvent(ExcuteStepFunc);
        CuiQuZhuJiaZiLight.AddDisposableclickObjectEvent(() => { CuiQuZhuJiaZi.ThisTransform.SetParent(null); CuiQuZhuJiaZi.PlayOncePathAnimation(ObjectTransformAnimationMode.P_R, AnimationStopMode.Keep, "Project/PathAnimation/Melamine/CuiQuZhuJiaZiPathAnim", new PathAnimationParamete(0, true)); });
    }

    void SeventySix_niko()
    {
        //给萃取柱架子移动动画结束绑定事件
        CuiQuZhuJiaZi.AddDisposablepathAnimationEndEvent(ReadFuncListToBinding);
        CuiQuZhuJiaZi.AddDisposablepathAnimationEndEvent(() => { LiXinGuan3.PlayOncePathAnimation(ObjectTransformAnimationMode.P_R, AnimationStopMode.Keep, "Project/PathAnimation/Melamine/LiXinGuan3DaoShuiToCuiQuZhu"); });
    }

    void SeventySeven_niko()
    {
        //给离心管3倒水动画结束绑定事件
        LiXinGuan3.AddDisposablepathAnimationEndEvent(ReadFuncListToBinding);
        LiXinGuan3.AddDisposablepathAnimationEndEvent(() => 
        {
            Guan3_YeMian_1.DisActiveGameObject();
            Guan3_YeMian_2.DisActiveGameObject();
            lvYeGoToImmunoaffinity.gameObject.SetActive(true);
            lvYeGoToImmunoaffinity.Play();
            delayEventBuffer.StartTimer(4);
        });
    }

    void SeventyEight_niko()
    {
        //给倒滤液进固相萃取柱动画结束绑定事件
        delayEventBuffer.AdddelayInvokeEvent(ReadFuncListToBinding);
        delayEventBuffer.AdddelayInvokeEvent(() =>
        {
            lvYeGoToImmunoaffinity.Stop();
            lvYeGoToImmunoaffinity.gameObject.SetActive(false);
            LiXinGuan3.PlayOncePathAnimation(ObjectTransformAnimationMode.P_R, AnimationStopMode.Keep, "Project/PathAnimation/Melamine/LiXinGuan3TuiHuiJiaZi");
        });
    }



    void SeventyNine_niko()
    {
        //给离心管3退回到架子上绑定事件
        LiXinGuan3.AddDisposablepathAnimationEndEvent(ReadFuncListToBinding);
        LiXinGuan3.AddDisposablepathAnimationEndEvent(()=> 
        {
            CuiQuZhuJiaZi.ThisTransform.SetParent(player.PlayerEyes);
            CuiQuZhuJiaZi.ThisTransform.localPosition = new Vector3(0.0258f, -0.0699f, 0.354f);
            player.Dead();
            player.PlayPathAnimationFromStart("Project/PathAnimation/Melamine/PlayerFromShiYanTaiToCuiQuYi");
        });
    }

    void Eighty_niko()
    {
        //给玩家移动到萃取仪位置动画结束绑定事件
        player.AddDisposablepathAnimationEndEvent(ReadFuncListToBinding);
        player.AddDisposablepathAnimationEndEvent(()=> 
        {
            CuiQuZhuJiaZi.ThisTransform.SetParent(null);
            CuiQuZhuJiaZi.PlayOncePathAnimation(ObjectTransformAnimationMode.P_R, AnimationStopMode.Keep, "Project/PathAnimation/Melamine/CuiQuZhuJiaZiToCuiQuYi");
        });

    }

    void EightyOne_niko()
    {
        //给萃取柱架子移动倒萃取仪旁边动画结束绑定事件
        CuiQuZhuJiaZi.AddDisposablepathAnimationEndEvent(ReadFuncListToBinding);
        CuiQuZhuJiaZi.AddDisposablepathAnimationEndEvent(() => 
        {
            player.Life();
            UpdateStepFunc(t: "GuXiangCuiQuYiZhiShi", edgeHighlight: GuXiangCuiQuYiLight);
        });
    }

    void EightyTwo_niko()
    {
        //给点击萃取仪绑定事件    
        GuXiangCuiQuYiLight.AddDisposableclickObjectEvent(ReadFuncListToBinding);
        GuXiangCuiQuYiLight.AddDisposableclickObjectEvent(ExcuteStepFunc);
        GuXiangCuiQuYiLight.AddDisposableclickObjectEvent(() =>
        {
            GuXiangCuiQuYiDoor.PlayOncePathAnimation(ObjectTransformAnimationMode.R, AnimationStopMode.Keep, "Project/PathAnimation/Melamine/GuXiangCuiQuYiOpenDoor", new PathAnimationParamete(0, true));
        });
    }

    void EightyThree_niko()
    {
        //给箱门打开动画结束后帮绑定事件
        GuXiangCuiQuYiDoor.AddDisposablepathAnimationEndEvent(ReadFuncListToBinding);
        GuXiangCuiQuYiDoor.AddDisposablepathAnimationEndEvent(() =>
        {
            firstImmunoaffinity.transform.SetParent(null);
            firstImmunoaffinity.PlayOncePathAnimation(ObjectTransformAnimationMode.P_R, AnimationStopMode.Keep, "Project/PathAnimation/Melamine/firstImmunoaffinityPathAnimation", new PathAnimationParamete(0, true));
        });
    }

    void EightyFour_niko()
    {
        //给第一根免疫亲和柱插在固相萃取仪上动画结束绑定事件
        firstImmunoaffinity.AddDisposablepathAnimationEndEvent(ReadFuncListToBinding);
        firstImmunoaffinity.AddDisposablepathAnimationEndEvent(() => delayToWakeUpGameObjectsCon.Play(immunoaffinityArray, 0.2f, immunoaffinityDestTransformArray));
    }

    void EightyFive_niko()
    {
        //给固相萃取柱出现动画结束后绑定事件      开启屏幕亮度和计时器界面
        delayToWakeUpGameObjectsCon.AddDisposableEvent(ReadFuncListToBinding);
        delayToWakeUpGameObjectsCon.AddDisposableEvent(() =>
        {
            lightScreen.SetActive(true);
            CuiQuTimer.StartTimer();
        });
    }

    void EightySix_niko()
    {
        //给萃取仪计时器动画结束后绑定事件
        CuiQuTimer.AddDisposableimageTimerEndEvent(ReadFuncListToBinding);
        CuiQuTimer.AddDisposableimageTimerEndEvent(() =>
        {
            lightScreen.SetActive(false);
            UpdateStepFunc(t: "HPLCZhiShi", edgeHighlight: HPLCLight);
            player.SetPlayerTransform("GoToTakeHPLC_player");
        });
    }

    void EightySeven_niko()
    {
        //给点击液相色谱仪绑定事件
        HPLCLight.AddDisposableclickObjectEvent(ReadFuncListToBinding);
        HPLCLight.AddDisposableclickObjectEvent(ExcuteStepFunc);
        HPLCLight.AddDisposableclickObjectEvent(() =>
        {
            sceneLogicManager.DisActiveHud();
            resultPanel.ActiveGameObject();
        });
    }


    void EightyEight_niko()
    {
        //给点击分析结果面板绑定事件
        resultPanel.AddDisposableClickEvent(sceneLogicManager.ActiveHud);
        resultPanel.AddDisposableClickEvent(sceneLogicManager.CompleteAllSteps);
    }































}

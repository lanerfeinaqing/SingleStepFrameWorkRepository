using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.Events;

/// <summary>
/// 用来供路径动画运行的参数类，第一个变量是动画开始前等待时间，第二个变量是要不要开启查看名称功能
/// </summary>
public class PathAnimationParamete
{
    public float delayTime;
    public bool lookNameQuad;
    public PathAnimationParamete()
    {
        delayTime = 0;
        lookNameQuad = false;
    }
    public PathAnimationParamete(float t = 0, bool value = false)
    {
        delayTime = t;
        lookNameQuad = value;
    }
}

public class PathTransformAnimationClass_CF : ReWriteGameControl_CF {


    //矩阵变换动画功能用到的变量

    /// <summary>
    /// 控制矩阵变换动画中位置、旋转、缩放哪几个动画，默认都动
    /// </summary>
    ObjectTransformAnimationMode transformAnimationMode = ObjectTransformAnimationMode.P_R_S;
    /// <summary>
    /// 控制矩阵变换动画播放是单次、从头循环还是来回循环
    /// </summary>
     AnimationPlayMode animationPlayMode = AnimationPlayMode.Once;
    /// <summary>
    /// 控制动画结束完成时是保持动画结束状态还是恢复到初始状态
    /// </summary>
     AnimationStopMode animationStopMode = AnimationStopMode.Keep;
    /// <summary>
    /// 动画是否在播放
    /// </summary>
    bool isPlaying = false;
    public bool IsPlaying
    {
        get { return isPlaying; }
    }

    /// <summary>
    /// 播放的路径动画要用的参数
    /// </summary>
    PathAnimationParamete animationParamete;

    /// <summary>
    /// 和路径动画相关联的mesh文字,在正式开始动画前会展示它,假如它存在的话
    /// </summary>
    GameObject nameQuad;

    /// <summary>
    ///矩阵路径动画节点纪录列表
    /// </summary>
    List<Transform> pathAnimationTransformList = new List<Transform>();
    /// <summary>
    /// 每个动画片段的开始事件
    /// </summary>
    public Dictionary<int, Action> nodeStartEvent = new Dictionary<int, Action>();
    /// <summary>
    ///矩阵路径动画片段所需时间纪录列表
    /// </summary>
    List<float> pathAnimationTimeList = new List<float>();
    /// <summary>
    /// 两点间空间分割计数器
    /// </summary>
    int destTransformCounter = 0;
    /// <summary>
    /// 从磁盘加载到的动画资源
    /// </summary>
    UnityEngine.Object pathAnimationSource;
    /// <summary>
    /// 循环动画的循环次数,默认为2
    /// </summary>
    int loopCount = 2;
    /// <summary>
    /// 矩阵路径动画开始事件
    /// </summary>
    public event Action pathAnimationStartEvent;
    /// <summary>
    /// 一次性计时器开始事件列表
    /// </summary>
    List<Action> disposablepathAnimationStarEventList = new List<Action>();
    /// <summary>
    /// 一次性计时器开始事件列表
    /// </summary>
    public void AddDisposablepathAnimationStarEvent(Action ac)
    {
        disposablepathAnimationStarEventList.Add(ac);
    }

    /// <summary>
    /// 矩阵路径动画结束事件
    /// </summary>
    public event Action pathAnimationEndEvent;
    /// <summary>
    /// 一次性计时器结束事件列表
    /// </summary>
    List<Action> disposablepathAnimationEndEventList = new List<Action>();
    /// <summary>
    ///一次性计时器结束事件列表
    /// </summary>
    public void AddDisposablepathAnimationEndEvent(Action ac)
    {
        disposablepathAnimationEndEventList.Add(ac);
    }






    public override void ActiveGameObject()
    {
        base.ActiveGameObject();
    }

    public override void DisActiveGameObject()
    {
        base.DisActiveGameObject();
    }

    public override void PauseGameFunc()
    {
        base.PauseGameFunc();
    }

    public override void ReplayGameFunc()
    {
        base.ReplayGameFunc();
    }

    public override void SetPlayerTransform(Vector3 Position, Vector3 Rotation)
    {
        base.SetPlayerTransform(Position, Rotation);
    }

    public override void SetPlayerTransform(Transform t)
    {
        base.SetPlayerTransform(t);
    }

    protected override void Awake()
    {
        if(transform.Find("NameQuad") !=null)
        {
            nameQuad = transform.Find("NameQuad").gameObject;
            nameQuad.SetActive(false);
        }
        base.Awake();
    }

    protected override void OnDestroy()
    {
        base.OnDestroy();
    }

    protected override void OnDisable()
    {
        base.OnDisable();
    }

    protected override void OnEnable()
    {
        base.OnEnable();
    }

    protected override void Start()
    {
        base.Start();
    }



    /// <summary>
    /// 正向播放单次矩阵路径动画从指定点矩阵，参数从场景中来
    /// </summary>
    /// <param name="transformMode">矩阵三轴模式</param>
    /// <param name="stopMode"></param>
    /// <param name="animationParent"></param>
    /// <param name="delay"></param>
    public void PlayOncePathAnimation(ObjectTransformAnimationMode transformMode,AnimationStopMode stopMode,Transform animationParent, PathAnimationParamete delay= null)
    {
        if (isPlaying)
        {
            if (Application.platform == RuntimePlatform.WindowsEditor)
            {
                throw new Exception("目前有动画片段正在播放，请等待其结束");
            }
            else
            {
                return;
            }
        }
        else
        {
            animationPlayMode = AnimationPlayMode.Once;
            transformAnimationMode = transformMode;
            animationStopMode = stopMode;
            SetPathAnimationTransformStartToEndList(animationParent);
            isPlaying = true;

            foreach (Action item in disposablepathAnimationStarEventList)
            {
                pathAnimationStartEvent += item;
            }
            pathAnimationStartEvent?.Invoke();
            foreach (Action item in disposablepathAnimationStarEventList)
            {
                pathAnimationStartEvent -= item;
            }
            disposablepathAnimationStarEventList.Clear();

            if (delay == null)
            {
                animationParamete = new PathAnimationParamete(0, false);
            }
            else
            {
                animationParamete = delay;
            }
            StartCoroutine("MoveFunc");

        }
    }

    /// <summary>
    /// 正向播放单次矩阵路径动画从指定点矩阵,参数动态加载而来
    /// </summary>
    /// <param name="transformMode"></param>
    /// <param name="stopMode"></param>
    /// <param name="address"></param>
    /// <param name="delay"></param>
    public void PlayOncePathAnimation(ObjectTransformAnimationMode transformMode, AnimationStopMode stopMode, string address, PathAnimationParamete delay = null)
    {
        if (isPlaying)
        {
            if (Application.platform == RuntimePlatform.WindowsEditor)
            {
                throw new Exception("目前有动画片段正在播放，请等待其结束");
            }
            else
            {
                return;
            }
        }
        else
        {
            animationPlayMode = AnimationPlayMode.Once;
            transformAnimationMode = transformMode;
            animationStopMode = stopMode;
            pathAnimationSource = Resources.Load(address);
            SetPathAnimationTransformStartToEndList((pathAnimationSource as GameObject).transform);
            isPlaying = true;
            foreach (Action item in disposablepathAnimationStarEventList)
            {
                pathAnimationStartEvent += item;
            }
            pathAnimationStartEvent?.Invoke();
            foreach (Action item in disposablepathAnimationStarEventList)
            {
                pathAnimationStartEvent -= item;
            }
            disposablepathAnimationStarEventList.Clear();
            if (delay == null)
            {
                animationParamete = new PathAnimationParamete(0, false);
            }
            else
            {
                animationParamete = delay;
            }
            StartCoroutine("MoveFunc");
        } 
    }



    /// <summary>
    /// 反向播放单次矩阵路径动画从指定点矩阵，参数从场景中来
    /// </summary>
    /// <param name="transformMode">矩阵三轴模式</param>
    /// <param name="stopMode"></param>
    /// <param name="animationParent"></param>
    /// <param name="delay"></param>
    public void InvertPlayOncePathAnimation(ObjectTransformAnimationMode transformMode, AnimationStopMode stopMode, Transform animationParent, PathAnimationParamete delay = null)
    {
        if (isPlaying)
        {
            if (Application.platform == RuntimePlatform.WindowsEditor)
            {
                throw new Exception("目前有动画片段正在播放，请等待其结束");
            }
            else
            {
                return;
            }
        }
        else
        {
            animationPlayMode = AnimationPlayMode.Once;
            transformAnimationMode = transformMode;
            animationStopMode = stopMode;
            SetPathAnimationTransformEndtToStartList(animationParent);
            isPlaying = true;
            foreach (Action item in disposablepathAnimationStarEventList)
            {
                pathAnimationStartEvent += item;
            }
            pathAnimationStartEvent?.Invoke();
            foreach (Action item in disposablepathAnimationStarEventList)
            {
                pathAnimationStartEvent -= item;
            }
            disposablepathAnimationStarEventList.Clear();
            if (delay == null)
            {
                animationParamete = new PathAnimationParamete(0, false);
            }
            else
            {
                animationParamete = delay;
            }
            StartCoroutine("MoveFunc");
        }
    }

    /// <summary>
    /// 反向播放单次矩阵路径动画从指定点矩阵,参数动态加载而来
    /// </summary>
    /// <param name="transformMode"></param>
    /// <param name="stopMode"></param>
    /// <param name="address"></param>
    /// <param name="delay"></param>
    public void InvertPlayOncePathAnimation(ObjectTransformAnimationMode transformMode, AnimationStopMode stopMode, string address, PathAnimationParamete delay = null)
    {
        if (isPlaying)
        {
            if (Application.platform == RuntimePlatform.WindowsEditor)
            {
                throw new Exception("目前有动画片段正在播放，请等待其结束");
            }
            else
            {
                return;
            }
        }
        else
        {
            animationPlayMode = AnimationPlayMode.Once;
            transformAnimationMode = transformMode;
            animationStopMode = stopMode;
            pathAnimationSource = Resources.Load(address);
            SetPathAnimationTransformEndtToStartList((pathAnimationSource as GameObject).transform);
            isPlaying = true;
            foreach (Action item in disposablepathAnimationStarEventList)
            {
                pathAnimationStartEvent += item;
            }
            pathAnimationStartEvent?.Invoke();
            foreach (Action item in disposablepathAnimationStarEventList)
            {
                pathAnimationStartEvent -= item;
            }
            disposablepathAnimationStarEventList.Clear();
            if (delay == null)
            {
                animationParamete = new PathAnimationParamete(0, false);
            }
            else
            {
                animationParamete = delay;
            }
            StartCoroutine("MoveFunc");
        }
    }




    /// <summary>
    /// 播放循环矩阵路径动画从指定点矩阵，参数从场景中来
    /// </summary>
    /// <param name="loopCount"></param>
    /// <param name="transformMode"></param>
    /// <param name="stopMode"></param>
    /// <param name="animationParent"></param>
    /// <param name="delay"></param>
    public void PlayLoopPathAnimation(int loopCount,ObjectTransformAnimationMode transformMode, AnimationStopMode stopMode, Transform animationParent, PathAnimationParamete delay = null)
    {
        if (loopCount == 0)
        {
            throw new Exception("播放次数不能为0，要么是负数无限循环播放，要么是正数，有限次的循环");
        }
        if (isPlaying)
        {
            if (Application.platform == RuntimePlatform.WindowsEditor)
            {
                throw new Exception("目前有动画片段正在播放，请等待其结束");
            }
            else
            {
                return;
            }
        }
        else
        {
            animationPlayMode = AnimationPlayMode.Loop;
            this.loopCount = loopCount;
            transformAnimationMode = transformMode;
            animationStopMode = stopMode;
            SetPathAnimationTransformStartToEndList(animationParent);
            isPlaying = true;
            foreach (Action item in disposablepathAnimationStarEventList)
            {
                pathAnimationStartEvent += item;
            }
            pathAnimationStartEvent?.Invoke();
            foreach (Action item in disposablepathAnimationStarEventList)
            {
                pathAnimationStartEvent -= item;
            }
            disposablepathAnimationStarEventList.Clear();
            if (delay == null)
            {
                animationParamete = new PathAnimationParamete(0, false);
            }
            else
            {
                animationParamete = delay;
            }
            StartCoroutine("MoveFunc");
        }
    }
    /// <summary>
    /// 播放循环矩阵路径动画从指定点矩阵，参数动态加载而来
    /// </summary>
    /// <param name="loopCount"></param>
    /// <param name="transformMode"></param>
    /// <param name="stopMode"></param>
    /// <param name="address"></param>
    /// <param name="delay"></param>
    public void PlayLoopPathAnimation(int loopCount, ObjectTransformAnimationMode transformMode, AnimationStopMode stopMode, string address, PathAnimationParamete delay = null)
    {
        if (loopCount == 0)
        {
            throw new Exception("播放次数不能为0，要么是负数无限循环播放，要么是正数，有限次的循环");
        }
        if (isPlaying)
        {
            if (Application.platform == RuntimePlatform.WindowsEditor)
            {
                throw new Exception("目前有动画片段正在播放，请等待其结束");
            }
            else
            {
                return;
            }
        }
        else
        {
            animationPlayMode = AnimationPlayMode.Loop;
            this.loopCount = loopCount;
            transformAnimationMode = transformMode;
            animationStopMode = stopMode;
            pathAnimationSource = Resources.Load(address);
            SetPathAnimationTransformStartToEndList((pathAnimationSource as GameObject).transform);
            isPlaying = true;
            foreach (Action item in disposablepathAnimationStarEventList)
            {
                pathAnimationStartEvent += item;
            }
            pathAnimationStartEvent?.Invoke();
            foreach (Action item in disposablepathAnimationStarEventList)
            {
                pathAnimationStartEvent -= item;
            }
            disposablepathAnimationStarEventList.Clear();
            if (delay == null)
            {
                animationParamete = new PathAnimationParamete(0, false);
            }
            else
            {
                animationParamete = delay;
            }
            StartCoroutine("MoveFunc");
        }
    }




    /// <summary>
    /// 播放PingPong矩阵路径动画从指定点矩阵，参数从场景中来
    /// </summary>
    /// <param name="loopCount"></param>
    /// <param name="transformMode"></param>
    /// <param name="stopMode"></param>
    /// <param name="animationParent"></param>
    /// <param name="delay"></param>
    public void PlayPingPongPathAnimation(int loopCount, ObjectTransformAnimationMode transformMode, AnimationStopMode stopMode, Transform animationParent, PathAnimationParamete delay = null)
    {
        if(loopCount==0)
        {
            throw new Exception("播放次数不能为0，要么是负数无限循环播放，要么是正数，有限次的循环");
        }
        if (isPlaying)
        {
            if (Application.platform == RuntimePlatform.WindowsEditor)
            {
                throw new Exception("目前有动画片段正在播放，请等待其结束");
            }
            else
            {
                return;
            }
        }
        else
        {
            animationPlayMode = AnimationPlayMode.PingPong;
            this.loopCount = loopCount;
            transformAnimationMode = transformMode;
            animationStopMode = stopMode;
            SetPathAnimationTransPingPongList(animationParent);
            isPlaying = true;
            foreach (Action item in disposablepathAnimationStarEventList)
            {
                pathAnimationStartEvent += item;
            }
            pathAnimationStartEvent?.Invoke();
            foreach (Action item in disposablepathAnimationStarEventList)
            {
                pathAnimationStartEvent -= item;
            }
            disposablepathAnimationStarEventList.Clear();
            if (delay == null)
            {
                animationParamete = new PathAnimationParamete(0, false);
            }
            else
            {
                animationParamete = delay;
            }
            StartCoroutine("MoveFunc");
        }
    }
    /// <summary>
    /// 播放PingPong矩阵路径动画从指定点矩阵，参数动态加载而来
    /// </summary>
    /// <param name="loopCount"></param>
    /// <param name="transformMode"></param>
    /// <param name="stopMode"></param>
    /// <param name="address"></param>
    /// <param name="delay"></param>
    public void PlayPingPongPathAnimation(int loopCount, ObjectTransformAnimationMode transformMode, AnimationStopMode stopMode, string address, PathAnimationParamete delay = null)
    {
        if (loopCount == 0)
        {
            throw new Exception("播放次数不能为0，要么是负数无限循环播放，要么是正数，有限次的循环");
        }
        if (isPlaying)
        {
            if (Application.platform == RuntimePlatform.WindowsEditor)
            {
                throw new Exception("目前有动画片段正在播放，请等待其结束");
            }
            else
            {
                return;
            }
        }
        else
        {
            animationPlayMode = AnimationPlayMode.PingPong;
            this.loopCount = loopCount;
            transformAnimationMode = transformMode;
            animationStopMode = stopMode;
            pathAnimationSource = Resources.Load(address);
            SetPathAnimationTransPingPongList((pathAnimationSource as GameObject).transform);
            isPlaying = true;
            foreach (Action item in disposablepathAnimationStarEventList)
            {
                pathAnimationStartEvent += item;
            }
            pathAnimationStartEvent?.Invoke();
            foreach (Action item in disposablepathAnimationStarEventList)
            {
                pathAnimationStartEvent -= item;
            }
            disposablepathAnimationStarEventList.Clear();
            if (delay == null)
            {
                animationParamete = new PathAnimationParamete(0, false);
            }
            else
            {
                animationParamete = delay;
            }
            StartCoroutine("MoveFunc");
        }
    }


    /// <summary>
    /// 结束矩阵路径动画
    /// </summary>
    void CompletePathAnimation()
    {
        StopCoroutine("MoveFunc");
        foreach (Action item in disposablepathAnimationEndEventList)
        {
            pathAnimationEndEvent += item;
        }
        pathAnimationEndEvent?.Invoke();
        foreach (Action item in disposablepathAnimationEndEventList)
        {
            pathAnimationEndEvent -= item;
        }
        disposablepathAnimationEndEventList.Clear();

        pathAnimationTransformList.Clear();
        pathAnimationTimeList.Clear();
        nodeStartEvent.Clear();
        this.loopCount = 0;
        destTransformCounter = 0;
        EventCenter_CF.Instance.OpenInteractiveControl();
        pathAnimationSource = null;
        isPlaying = false;
        Resources.UnloadUnusedAssets();
    }



    /// <summary>
    /// 从首到尾：设置矩阵路径动画所需要的路径点和对应的所需时间
    /// </summary>
    void SetPathAnimationTransformStartToEndList(Transform pathAnimation)
    {
        if (pathAnimationTransformList.Count > 0)
        {
            pathAnimationTransformList.Clear();
        }
        if (pathAnimationTimeList.Count > 0)
        {
            pathAnimationTimeList.Clear();
        }
        string timeText = "";
        float time = 0;

        for (int i = 0; i < pathAnimation.childCount; i++)
        {

            pathAnimationTransformList.Add(pathAnimation.GetChild(i));
            timeText = pathAnimation.GetChild(i).name.Split('_')[1];
            time = 0;
            if (float.TryParse(timeText, out time))
            {
                if (time <= 0)
                {
                    throw new Exception("矩阵路径动画中名为" + pathAnimation.GetChild(i).name + ",索引号为" + i + "的位置节点的动画片段所需时间错误,时间不能小于等于0");
                }
                else
                {
                    pathAnimationTimeList.Add(time);
                }
            }
            else
            {
                throw new Exception("矩阵路径动画中名为" + pathAnimation.GetChild(i).name + ",索引号为" + i + "的位置节点的动画片段所需时间填写有误,请注意格式");
            }
        }
        pathAnimationTimeList.RemoveAt(pathAnimationTimeList.Count - 1);
        if (pathAnimationTransformList.Count != pathAnimation.childCount)
        {
            throw new Exception("单次动画:矩阵路径动画transform节点列表数量与动画父物体的子物体数量不一致");
        }
        int n = pathAnimation.childCount - 1 - pathAnimationTimeList.Count;

        if (n > 0 || n < 0)
        {
            throw new Exception("单次动画:矩阵路径动画片段列表数量与动画父物体的子物体数量不匹配，时间个数应比节点个数少1,目前的差为 " + n);
        }


    }


    /// <summary>
    /// 从尾到首:设置矩阵路径动画所需要的路径点和对应的所需时间
    /// </summary>
    void SetPathAnimationTransformEndtToStartList(Transform pathAnimation)
    {
        if (pathAnimationTransformList.Count > 0)
        {
            pathAnimationTransformList.Clear();
        }
        if (pathAnimationTimeList.Count > 0)
        {
            pathAnimationTimeList.Clear();
        }
        string timeText = "";
        float time = 0;

        for (int i = pathAnimation.childCount-1; i >=0 ; i--)
        {

            pathAnimationTransformList.Add(pathAnimation.GetChild(i));
            timeText = pathAnimation.GetChild(i).name.Split('_')[1];
            time = 0;
            if (float.TryParse(timeText, out time))
            {
                if (time <= 0)
                {
                    throw new Exception("矩阵路径动画中名为" + pathAnimation.GetChild(i).name + ",索引号为" + i + "的位置节点的动画片段所需时间错误,时间不能小于等于0");
                }
                else
                {
                    pathAnimationTimeList.Add(time);
                }
            }
            else
            {
                throw new Exception("矩阵路径动画中名为" + pathAnimation.GetChild(i).name + ",索引号为" + i + "的位置节点的动画片段所需时间填写有误,请注意格式");
            }
        }

        pathAnimationTimeList.RemoveAt(pathAnimationTimeList.Count - 1);
        if (pathAnimationTransformList.Count != pathAnimation.childCount)
        {
            throw new Exception("单次动画:矩阵路径动画transform节点列表数量与动画父物体的子物体数量不一致");
        }
        int n = pathAnimation.childCount - 1 - pathAnimationTimeList.Count;

        if (n > 0 || n < 0)
        {
            throw new Exception("单次动画:矩阵路径动画片段列表数量与动画父物体的子物体数量不匹配，时间个数应比节点个数少1,目前的差为 " + n);
        }

    }



    /// <summary>
    /// PingPong数据:设置矩阵路径动画所需要的路径点和对应的所需时间,PingPong模式，多添一轮数据以供动画往返
    /// </summary>
    void SetPathAnimationTransPingPongList(Transform pathAnimation)
    {
        if (pathAnimationTransformList.Count > 0)
        {
            pathAnimationTransformList.Clear();
        }
        if (pathAnimationTimeList.Count > 0)
        {
            pathAnimationTimeList.Clear();
        }
        string timeText = "";
        float time = 0;

        for (int i = 0; i< pathAnimation.childCount; i++)
        {

            pathAnimationTransformList.Add(pathAnimation.GetChild(i));
            timeText = pathAnimation.GetChild(i).name.Split('_')[1];
            time = 0;
            if (float.TryParse(timeText, out time))
            {
                if (time <= 0)
                {
                    throw new Exception("矩阵路径动画中名为" + pathAnimation.GetChild(i).name + ",索引号为" + i + "的位置节点的动画片段所需时间错误,时间不能小于等于0");
                }
                else
                {
                    pathAnimationTimeList.Add(time);
                }
            }
            else
            {
                throw new Exception("矩阵路径动画中名为" + pathAnimation.GetChild(i).name + ",索引号为" + i + "的位置节点的动画片段所需时间填写有误,请注意格式");
            }
        }
        pathAnimationTimeList.RemoveAt(pathAnimationTimeList.Count - 1);
        if (pathAnimationTransformList.Count != pathAnimation.childCount)
        {
            throw new Exception("单次动画:矩阵路径动画transform节点列表数量与动画父物体的子物体数量不一致,列表中的数量为： " + pathAnimationTransformList.Count);
        }
        int n = pathAnimation.childCount - 1 - pathAnimationTimeList.Count;

        if (n > 0 || n < 0)
        {
            throw new Exception("单次动画:矩阵路径动画片段列表数量与动画父物体的子物体数量不匹配，时间个数应比节点个数少1,目前的差为 " + n);
        }

        List<Transform> tempTList = new List<Transform>();
        for (int m = pathAnimationTransformList.Count - 2; m >= 0; m--)
        {
            tempTList.Add(pathAnimationTransformList[m]);
        }
        pathAnimationTransformList.AddRange(tempTList);


        List<float> tempTimeList = new List<float>();
        for (int l = pathAnimationTimeList.Count - 1; l >= 0; l--)
        {
            tempTimeList.Add(pathAnimationTimeList[l]);
        }
        pathAnimationTimeList.AddRange(tempTimeList);


    }






    /// <summary>
    /// 动画矩阵更新函数
    /// </summary>
    /// <param name="t"></param>
    /// <returns></returns>
    IEnumerator MoveFunc()
    {
        float timer = 0;
        float waitRate = 0.1f;
        if(animationParamete!=null)
        {
            while (timer < animationParamete.delayTime)
            {
                if (pauseGame)
                {
                    yield return new WaitForSeconds(waitRate);
                }
                else
                {
                    timer += waitRate;
                    yield return new WaitForSeconds(waitRate);
                }
            }
        }
        //记录物体在动画播放前所处的位置
        Vector3 recordObjPosition = thistransform.localPosition;
        Vector3 recordObjRotation = thistransform.localEulerAngles;
        Vector3 recordObjScale = thistransform.localScale;
        EventCenter_CF.Instance.CloseInteractiveControl();

        if(animationParamete != null)
        {
            //如果对应的文字模型存在，则将其展示出来
            if ((nameQuad != null) && (animationParamete.lookNameQuad == true))
            {
                nameQuad.SetActive(true);
                timer = 0;
                while (timer < 1.0)
                {
                    timer += waitRate;
                    yield return new WaitForSeconds(waitRate);
                }
                timer = 0;
                nameQuad.SetActive(false);
            }
        }
       
        //根据播放动画的不同模式，来控制动画的代码块的运作
        switch (animationPlayMode)
        {
            case AnimationPlayMode.Once:
                for (int i = 0; i < pathAnimationTransformList.Count - 1; i++)
                {
                    Transform nodeStarTransform;
                    Transform nodeEndTransform;
                    Vector3 diffrencePosition;
                    Vector3 differenceRotation;
                    Vector3 diffrenceScale;
                    if(nodeStartEvent.ContainsKey(i))
                    {
                        nodeStartEvent[i].Invoke();
                    }
                    switch (transformAnimationMode)
                    {
                        case ObjectTransformAnimationMode.P_R_S:
                            destTransformCounter = (int)(pathAnimationTimeList[i] / Time.fixedDeltaTime);
                            nodeStarTransform = pathAnimationTransformList[i];
                            nodeEndTransform = pathAnimationTransformList[i + 1];
                            diffrencePosition = nodeEndTransform.localPosition - nodeStarTransform.localPosition;
                            differenceRotation = nodeEndTransform.localEulerAngles - nodeStarTransform.localEulerAngles;
                            differenceRotation = new Vector3(Math_CF.CheckAngle(differenceRotation.x), Math_CF.CheckAngle(differenceRotation.y), Math_CF.CheckAngle(differenceRotation.z));
                            diffrenceScale = nodeEndTransform.localScale - nodeStarTransform.localScale;
                            for (int j = 0; j < destTransformCounter; j++)
                            {
                                while (pauseGame)
                                {
                                    yield return new WaitForSeconds(waitRate);
                                }
                                if(j==0)
                                {
                                    thistransform.localPosition =  nodeStarTransform.localPosition;
                                    thistransform.localEulerAngles = nodeStarTransform.localEulerAngles;
                                    thistransform.localScale = nodeStarTransform.localScale;
                                }
                                else if(j==destTransformCounter-1)
                                {
                                    thistransform.localPosition = nodeEndTransform.localPosition;
                                    thistransform.localEulerAngles = nodeEndTransform.localEulerAngles;
                                    thistransform.localScale = nodeEndTransform.localScale;
                                }
                                else
                                {
                                    float a = j;
                                    float b = destTransformCounter;
                                    float Proportion = a / b;
                                    thistransform.localPosition = Proportion * diffrencePosition + nodeStarTransform.localPosition;
                                    thistransform.localEulerAngles = (Proportion * differenceRotation) + nodeStarTransform.localEulerAngles;
                                    thistransform.localScale = Proportion * diffrenceScale + nodeStarTransform.localScale;
                                }
                                yield return new WaitForFixedUpdate();
                            }
                            break;
                        case ObjectTransformAnimationMode.P:
                            destTransformCounter = (int)(pathAnimationTimeList[i] / Time.fixedDeltaTime);
                            nodeStarTransform = pathAnimationTransformList[i];
                            nodeEndTransform = pathAnimationTransformList[i + 1];
                            diffrencePosition = nodeEndTransform.localPosition - nodeStarTransform.localPosition;
                            for (int j = 0; j < destTransformCounter; j++)
                            {
                                while (pauseGame)
                                {
                                    yield return new WaitForSeconds(waitRate);
                                }
                                if (j == 0)
                                {
                                    thistransform.localPosition = nodeStarTransform.localPosition;
                                   
                                }
                                else if (j == destTransformCounter - 1)
                                {
                                    thistransform.localPosition = nodeEndTransform.localPosition;
                                    
                                }
                                else
                                {
                                    float a = j;
                                    float b = destTransformCounter;
                                    float Proportion = a / b;
                                    thistransform.localPosition = Proportion * diffrencePosition + nodeStarTransform.localPosition;
                                }
                                yield return new WaitForFixedUpdate();
                            }
                            break;
                        case ObjectTransformAnimationMode.R:
                            destTransformCounter = (int)(pathAnimationTimeList[i] / Time.fixedDeltaTime);
                            nodeStarTransform = pathAnimationTransformList[i];
                            nodeEndTransform = pathAnimationTransformList[i + 1];
                            differenceRotation = nodeEndTransform.localEulerAngles - nodeStarTransform.localEulerAngles;
                            differenceRotation = new Vector3(Math_CF.CheckAngle(differenceRotation.x), Math_CF.CheckAngle(differenceRotation.y), Math_CF.CheckAngle(differenceRotation.z));
                            for (int j = 0; j < destTransformCounter; j++)
                            {
                                while (pauseGame)
                                {
                                    yield return new WaitForSeconds(waitRate);
                                }
                                if (j == 0)
                                {
                                    thistransform.localEulerAngles = nodeStarTransform.localEulerAngles;
                                }
                                else if (j == destTransformCounter - 1)
                                {
                                    thistransform.localEulerAngles = nodeEndTransform.localEulerAngles;
                                }
                                else
                                {
                                    float a = j;
                                    float b = destTransformCounter;
                                    float Proportion = a / b;
                                    thistransform.localEulerAngles = (Proportion * differenceRotation) + nodeStarTransform.localEulerAngles;
                                }
                                yield return new WaitForFixedUpdate();
                            }
                            break;
                        case ObjectTransformAnimationMode.S:
                            destTransformCounter = (int)(pathAnimationTimeList[i] / Time.fixedDeltaTime);
                            nodeStarTransform = pathAnimationTransformList[i];
                            nodeEndTransform = pathAnimationTransformList[i + 1];
                            diffrenceScale = nodeEndTransform.localScale - nodeStarTransform.localScale;
                            for (int j = 0; j < destTransformCounter; j++)
                            {
                                while (pauseGame)
                                {
                                    yield return new WaitForSeconds(waitRate);
                                }
                                if (j == 0)
                                {
                                    thistransform.localScale = nodeStarTransform.localScale;
                                }
                                else if (j == destTransformCounter - 1)
                                {
                                    thistransform.localScale = nodeEndTransform.localScale;
                                }
                                else
                                {
                                    float a = j;
                                    float b = destTransformCounter;
                                    float Proportion = a / b;
                                    thistransform.localScale = Proportion * diffrenceScale + nodeStarTransform.localScale;
                                }
                                yield return new WaitForFixedUpdate();
                            }
                            break;
                        case ObjectTransformAnimationMode.P_R:
                            destTransformCounter = (int)(pathAnimationTimeList[i] / Time.fixedDeltaTime);
                            nodeStarTransform = pathAnimationTransformList[i];
                            nodeEndTransform = pathAnimationTransformList[i + 1];
                            diffrencePosition = nodeEndTransform.localPosition - nodeStarTransform.localPosition;
                            differenceRotation = nodeEndTransform.localEulerAngles - nodeStarTransform.localEulerAngles;
                            differenceRotation = new Vector3(Math_CF.CheckAngle(differenceRotation.x), Math_CF.CheckAngle(differenceRotation.y), Math_CF.CheckAngle(differenceRotation.z));
                            for (int j = 0; j < destTransformCounter; j++)
                            {
                                while (pauseGame)
                                {
                                    yield return new WaitForSeconds(waitRate);
                                }
                                if (j == 0)
                                {
                                    thistransform.localPosition = nodeStarTransform.localPosition;
                                    thistransform.localEulerAngles = nodeStarTransform.localEulerAngles;
                                   
                                }
                                else if (j == destTransformCounter - 1)
                                {
                                    thistransform.localPosition = nodeEndTransform.localPosition;
                                    thistransform.localEulerAngles = nodeEndTransform.localEulerAngles;
                                  
                                }
                                else
                                {
                                    float a = j;
                                    float b = destTransformCounter;
                                    float Proportion = a / b;
                                    thistransform.localPosition = Proportion * diffrencePosition + nodeStarTransform.localPosition;
                                    thistransform.localEulerAngles = (Proportion * differenceRotation) + nodeStarTransform.localEulerAngles;
                                }
                                yield return new WaitForFixedUpdate();
                            }
                            break;
                        case ObjectTransformAnimationMode.R_S:
                            destTransformCounter = (int)(pathAnimationTimeList[i] / Time.fixedDeltaTime);
                            nodeStarTransform = pathAnimationTransformList[i];
                            nodeEndTransform = pathAnimationTransformList[i + 1];
                            differenceRotation = nodeEndTransform.localEulerAngles - nodeStarTransform.localEulerAngles;
                            differenceRotation = new Vector3(Math_CF.CheckAngle(differenceRotation.x), Math_CF.CheckAngle(differenceRotation.y), Math_CF.CheckAngle(differenceRotation.z));
                            diffrenceScale = nodeEndTransform.localScale - nodeStarTransform.localScale;
                            for (int j = 0; j < destTransformCounter; j++)
                            {
                                while (pauseGame)
                                {
                                    yield return new WaitForSeconds(waitRate);
                                }
                                if (j == 0)
                                {
                                   
                                    thistransform.localEulerAngles = nodeStarTransform.localEulerAngles;
                                    thistransform.localScale = nodeStarTransform.localScale;
                                }
                                else if (j == destTransformCounter - 1)
                                {
                                   
                                    thistransform.localEulerAngles = nodeEndTransform.localEulerAngles;
                                    thistransform.localScale = nodeEndTransform.localScale;
                                }
                                else
                                {
                                    float a = j;
                                    float b = destTransformCounter;
                                    float Proportion = a / b;

                                    thistransform.localEulerAngles = (Proportion * differenceRotation) + nodeStarTransform.localEulerAngles;
                                    thistransform.localScale = Proportion * diffrenceScale + nodeStarTransform.localScale;
                                }
                                yield return new WaitForFixedUpdate();
                            }
                            break;
                        case ObjectTransformAnimationMode.P_S:
                            destTransformCounter = (int)(pathAnimationTimeList[i] / Time.fixedDeltaTime);
                            nodeStarTransform = pathAnimationTransformList[i];
                            nodeEndTransform = pathAnimationTransformList[i + 1];
                            diffrencePosition = nodeEndTransform.localPosition - nodeStarTransform.localPosition;
                            diffrenceScale = nodeEndTransform.localScale - nodeStarTransform.localScale;
                            for (int j = 0; j < destTransformCounter; j++)
                            {
                                while (pauseGame)
                                {
                                    yield return new WaitForSeconds(waitRate);
                                }
                                if (j == 0)
                                {
                                    thistransform.localPosition = nodeStarTransform.localPosition;
                                    thistransform.localScale = nodeStarTransform.localScale;
                                }
                                else if (j == destTransformCounter - 1)
                                {
                                    thistransform.localPosition = nodeEndTransform.localPosition;
                                    thistransform.localScale = nodeEndTransform.localScale;
                                }
                                else
                                {
                                    float a = j;
                                    float b = destTransformCounter;
                                    float Proportion = a / b;
                                    thistransform.localPosition = Proportion * diffrencePosition + nodeStarTransform.localPosition;
                                    thistransform.localScale = Proportion * diffrenceScale + nodeStarTransform.localScale;
                                }
                                yield return new WaitForFixedUpdate();
                            }
                            break;
                    }
                }
                if(animationStopMode==AnimationStopMode.Rest)
                {
                        thistransform.localPosition = recordObjPosition;
                        thistransform.localEulerAngles = recordObjRotation;
                        thistransform.localScale = recordObjScale;
                }
                break;
            case AnimationPlayMode.Loop:
                if(this.loopCount>0)
                {
                    for (int m = 0; m < loopCount; m++)
                    {
                        for (int i = 0; i < pathAnimationTransformList.Count - 1; i++)
                        {
                            Transform nodeStarTransform;
                            Transform nodeEndTransform;
                            Vector3 diffrencePosition;
                            Vector3 differenceRotation;
                            Vector3 diffrenceScale;
                            if (nodeStartEvent.ContainsKey(i))
                            {
                                nodeStartEvent[i].Invoke();
                            }
                            switch (transformAnimationMode)
                            {
                                case ObjectTransformAnimationMode.P_R_S:
                                    destTransformCounter = (int)(pathAnimationTimeList[i] / Time.fixedDeltaTime);
                                    nodeStarTransform = pathAnimationTransformList[i];
                                    nodeEndTransform = pathAnimationTransformList[i + 1];
                                    diffrencePosition = nodeEndTransform.localPosition - nodeStarTransform.localPosition;
                                    differenceRotation = nodeEndTransform.localEulerAngles - nodeStarTransform.localEulerAngles;
                                    differenceRotation = new Vector3(Math_CF.CheckAngle(differenceRotation.x), Math_CF.CheckAngle(differenceRotation.y), Math_CF.CheckAngle(differenceRotation.z));
                                    diffrenceScale = nodeEndTransform.localScale - nodeStarTransform.localScale;
                                    for (int j = 0; j < destTransformCounter; j++)
                                    {
                                        while (pauseGame)
                                        {
                                            yield return new WaitForSeconds(waitRate);
                                        }
                                        if (j == 0)
                                        {
                                            thistransform.localPosition = nodeStarTransform.localPosition;
                                            thistransform.localEulerAngles = nodeStarTransform.localEulerAngles;
                                            thistransform.localScale = nodeStarTransform.localScale;
                                        }
                                        else if (j == destTransformCounter - 1)
                                        {
                                            thistransform.localPosition = nodeEndTransform.localPosition;
                                            thistransform.localEulerAngles = nodeEndTransform.localEulerAngles;
                                            thistransform.localScale = nodeEndTransform.localScale;
                                        }
                                        else
                                        {
                                            float a = j;
                                            float b = destTransformCounter;
                                            float Proportion = a / b;
                                            thistransform.localPosition = Proportion * diffrencePosition + nodeStarTransform.localPosition;
                                            thistransform.localEulerAngles = (Proportion * differenceRotation) + nodeStarTransform.localEulerAngles;
                                            thistransform.localScale = Proportion * diffrenceScale + nodeStarTransform.localScale;
                                        }
                                        yield return new WaitForFixedUpdate();
                                    }
                                    break;
                                case ObjectTransformAnimationMode.P:
                                    destTransformCounter = (int)(pathAnimationTimeList[i] / Time.fixedDeltaTime);
                                    nodeStarTransform = pathAnimationTransformList[i];
                                    nodeEndTransform = pathAnimationTransformList[i + 1];
                                    diffrencePosition = nodeEndTransform.localPosition - nodeStarTransform.localPosition;
                                    for (int j = 0; j < destTransformCounter; j++)
                                    {
                                        while (pauseGame)
                                        {
                                            yield return new WaitForSeconds(waitRate);
                                        }
                                        if (j == 0)
                                        {
                                            thistransform.localPosition = nodeStarTransform.localPosition;
                                        }
                                        else if (j == destTransformCounter - 1)
                                        {
                                            thistransform.localPosition = nodeEndTransform.localPosition;
                                        }
                                        else
                                        {
                                            float a = j;
                                            float b = destTransformCounter;
                                            float Proportion = a / b;
                                            thistransform.localPosition = Proportion * diffrencePosition + nodeStarTransform.localPosition;
                                        }
                                        yield return new WaitForFixedUpdate();
                                    }
                                    break;
                                case ObjectTransformAnimationMode.R:
                                    destTransformCounter = (int)(pathAnimationTimeList[i] / Time.fixedDeltaTime);
                                    nodeStarTransform = pathAnimationTransformList[i];
                                    nodeEndTransform = pathAnimationTransformList[i + 1];
                                    differenceRotation = nodeEndTransform.localEulerAngles - nodeStarTransform.localEulerAngles;
                                    differenceRotation = new Vector3(Math_CF.CheckAngle(differenceRotation.x), Math_CF.CheckAngle(differenceRotation.y), Math_CF.CheckAngle(differenceRotation.z));
                                    for (int j = 0; j < destTransformCounter; j++)
                                    {
                                        while (pauseGame)
                                        {
                                            yield return new WaitForSeconds(waitRate);
                                        }
                                        if (j == 0)
                                        {
                                            thistransform.localEulerAngles = nodeStarTransform.localEulerAngles;
                                        }
                                        else if (j == destTransformCounter - 1)
                                        {
                                            thistransform.localEulerAngles = nodeEndTransform.localEulerAngles;
                                        }
                                        else
                                        {
                                            float a = j;
                                            float b = destTransformCounter;
                                            float Proportion = a / b;
                                            thistransform.localEulerAngles = (Proportion * differenceRotation) + nodeStarTransform.localEulerAngles;
                                        }
                                        yield return new WaitForFixedUpdate();
                                    }
                                    break;
                                case ObjectTransformAnimationMode.S:
                                    destTransformCounter = (int)(pathAnimationTimeList[i] / Time.fixedDeltaTime);
                                    nodeStarTransform = pathAnimationTransformList[i];
                                    nodeEndTransform = pathAnimationTransformList[i + 1];
                                    diffrenceScale = nodeEndTransform.localScale - nodeStarTransform.localScale;
                                    for (int j = 0; j < destTransformCounter; j++)
                                    {
                                        while (pauseGame)
                                        {
                                            yield return new WaitForSeconds(waitRate);
                                        }
                                        if (j == 0)
                                        {

                                            thistransform.localScale = nodeStarTransform.localScale;
                                        }
                                        else if (j == destTransformCounter - 1)
                                        {
                                           
                                            thistransform.localScale = nodeEndTransform.localScale;
                                        }
                                        else
                                        {
                                            float a = j;
                                            float b = destTransformCounter;
                                            float Proportion = a / b;
                                            

                                            thistransform.localScale = Proportion * diffrenceScale + nodeStarTransform.localScale;
                                        }
                                        yield return new WaitForFixedUpdate();
                                    }
                                    break;
                                case ObjectTransformAnimationMode.P_R:
                                    destTransformCounter = (int)(pathAnimationTimeList[i] / Time.fixedDeltaTime);
                                    nodeStarTransform = pathAnimationTransformList[i];
                                    nodeEndTransform = pathAnimationTransformList[i + 1];
                                    diffrencePosition = nodeEndTransform.localPosition - nodeStarTransform.localPosition;
                                    differenceRotation = nodeEndTransform.localEulerAngles - nodeStarTransform.localEulerAngles;
                                    differenceRotation = new Vector3(Math_CF.CheckAngle(differenceRotation.x), Math_CF.CheckAngle(differenceRotation.y), Math_CF.CheckAngle(differenceRotation.z));
                                    for (int j = 0; j < destTransformCounter; j++)
                                    {
                                        while (pauseGame)
                                        {
                                            yield return new WaitForSeconds(waitRate);
                                        }
                                        if (j == 0)
                                        {
                                            thistransform.localPosition = nodeStarTransform.localPosition;
                                            thistransform.localEulerAngles = nodeStarTransform.localEulerAngles;
                                            thistransform.localScale = nodeStarTransform.localScale;
                                        }
                                        else if (j == destTransformCounter - 1)
                                        {
                                            thistransform.localPosition = nodeEndTransform.localPosition;
                                            thistransform.localEulerAngles = nodeEndTransform.localEulerAngles;
                                            thistransform.localScale = nodeEndTransform.localScale;
                                        }
                                        else
                                        {
                                            float a = j;
                                            float b = destTransformCounter;
                                            float Proportion = a / b;
                                            thistransform.localPosition = Proportion * diffrencePosition + nodeStarTransform.localPosition;
                                            thistransform.localEulerAngles = (Proportion * differenceRotation) + nodeStarTransform.localEulerAngles;

                                        }
                                        yield return new WaitForFixedUpdate();
                                    }
                                    break;
                                case ObjectTransformAnimationMode.R_S:
                                    destTransformCounter = (int)(pathAnimationTimeList[i] / Time.fixedDeltaTime);
                                    nodeStarTransform = pathAnimationTransformList[i];
                                    nodeEndTransform = pathAnimationTransformList[i + 1];
                                    differenceRotation = nodeEndTransform.localEulerAngles - nodeStarTransform.localEulerAngles;
                                    differenceRotation = new Vector3(Math_CF.CheckAngle(differenceRotation.x), Math_CF.CheckAngle(differenceRotation.y), Math_CF.CheckAngle(differenceRotation.z));
                                    diffrenceScale = nodeEndTransform.localScale - nodeStarTransform.localScale;
                                    for (int j = 0; j < destTransformCounter; j++)
                                    {
                                        while (pauseGame)
                                        {
                                            yield return new WaitForSeconds(waitRate);
                                        }
                                        if (j == 0)
                                        {
                                           
                                            thistransform.localEulerAngles = nodeStarTransform.localEulerAngles;
                                            thistransform.localScale = nodeStarTransform.localScale;
                                        }
                                        else if (j == destTransformCounter - 1)
                                        {
                                           
                                            thistransform.localEulerAngles = nodeEndTransform.localEulerAngles;
                                            thistransform.localScale = nodeEndTransform.localScale;
                                        }
                                        else
                                        {
                                            float a = j;
                                            float b = destTransformCounter;
                                            float Proportion = a / b;

                                            thistransform.localEulerAngles = (Proportion * differenceRotation) + nodeStarTransform.localEulerAngles;
                                            thistransform.localScale = Proportion * diffrenceScale + nodeStarTransform.localScale;
                                        }
                                        yield return new WaitForFixedUpdate();
                                    }
                                    break;
                                case ObjectTransformAnimationMode.P_S:
                                    destTransformCounter = (int)(pathAnimationTimeList[i] / Time.fixedDeltaTime);
                                    nodeStarTransform = pathAnimationTransformList[i];
                                    nodeEndTransform = pathAnimationTransformList[i + 1];
                                    diffrencePosition = nodeEndTransform.localPosition - nodeStarTransform.localPosition;
                                    diffrenceScale = nodeEndTransform.localScale - nodeStarTransform.localScale;
                                    for (int j = 0; j < destTransformCounter; j++)
                                    {
                                        while (pauseGame)
                                        {
                                            yield return new WaitForSeconds(waitRate);
                                        }
                                        if (j == 0)
                                        {
                                            thistransform.localPosition = nodeStarTransform.localPosition;
                                            thistransform.localScale = nodeStarTransform.localScale;
                                        }
                                        else if (j == destTransformCounter - 1)
                                        {
                                            thistransform.localPosition = nodeEndTransform.localPosition;

                                            thistransform.localScale = nodeEndTransform.localScale;
                                        }
                                        else
                                        {
                                            float a = j;
                                            float b = destTransformCounter;
                                            float Proportion = a / b;
                                            thistransform.localPosition = Proportion * diffrencePosition + nodeStarTransform.localPosition;

                                            thistransform.localScale = Proportion * diffrenceScale + nodeStarTransform.localScale;
                                        }
                                        yield return new WaitForFixedUpdate();
                                    }
                                    break;
                            }
                        }
                    }
                }
                else
                {
                    while(loopCount<0)
                    {
                        for (int i = 0; i < pathAnimationTransformList.Count - 1; i++)
                        {
                            Transform nodeStarTransform;
                            Transform nodeEndTransform;
                            Vector3 diffrencePosition;
                            Vector3 differenceRotation;
                            Vector3 diffrenceScale;
                            if (nodeStartEvent.ContainsKey(i))
                            {
                                nodeStartEvent[i].Invoke();
                            }
                            switch (transformAnimationMode)
                            {
                                case ObjectTransformAnimationMode.P_R_S:
                                    destTransformCounter = (int)(pathAnimationTimeList[i] / Time.fixedDeltaTime);
                                    nodeStarTransform = pathAnimationTransformList[i];
                                    nodeEndTransform = pathAnimationTransformList[i + 1];
                                    diffrencePosition = nodeEndTransform.localPosition - nodeStarTransform.localPosition;
                                    differenceRotation = nodeEndTransform.localEulerAngles - nodeStarTransform.localEulerAngles;
                                    differenceRotation = new Vector3(Math_CF.CheckAngle(differenceRotation.x), Math_CF.CheckAngle(differenceRotation.y), Math_CF.CheckAngle(differenceRotation.z));
                                    diffrenceScale = nodeEndTransform.localScale - nodeStarTransform.localScale;
                                    for (int j = 0; j < destTransformCounter; j++)
                                    {
                                        while (pauseGame)
                                        {
                                            yield return new WaitForSeconds(waitRate);
                                        }
                                        if (j == 0)
                                        {
                                            thistransform.localPosition = nodeStarTransform.localPosition;
                                            thistransform.localEulerAngles = nodeStarTransform.localEulerAngles;
                                            thistransform.localScale = nodeStarTransform.localScale;
                                        }
                                        else if (j == destTransformCounter - 1)
                                        {
                                            thistransform.localPosition = nodeEndTransform.localPosition;
                                            thistransform.localEulerAngles = nodeEndTransform.localEulerAngles;
                                            thistransform.localScale = nodeEndTransform.localScale;
                                        }
                                        else
                                        {
                                            float a = j;
                                            float b = destTransformCounter;
                                            float Proportion = a / b;
                                            thistransform.localPosition = Proportion * diffrencePosition + nodeStarTransform.localPosition;
                                            thistransform.localEulerAngles = (Proportion * differenceRotation) + nodeStarTransform.localEulerAngles;
                                            thistransform.localScale = Proportion * diffrenceScale + nodeStarTransform.localScale;
                                        }
                                        yield return new WaitForFixedUpdate();
                                    }
                                    break;
                                case ObjectTransformAnimationMode.P:
                                    destTransformCounter = (int)(pathAnimationTimeList[i] / Time.fixedDeltaTime);
                                    nodeStarTransform = pathAnimationTransformList[i];
                                    nodeEndTransform = pathAnimationTransformList[i + 1];
                                    diffrencePosition = nodeEndTransform.localPosition - nodeStarTransform.localPosition;
                                    for (int j = 0; j < destTransformCounter; j++)
                                    {
                                        while (pauseGame)
                                        {
                                            yield return new WaitForSeconds(waitRate);
                                        }
                                        if (j == 0)
                                        {
                                            thistransform.localPosition = nodeStarTransform.localPosition;
                                            
                                        }
                                        else if (j == destTransformCounter - 1)
                                        {
                                            thistransform.localPosition = nodeEndTransform.localPosition;
                                          
                                        }
                                        else
                                        {
                                            float a = j;
                                            float b = destTransformCounter;
                                            float Proportion = a / b;
                                            thistransform.localPosition = Proportion * diffrencePosition + nodeStarTransform.localPosition;
                                           
                                        }
                                        yield return new WaitForFixedUpdate();
                                    }
                                    break;
                                case ObjectTransformAnimationMode.R:
                                    destTransformCounter = (int)(pathAnimationTimeList[i] / Time.fixedDeltaTime);
                                    nodeStarTransform = pathAnimationTransformList[i];
                                    nodeEndTransform = pathAnimationTransformList[i + 1];
                                    differenceRotation = nodeEndTransform.localEulerAngles - nodeStarTransform.localEulerAngles;
                                    differenceRotation = new Vector3(Math_CF.CheckAngle(differenceRotation.x), Math_CF.CheckAngle(differenceRotation.y), Math_CF.CheckAngle(differenceRotation.z));
                                    for (int j = 0; j < destTransformCounter; j++)
                                    {
                                        while (pauseGame)
                                        {
                                            yield return new WaitForSeconds(waitRate);
                                        }
                                        if (j == 0)
                                        {
                                           
                                            thistransform.localEulerAngles = nodeStarTransform.localEulerAngles;
                                          
                                        }
                                        else if (j == destTransformCounter - 1)
                                        {
                                           
                                            thistransform.localEulerAngles = nodeEndTransform.localEulerAngles;
                                           
                                        }
                                        else
                                        {
                                            float a = j;
                                            float b = destTransformCounter;
                                            float Proportion = a / b;
                                            
                                            thistransform.localEulerAngles = (Proportion * differenceRotation) + nodeStarTransform.localEulerAngles;

                                        }
                                        yield return new WaitForFixedUpdate();
                                    }
                                    break;
                                case ObjectTransformAnimationMode.S:
                                    destTransformCounter = (int)(pathAnimationTimeList[i] / Time.fixedDeltaTime);
                                    nodeStarTransform = pathAnimationTransformList[i];
                                    nodeEndTransform = pathAnimationTransformList[i + 1];
                                    diffrenceScale = nodeEndTransform.localScale - nodeStarTransform.localScale;
                                    for (int j = 0; j < destTransformCounter; j++)
                                    {
                                        while (pauseGame)
                                        {
                                            yield return new WaitForSeconds(waitRate);
                                        }
                                        if (j == 0)
                                        {

                                            
                                            thistransform.localScale = nodeStarTransform.localScale;
                                        }
                                        else if (j == destTransformCounter - 1)
                                        {
 
                                        
                                            thistransform.localScale = nodeEndTransform.localScale;
                                        }
                                        else
                                        {
                                            float a = j;
                                            float b = destTransformCounter;
                                            float Proportion = a / b;

                                           
                                            thistransform.localScale = Proportion * diffrenceScale + nodeStarTransform.localScale;
                                        }
                                        yield return new WaitForFixedUpdate();
                                    }
                                    break;
                                case ObjectTransformAnimationMode.P_R:
                                    destTransformCounter = (int)(pathAnimationTimeList[i] / Time.fixedDeltaTime);
                                    nodeStarTransform = pathAnimationTransformList[i];
                                    nodeEndTransform = pathAnimationTransformList[i + 1];
                                    diffrencePosition = nodeEndTransform.localPosition - nodeStarTransform.localPosition;
                                    differenceRotation = nodeEndTransform.localEulerAngles - nodeStarTransform.localEulerAngles;
                                    differenceRotation = new Vector3(Math_CF.CheckAngle(differenceRotation.x), Math_CF.CheckAngle(differenceRotation.y), Math_CF.CheckAngle(differenceRotation.z));
                                    for (int j = 0; j < destTransformCounter; j++)
                                    {
                                        while (pauseGame)
                                        {
                                            yield return new WaitForSeconds(waitRate);
                                        }
                                        if (j == 0)
                                        {
                                            thistransform.localPosition = nodeStarTransform.localPosition;
                                            thistransform.localEulerAngles = nodeStarTransform.localEulerAngles;
                                            
                                        }
                                        else if (j == destTransformCounter - 1)
                                        {
                                            thistransform.localPosition = nodeEndTransform.localPosition;
                                            thistransform.localEulerAngles = nodeEndTransform.localEulerAngles;
                                       
                                        }
                                        else
                                        {
                                            float a = j;
                                            float b = destTransformCounter;
                                            float Proportion = a / b;
                                            thistransform.localPosition = Proportion * diffrencePosition + nodeStarTransform.localPosition;
                                            thistransform.localEulerAngles = (Proportion * differenceRotation) + nodeStarTransform.localEulerAngles;

                                        }
                                        yield return new WaitForFixedUpdate();
                                    }
                                    break;
                                case ObjectTransformAnimationMode.R_S:
                                    destTransformCounter = (int)(pathAnimationTimeList[i] / Time.fixedDeltaTime);
                                    nodeStarTransform = pathAnimationTransformList[i];
                                    nodeEndTransform = pathAnimationTransformList[i + 1];
                                    differenceRotation = nodeEndTransform.localEulerAngles - nodeStarTransform.localEulerAngles;
                                    differenceRotation = new Vector3(Math_CF.CheckAngle(differenceRotation.x), Math_CF.CheckAngle(differenceRotation.y), Math_CF.CheckAngle(differenceRotation.z));
                                    diffrenceScale = nodeEndTransform.localScale - nodeStarTransform.localScale;
                                    for (int j = 0; j < destTransformCounter; j++)
                                    {
                                        while (pauseGame)
                                        {
                                            yield return new WaitForSeconds(waitRate);
                                        }
                                        if (j == 0)
                                        {
                                          
                                            thistransform.localEulerAngles = nodeStarTransform.localEulerAngles;
                                            thistransform.localScale = nodeStarTransform.localScale;
                                        }
                                        else if (j == destTransformCounter - 1)
                                        {
                                            
                                            thistransform.localEulerAngles = nodeEndTransform.localEulerAngles;
                                            thistransform.localScale = nodeEndTransform.localScale;
                                        }
                                        else
                                        {
                                            float a = j;
                                            float b = destTransformCounter;
                                            float Proportion = a / b;

                                            thistransform.localEulerAngles = (Proportion * differenceRotation) + nodeStarTransform.localEulerAngles;
                                            thistransform.localScale = Proportion * diffrenceScale + nodeStarTransform.localScale;
                                        }
                                        yield return new WaitForFixedUpdate();
                                    }
                                    break;
                                case ObjectTransformAnimationMode.P_S:
                                    destTransformCounter = (int)(pathAnimationTimeList[i] / Time.fixedDeltaTime);
                                    nodeStarTransform = pathAnimationTransformList[i];
                                    nodeEndTransform = pathAnimationTransformList[i + 1];
                                    diffrencePosition = nodeEndTransform.localPosition - nodeStarTransform.localPosition;
                                    diffrenceScale = nodeEndTransform.localScale - nodeStarTransform.localScale;
                                    for (int j = 0; j < destTransformCounter; j++)
                                    {
                                        while (pauseGame)
                                        {
                                            yield return new WaitForSeconds(waitRate);
                                        }
                                        if (j == 0)
                                        {
                                            thistransform.localPosition = nodeStarTransform.localPosition;
                                           
                                            thistransform.localScale = nodeStarTransform.localScale;
                                        }
                                        else if (j == destTransformCounter - 1)
                                        {
                                            thistransform.localPosition = nodeEndTransform.localPosition;
                                            
                                            thistransform.localScale = nodeEndTransform.localScale;
                                        }
                                        else
                                        {
                                            float a = j;
                                            float b = destTransformCounter;
                                            float Proportion = a / b;
                                            thistransform.localPosition = Proportion * diffrencePosition + nodeStarTransform.localPosition;

                                            thistransform.localScale = Proportion * diffrenceScale + nodeStarTransform.localScale;
                                        }
                                        yield return new WaitForFixedUpdate();
                                    }
                                    break;
                            }
                        }
                    }
                }
                if (animationStopMode == AnimationStopMode.Rest)
                {
                    thistransform.localPosition = recordObjPosition;
                    thistransform.localEulerAngles = recordObjRotation;
                    thistransform.localScale = recordObjScale;
                }
                break;
            case AnimationPlayMode.PingPong:
                if(this.loopCount>0)
                {
                    for (int m = 0; m < loopCount; m++)
                    {
                        for (int i = 0; i < pathAnimationTransformList.Count - 1; i++)
                        {
                            Transform nodeStarTransform;
                            Transform nodeEndTransform;
                            Vector3 diffrencePosition;
                            Vector3 differenceRotation;
                            Vector3 diffrenceScale;
                            if (nodeStartEvent.ContainsKey(i))
                            {
                                nodeStartEvent[i].Invoke();
                            }
                            switch (transformAnimationMode)
                            {
                                case ObjectTransformAnimationMode.P_R_S:
                                    destTransformCounter = (int)(pathAnimationTimeList[i] / Time.fixedDeltaTime);
                                    nodeStarTransform = pathAnimationTransformList[i];
                                    nodeEndTransform = pathAnimationTransformList[i + 1];
                                    diffrencePosition = nodeEndTransform.localPosition - nodeStarTransform.localPosition;
                                    differenceRotation = nodeEndTransform.localEulerAngles - nodeStarTransform.localEulerAngles;
                                    differenceRotation = new Vector3(Math_CF.CheckAngle(differenceRotation.x), Math_CF.CheckAngle(differenceRotation.y), Math_CF.CheckAngle(differenceRotation.z));
                                    diffrenceScale = nodeEndTransform.localScale - nodeStarTransform.localScale;
                                    for (int j = 0; j < destTransformCounter; j++)
                                    {
                                        while (pauseGame)
                                        {
                                            yield return new WaitForSeconds(waitRate);
                                        }
                                        if (j == 0)
                                        {
                                            thistransform.localPosition = nodeStarTransform.localPosition;
                                            thistransform.localEulerAngles = nodeStarTransform.localEulerAngles;
                                            thistransform.localScale = nodeStarTransform.localScale;
                                        }
                                        else if (j == destTransformCounter - 1)
                                        {
                                            thistransform.localPosition = nodeEndTransform.localPosition;
                                            thistransform.localEulerAngles = nodeEndTransform.localEulerAngles;
                                            thistransform.localScale = nodeEndTransform.localScale;
                                        }
                                        else
                                        {
                                            float a = j;
                                            float b = destTransformCounter;
                                            float Proportion = a / b;
                                            thistransform.localPosition = Proportion * diffrencePosition + nodeStarTransform.localPosition;
                                            thistransform.localEulerAngles = (Proportion * differenceRotation) + nodeStarTransform.localEulerAngles;
                                            thistransform.localScale = Proportion * diffrenceScale + nodeStarTransform.localScale;
                                        }
                                        yield return new WaitForFixedUpdate();
                                    }
                                    break;
                                case ObjectTransformAnimationMode.P:
                                    destTransformCounter = (int)(pathAnimationTimeList[i] / Time.fixedDeltaTime);
                                    nodeStarTransform = pathAnimationTransformList[i];
                                    nodeEndTransform = pathAnimationTransformList[i + 1];
                                    diffrencePosition = nodeEndTransform.localPosition - nodeStarTransform.localPosition;
                                    for (int j = 0; j < destTransformCounter; j++)
                                    {
                                        while (pauseGame)
                                        {
                                            yield return new WaitForSeconds(waitRate);
                                        }
                                        if (j == 0)
                                        {
                                            thistransform.localPosition = nodeStarTransform.localPosition;
                                            thistransform.localEulerAngles = nodeStarTransform.localEulerAngles;
                                            thistransform.localScale = nodeStarTransform.localScale;
                                        }
                                        else if (j == destTransformCounter - 1)
                                        {
                                            thistransform.localPosition = nodeEndTransform.localPosition;
                                            thistransform.localEulerAngles = nodeEndTransform.localEulerAngles;
                                            thistransform.localScale = nodeEndTransform.localScale;
                                        }
                                        else
                                        {
                                            float a = j;
                                            float b = destTransformCounter;
                                            float Proportion = a / b;
                                            thistransform.localPosition = Proportion * diffrencePosition + nodeStarTransform.localPosition;
                                            
                                        }
                                        yield return new WaitForFixedUpdate();
                                    }
                                    break;
                                case ObjectTransformAnimationMode.R:
                                    destTransformCounter = (int)(pathAnimationTimeList[i] / Time.fixedDeltaTime);
                                    nodeStarTransform = pathAnimationTransformList[i];
                                    nodeEndTransform = pathAnimationTransformList[i + 1];
                                    differenceRotation = nodeEndTransform.localEulerAngles - nodeStarTransform.localEulerAngles;
                                    differenceRotation = new Vector3(Math_CF.CheckAngle(differenceRotation.x), Math_CF.CheckAngle(differenceRotation.y), Math_CF.CheckAngle(differenceRotation.z));
                                    for (int j = 0; j < destTransformCounter; j++)
                                    {
                                        while (pauseGame)
                                        {
                                            yield return new WaitForSeconds(waitRate);
                                        }
                                        if (j == 0)
                                        {
                                           
                                            thistransform.localEulerAngles = nodeStarTransform.localEulerAngles;
                                           
                                        }
                                        else if (j == destTransformCounter - 1)
                                        {
                                           
                                            thistransform.localEulerAngles = nodeEndTransform.localEulerAngles;
                                          
                                        }
                                        else
                                        {
                                            float a = j;
                                            float b = destTransformCounter;
                                            float Proportion = a / b;
                                            
                                            thistransform.localEulerAngles = (Proportion * differenceRotation) + nodeStarTransform.localEulerAngles;

                                        }
                                        yield return new WaitForFixedUpdate();
                                    }
                                    break;
                                case ObjectTransformAnimationMode.S:
                                    destTransformCounter = (int)(pathAnimationTimeList[i] / Time.fixedDeltaTime);
                                    nodeStarTransform = pathAnimationTransformList[i];
                                    nodeEndTransform = pathAnimationTransformList[i + 1];
                                    diffrenceScale = nodeEndTransform.localScale - nodeStarTransform.localScale;
                                    for (int j = 0; j < destTransformCounter; j++)
                                    {
                                        while (pauseGame)
                                        {
                                            yield return new WaitForSeconds(waitRate);
                                        }
                                        if (j == 0)
                                        {
                                            
                                            thistransform.localScale = nodeStarTransform.localScale;
                                        }
                                        else if (j == destTransformCounter - 1)
                                        {
                                            
                                            thistransform.localScale = nodeEndTransform.localScale;
                                        }
                                        else
                                        {
                                            float a = j;
                                            float b = destTransformCounter;
                                            float Proportion = a / b;
                                            thistransform.localScale = Proportion * diffrenceScale + nodeStarTransform.localScale;
                                        }
                                        yield return new WaitForFixedUpdate();
                                    }
                                    break;
                                case ObjectTransformAnimationMode.P_R:
                                    destTransformCounter = (int)(pathAnimationTimeList[i] / Time.fixedDeltaTime);
                                    nodeStarTransform = pathAnimationTransformList[i];
                                    nodeEndTransform = pathAnimationTransformList[i + 1];
                                    diffrencePosition = nodeEndTransform.localPosition - nodeStarTransform.localPosition;
                                    differenceRotation = nodeEndTransform.localEulerAngles - nodeStarTransform.localEulerAngles;
                                    differenceRotation = new Vector3(Math_CF.CheckAngle(differenceRotation.x), Math_CF.CheckAngle(differenceRotation.y), Math_CF.CheckAngle(differenceRotation.z));
                                    for (int j = 0; j < destTransformCounter; j++)
                                    {
                                        while (pauseGame)
                                        {
                                            yield return new WaitForSeconds(waitRate);
                                        }
                                        if (j == 0)
                                        {
                                            thistransform.localPosition = nodeStarTransform.localPosition;
                                            thistransform.localEulerAngles = nodeStarTransform.localEulerAngles;
                                           
                                        }
                                        else if (j == destTransformCounter - 1)
                                        {
                                            thistransform.localPosition = nodeEndTransform.localPosition;
                                            thistransform.localEulerAngles = nodeEndTransform.localEulerAngles;
                                           
                                        }
                                        else
                                        {
                                            float a = j;
                                            float b = destTransformCounter;
                                            float Proportion = a / b;
                                            thistransform.localPosition = Proportion * diffrencePosition + nodeStarTransform.localPosition;
                                            thistransform.localEulerAngles = (Proportion * differenceRotation) + nodeStarTransform.localEulerAngles;

                                        }
                                        yield return new WaitForFixedUpdate();
                                    }
                                    break;
                                case ObjectTransformAnimationMode.R_S:
                                    destTransformCounter = (int)(pathAnimationTimeList[i] / Time.fixedDeltaTime);
                                    nodeStarTransform = pathAnimationTransformList[i];
                                    nodeEndTransform = pathAnimationTransformList[i + 1];
                                    differenceRotation = nodeEndTransform.localEulerAngles - nodeStarTransform.localEulerAngles;
                                    differenceRotation = new Vector3(Math_CF.CheckAngle(differenceRotation.x), Math_CF.CheckAngle(differenceRotation.y), Math_CF.CheckAngle(differenceRotation.z));
                                    diffrenceScale = nodeEndTransform.localScale - nodeStarTransform.localScale;
                                    for (int j = 0; j < destTransformCounter; j++)
                                    {
                                        while (pauseGame)
                                        {
                                            yield return new WaitForSeconds(waitRate);
                                        }
                                        if (j == 0)
                                        {
                                           
                                            thistransform.localEulerAngles = nodeStarTransform.localEulerAngles;
                                            thistransform.localScale = nodeStarTransform.localScale;
                                        }
                                        else if (j == destTransformCounter - 1)
                                        {
                                            
                                            thistransform.localEulerAngles = nodeEndTransform.localEulerAngles;
                                            thistransform.localScale = nodeEndTransform.localScale;
                                        }
                                        else
                                        {
                                            float a = j;
                                            float b = destTransformCounter;
                                            float Proportion = a / b;

                                            thistransform.localEulerAngles = (Proportion * differenceRotation) + nodeStarTransform.localEulerAngles;
                                            thistransform.localScale = Proportion * diffrenceScale + nodeStarTransform.localScale;
                                        }
                                        yield return new WaitForFixedUpdate();
                                    }
                                    break;
                                case ObjectTransformAnimationMode.P_S:
                                    destTransformCounter = (int)(pathAnimationTimeList[i] / Time.fixedDeltaTime);
                                    nodeStarTransform = pathAnimationTransformList[i];
                                    nodeEndTransform = pathAnimationTransformList[i + 1];
                                    diffrencePosition = nodeEndTransform.localPosition - nodeStarTransform.localPosition;
                                    diffrenceScale = nodeEndTransform.localScale - nodeStarTransform.localScale;
                                    for (int j = 0; j < destTransformCounter; j++)
                                    {
                                        while (pauseGame)
                                        {
                                            yield return new WaitForSeconds(waitRate);
                                        }
                                        if (j == 0)
                                        {
                                            thistransform.localPosition = nodeStarTransform.localPosition;
                                           
                                            thistransform.localScale = nodeStarTransform.localScale;
                                        }
                                        else if (j == destTransformCounter - 1)
                                        {
                                            thistransform.localPosition = nodeEndTransform.localPosition;
                                           
                                            thistransform.localScale = nodeEndTransform.localScale;
                                        }
                                        else
                                        {
                                            float a = j;
                                            float b = destTransformCounter;
                                            float Proportion = a / b;
                                            thistransform.localPosition = Proportion * diffrencePosition + nodeStarTransform.localPosition;

                                            thistransform.localScale = Proportion * diffrenceScale + nodeStarTransform.localScale;
                                        }
                                        yield return new WaitForFixedUpdate();
                                    }
                                    break;
                            }
                        }
                    }
                }
                else
                {
                    while(loopCount<0)
                    {
                        for (int i = 0; i < pathAnimationTransformList.Count - 1; i++)
                        {
                            Transform nodeStarTransform;
                            Transform nodeEndTransform;
                            Vector3 diffrencePosition;
                            Vector3 differenceRotation;
                            Vector3 diffrenceScale;
                            if (nodeStartEvent.ContainsKey(i))
                            {
                                nodeStartEvent[i].Invoke();
                            }
                            switch (transformAnimationMode)
                            {
                                case ObjectTransformAnimationMode.P_R_S:
                                    destTransformCounter = (int)(pathAnimationTimeList[i] / Time.fixedDeltaTime);
                                    nodeStarTransform = pathAnimationTransformList[i];
                                    nodeEndTransform = pathAnimationTransformList[i + 1];
                                    diffrencePosition = nodeEndTransform.localPosition - nodeStarTransform.localPosition;
                                    differenceRotation = nodeEndTransform.localEulerAngles - nodeStarTransform.localEulerAngles;
                                    differenceRotation = new Vector3(Math_CF.CheckAngle(differenceRotation.x), Math_CF.CheckAngle(differenceRotation.y), Math_CF.CheckAngle(differenceRotation.z));
                                    diffrenceScale = nodeEndTransform.localScale - nodeStarTransform.localScale;
                                    for (int j = 0; j < destTransformCounter; j++)
                                    {
                                        while (pauseGame)
                                        {
                                            yield return new WaitForSeconds(waitRate);
                                        }
                                        if (j == 0)
                                        {
                                            thistransform.localPosition = nodeStarTransform.localPosition;
                                            thistransform.localEulerAngles = nodeStarTransform.localEulerAngles;
                                            thistransform.localScale = nodeStarTransform.localScale;
                                        }
                                        else if (j == destTransformCounter - 1)
                                        {
                                            thistransform.localPosition = nodeEndTransform.localPosition;
                                            thistransform.localEulerAngles = nodeEndTransform.localEulerAngles;
                                            thistransform.localScale = nodeEndTransform.localScale;
                                        }
                                        else
                                        {
                                            float a = j;
                                            float b = destTransformCounter;
                                            float Proportion = a / b;
                                            thistransform.localPosition = Proportion * diffrencePosition + nodeStarTransform.localPosition;
                                            thistransform.localEulerAngles = (Proportion * differenceRotation) + nodeStarTransform.localEulerAngles;
                                            thistransform.localScale = Proportion * diffrenceScale + nodeStarTransform.localScale;
                                        }
                                        yield return new WaitForFixedUpdate();
                                    }
                                    break;
                                case ObjectTransformAnimationMode.P:
                                    destTransformCounter = (int)(pathAnimationTimeList[i] / Time.fixedDeltaTime);
                                    nodeStarTransform = pathAnimationTransformList[i];
                                    nodeEndTransform = pathAnimationTransformList[i + 1];
                                    diffrencePosition = nodeEndTransform.localPosition - nodeStarTransform.localPosition;
                                    for (int j = 0; j < destTransformCounter; j++)
                                    {
                                        while (pauseGame)
                                        {
                                            yield return new WaitForSeconds(waitRate);
                                        }
                                        if (j == 0)
                                        {
                                            thistransform.localPosition = nodeStarTransform.localPosition;
                                            
                                        }
                                        else if (j == destTransformCounter - 1)
                                        {
                                            thistransform.localPosition = nodeEndTransform.localPosition;
                                           
                                        }
                                        else
                                        {
                                            float a = j;
                                            float b = destTransformCounter;
                                            float Proportion = a / b;
                                            thistransform.localPosition = Proportion * diffrencePosition + nodeStarTransform.localPosition;
                                            
                                        }
                                        yield return new WaitForFixedUpdate();
                                    }
                                    break;
                                case ObjectTransformAnimationMode.R:
                                    destTransformCounter = (int)(pathAnimationTimeList[i] / Time.fixedDeltaTime);
                                    nodeStarTransform = pathAnimationTransformList[i];
                                    nodeEndTransform = pathAnimationTransformList[i + 1];
                                    differenceRotation = nodeEndTransform.localEulerAngles - nodeStarTransform.localEulerAngles;
                                    differenceRotation = new Vector3(Math_CF.CheckAngle(differenceRotation.x), Math_CF.CheckAngle(differenceRotation.y), Math_CF.CheckAngle(differenceRotation.z));
                                    for (int j = 0; j < destTransformCounter; j++)
                                    {
                                        while (pauseGame)
                                        {
                                            yield return new WaitForSeconds(waitRate);
                                        }
                                        if (j == 0)
                                        {
                                            
                                            thistransform.localEulerAngles = nodeStarTransform.localEulerAngles;
                                           
                                        }
                                        else if (j == destTransformCounter - 1)
                                        {
                                            
                                            thistransform.localEulerAngles = nodeEndTransform.localEulerAngles;
                                          
                                        }
                                        else
                                        {
                                            float a = j;
                                            float b = destTransformCounter;
                                            float Proportion = a / b;

                                            thistransform.localEulerAngles = (Proportion * differenceRotation) + nodeStarTransform.localEulerAngles;
                                           
                                        }
                                        yield return new WaitForFixedUpdate();
                                    }
                                    break;
                                case ObjectTransformAnimationMode.S:
                                    destTransformCounter = (int)(pathAnimationTimeList[i] / Time.fixedDeltaTime);
                                    nodeStarTransform = pathAnimationTransformList[i];
                                    nodeEndTransform = pathAnimationTransformList[i + 1];
                                    diffrenceScale = nodeEndTransform.localScale - nodeStarTransform.localScale;
                                    for (int j = 0; j < destTransformCounter; j++)
                                    {
                                        while (pauseGame)
                                        {
                                            yield return new WaitForSeconds(waitRate);
                                        }
                                        if (j == 0)
                                        {
                                            
                                            thistransform.localScale = nodeStarTransform.localScale;
                                        }
                                        else if (j == destTransformCounter - 1)
                                        {
                                           
                                            thistransform.localScale = nodeEndTransform.localScale;
                                        }
                                        else
                                        {
                                            float a = j;
                                            float b = destTransformCounter;
                                            float Proportion = a / b;
                                            
                                            thistransform.localScale = Proportion * diffrenceScale + nodeStarTransform.localScale;
                                        }
                                        yield return new WaitForFixedUpdate();
                                    }
                                    break;
                                case ObjectTransformAnimationMode.P_R:
                                    destTransformCounter = (int)(pathAnimationTimeList[i] / Time.fixedDeltaTime);
                                    nodeStarTransform = pathAnimationTransformList[i];
                                    nodeEndTransform = pathAnimationTransformList[i + 1];
                                    diffrencePosition = nodeEndTransform.localPosition - nodeStarTransform.localPosition;
                                    differenceRotation = nodeEndTransform.localEulerAngles - nodeStarTransform.localEulerAngles;
                                    differenceRotation = new Vector3(Math_CF.CheckAngle(differenceRotation.x), Math_CF.CheckAngle(differenceRotation.y), Math_CF.CheckAngle(differenceRotation.z));
                                    for (int j = 0; j < destTransformCounter; j++)
                                    {
                                        while (pauseGame)
                                        {
                                            yield return new WaitForSeconds(waitRate);
                                        }
                                        if (j == 0)
                                        {
                                            thistransform.localPosition = nodeStarTransform.localPosition;
                                            thistransform.localEulerAngles = nodeStarTransform.localEulerAngles;
                                          
                                        }
                                        else if (j == destTransformCounter - 1)
                                        {
                                            thistransform.localPosition = nodeEndTransform.localPosition;
                                            thistransform.localEulerAngles = nodeEndTransform.localEulerAngles;
                                            
                                        }
                                        else
                                        {
                                            float a = j;
                                            float b = destTransformCounter;
                                            float Proportion = a / b;
                                            thistransform.localPosition = Proportion * diffrencePosition + nodeStarTransform.localPosition;
                                            thistransform.localEulerAngles = (Proportion * differenceRotation) + nodeStarTransform.localEulerAngles;
                                            
                                        }
                                        yield return new WaitForFixedUpdate();
                                    }
                                    break;
                                case ObjectTransformAnimationMode.R_S:
                                    destTransformCounter = (int)(pathAnimationTimeList[i] / Time.fixedDeltaTime);
                                    nodeStarTransform = pathAnimationTransformList[i];
                                    nodeEndTransform = pathAnimationTransformList[i + 1];
                                    differenceRotation = nodeEndTransform.localEulerAngles - nodeStarTransform.localEulerAngles;
                                    differenceRotation = new Vector3(Math_CF.CheckAngle(differenceRotation.x), Math_CF.CheckAngle(differenceRotation.y), Math_CF.CheckAngle(differenceRotation.z));
                                    diffrenceScale = nodeEndTransform.localScale - nodeStarTransform.localScale;
                                    for (int j = 0; j < destTransformCounter; j++)
                                    {
                                        while (pauseGame)
                                        {
                                            yield return new WaitForSeconds(waitRate);
                                        }
                                        if (j == 0)
                                        {
                                           
                                            thistransform.localEulerAngles = nodeStarTransform.localEulerAngles;
                                            thistransform.localScale = nodeStarTransform.localScale;
                                        }
                                        else if (j == destTransformCounter - 1)
                                        {
                                           
                                            thistransform.localEulerAngles = nodeEndTransform.localEulerAngles;
                                            thistransform.localScale = nodeEndTransform.localScale;
                                        }
                                        else
                                        {
                                            float a = j;
                                            float b = destTransformCounter;
                                            float Proportion = a / b;

                                            thistransform.localEulerAngles = (Proportion * differenceRotation) + nodeStarTransform.localEulerAngles;
                                            thistransform.localScale = Proportion * diffrenceScale + nodeStarTransform.localScale;
                                        }
                                        yield return new WaitForFixedUpdate();
                                    }
                                    break;
                                case ObjectTransformAnimationMode.P_S:
                                    destTransformCounter = (int)(pathAnimationTimeList[i] / Time.fixedDeltaTime);
                                    nodeStarTransform = pathAnimationTransformList[i];
                                    nodeEndTransform = pathAnimationTransformList[i + 1];
                                    diffrencePosition = nodeEndTransform.localPosition - nodeStarTransform.localPosition;
                                    diffrenceScale = nodeEndTransform.localScale - nodeStarTransform.localScale;
                                    for (int j = 0; j < destTransformCounter; j++)
                                    {
                                        while (pauseGame)
                                        {
                                            yield return new WaitForSeconds(waitRate);
                                        }
                                        if (j == 0)
                                        {
                                            thistransform.localPosition = nodeStarTransform.localPosition;
                                         
                                            thistransform.localScale = nodeStarTransform.localScale;
                                        }
                                        else if (j == destTransformCounter - 1)
                                        {
                                            thistransform.localPosition = nodeEndTransform.localPosition;
                                          
                                            thistransform.localScale = nodeEndTransform.localScale;
                                        }
                                        else
                                        {
                                            float a = j;
                                            float b = destTransformCounter;
                                            float Proportion = a / b;
                                            thistransform.localPosition = Proportion * diffrencePosition + nodeStarTransform.localPosition;

                                            thistransform.localScale = Proportion * diffrenceScale + nodeStarTransform.localScale;
                                        }
                                        yield return new WaitForFixedUpdate();
                                    }
                                    break;
                            }
                        }
                    }
                }
                if (animationStopMode == AnimationStopMode.Rest)
                {
                    thistransform.localPosition = recordObjPosition;
                    thistransform.localEulerAngles = recordObjRotation;
                    thistransform.localScale = recordObjScale;
                }
                break;
        }
        CompletePathAnimation();

    }


}

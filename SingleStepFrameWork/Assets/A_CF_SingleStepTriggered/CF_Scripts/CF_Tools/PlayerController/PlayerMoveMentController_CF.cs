using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public enum MovementMode
{
    CanMoveCanLook,
    CanMoveNotLook,
    NotMoveCanLook,
    NotMoveNotLook
}

public class PlayerMoveMentController_CF : MonoBehaviour{

    /// <summary>
    /// 玩家控制器躯干tansform
    /// </summary>
     Transform thistransform;
    public Transform ThisTransform
    {
        get { return thistransform; }
    }

    /// <summary>
    /// 玩家运动模式
    /// </summary>
    MovementMode playerMovementMode = MovementMode.NotMoveNotLook;
    /// <summary>
    /// 摄像机
    /// </summary>
    Transform eyes;
    public Transform PlayerEyes
    {
        get { return eyes; }
    }

    /// <summary>
    /// 前后左右移动速度
    /// </summary>
    float crossmoveSpeed = 3.5f;
    /// <summary>
    /// 上下移动速度
    /// </summary>
    float upAndDownmoveSpeed = 1.5f;
    /// <summary>
    /// 键盘旋转视角速度
    /// </summary>
    float keylookRoateSpeed = 50.0f;
    /// <summary>
    /// 鼠标水平转动视角速度
    /// </summary>
    float mouseXRotateSpeed = 50.0f;
    /// <summary>
    /// 鼠标垂直转动视角速度
    /// </summary>
    float mouseYRotateSpeed = 50.0f;

    float MinimumX = -75; 
    float MaximumX = 75;
    /// <summary>
    /// 玩家的目标位置
    /// </summary>
    Vector3 destinationPos = new Vector3();
    /// <summary>
    /// 玩家躯干目标角度
    /// </summary>
    Quaternion destinationRotateF ;
    /// <summary>
    /// 玩家头部目标角度
    /// </summary>
    Quaternion destinationRotateC;
    /// <summary>
    /// 头部旋转增量
    /// </summary>
    float headXDelta;
    /// <summary>
    /// 躯干旋转增量
    /// </summary>
    float bodyYDelta;
    /// <summary>
    /// 玩家行进控制器
    /// </summary>
    CharacterController playerCon;
    /// <summary>
    /// 输入事件管理器
    /// </summary>
    InputManager_CF inputManager_CF;
    /// <summary>
    /// 鼠标指针管理器
    /// </summary>
    MouseController_CF mcon;
    /// <summary>
    /// 暂停游戏标志位
    /// </summary>
    bool pauseGame = false;
    /// <summary>
    /// 退出界面，也兼有暂停游戏功能
    /// </summary>
    EscapePanelScript_CF escape;



    //矩阵变换动画功能用到的变量
    /// <summary>
    /// 矩阵变换动画模式
    /// </summary>
    ObjectTransformAnimationMode transformAnimationMode = ObjectTransformAnimationMode.P_R;
    /// <summary>
    ///矩阵路径动画节点纪录列表
    /// </summary>
    List<Transform> pathAnimationTransformList = new List<Transform>();
    /// <summary>
    ///矩阵路径动画片段所需时间纪录列表
    /// </summary>
    List<float> pathAnimationTimeList = new List<float>();
    /// <summary>
    /// 两点间空间分割计数器
    /// </summary>
    int destTransformCounter = 0;
    /// <summary>
    /// 矩阵路径动画是否从自身开始
    /// </summary>
    bool pathAnimationFromSelf = false;
    /// <summary>
    /// 从磁盘加载到的动画资源
    /// </summary>
    UnityEngine.Object pathAnimationSource;




    //游戏内的一些事件
    /// <summary>
    /// 玩家矩阵路径动画开始事件
    /// </summary>
    public event Action pathAnimationStartEvent;
    /// <summary>
    /// 玩家矩阵路径动画开始事件列表
    /// </summary>
    List<Action> disposablepathAnimationStarEventList = new List<Action>();
    /// <summary>
    /// 玩家矩阵路径动画开始事件列表
    /// </summary>
    public void AddDisposablepathAnimationStarEvent(Action ac)
    {
        disposablepathAnimationStarEventList.Add(ac);
    }

    /// <summary>
    /// 玩家矩阵路径动画结束事件
    /// </summary>
    public event Action pathAnimationEndEvent;
    /// <summary>
    /// 玩家矩阵路径动画结束事件列表
    /// </summary>
    List<Action> disposablepathAnimationEndEventList = new List<Action>();
    /// <summary>
    /// 玩家矩阵路径动画结束事件列表
    /// </summary>
    public void AddDisposablepathAnimationEndEvent(Action ac)
    {
        disposablepathAnimationEndEventList.Add(ac);
    }



    private void Awake()
    {
        thistransform = transform;
        eyes = thistransform.Find("Eyes");
        playerCon = transform.GetComponent<CharacterController>();
        inputManager_CF = FindObjectOfType<InputManager_CF>();
        mcon = FindObjectOfType<MouseController_CF>();
        escape = FindObjectOfType<EscapePanelScript_CF>();
        if (escape == null)
        {
            Debug.LogError("退出界面不存在或者获取失败");
        }
        else
        {
            escape.panelActiveEvent += PauseGameFunc;
            escape.panelDisActiveEvent += ReplayGameFunc;
        }
    }

    // Use this for initialization
    void Start () {
        destinationRotateF = thistransform.localRotation;
        destinationRotateC = eyes.localRotation;
    }

    /// <summary>
    /// 计算玩家输入控制数据更新计算
    /// </summary>
    private void Update()
    {
        if (pauseGame)
        {
            return;
        }
        else
        {
            PlayerInputDataCalculate();
        }
    }
    /// <summary>
    /// 更新玩家位置和角度，在这里更新不会抖动
    /// </summary>
    private void LateUpdate()
    {
        if (pauseGame)
        {
            return;
        }
        else
        {
            PlayerTransformChangeFunc();
        }
    }
    /// <summary>
    /// 暂停游戏
    /// </summary>
    void PauseGameFunc()
    {
        pauseGame = true;
    }
    /// <summary>
    /// 恢复游戏
    /// </summary>
    void ReplayGameFunc()
    {
        pauseGame = false;
    }


    /// <summary>
    /// 玩家位置和角度更新函数
    /// </summary>
    private void PlayerTransformChangeFunc()
    {
        switch (playerMovementMode)
        {
            case MovementMode.CanMoveCanLook:
                playerCon.Move(destinationPos);
                destinationRotateF = thistransform.localRotation;
                destinationRotateC = eyes.localRotation;
                destinationRotateF *=Quaternion.Euler(0,bodyYDelta,0);
                destinationRotateC*= Quaternion.Euler(headXDelta, 0, 0);
                destinationRotateC = ClampRotationAroundXAxis(destinationRotateC);
                thistransform.localRotation = destinationRotateF;
                eyes.localRotation=destinationRotateC;
                bodyYDelta = 0;
                headXDelta = 0;
                break;

            case MovementMode.CanMoveNotLook:
                playerCon.Move(destinationPos);
                break;

            case MovementMode.NotMoveCanLook:
                destinationRotateF = thistransform.localRotation;
                destinationRotateC = eyes.localRotation;
                destinationRotateF *= Quaternion.Euler(0, bodyYDelta, 0);
                destinationRotateC *= Quaternion.Euler(headXDelta, 0, 0);
                destinationRotateC = ClampRotationAroundXAxis(destinationRotateC);
                thistransform.localRotation = destinationRotateF;
                eyes.localRotation = destinationRotateC;
                bodyYDelta = 0;
                headXDelta = 0;
                break;

            case MovementMode.NotMoveNotLook:
                break;
        }
    }
    /// <summary>
    /// 不能动也不能移动镜头
    /// </summary>
    public void Dead()
    {
        playerMovementMode = MovementMode.NotMoveNotLook;
    }
    /// <summary>
    /// 能动也能移动镜头
    /// </summary>
    public void Life()
    {
        playerMovementMode = MovementMode.CanMoveCanLook;
    }
    /// <summary>
    /// 不能动能移动镜头
    /// </summary>
    public void BrokenLeg()
    {
        playerMovementMode = MovementMode.NotMoveCanLook;
    }
    /// <summary>
    /// 能动但不能移动镜头
    /// </summary>
    public void CervicalSpondylitis()
    {
        playerMovementMode = MovementMode.CanMoveNotLook;
    }


    /// <summary>
    /// 玩家相机渲染所有物体
    /// </summary>
    public void OpenEyes()
    {
        Camera.main.clearFlags = CameraClearFlags.Skybox;
        Camera.main.cullingMask = -1;
    }
    /// <summary>
    /// 玩家相机不渲染任何物体
    /// </summary>
    public void CloseEyes()
    {
        Camera.main.clearFlags = CameraClearFlags.Depth;
        Camera.main.cullingMask = 0;
    }
    /// <summary>
    /// 玩家输入控制数据计算
    /// </summary>
    public void PlayerInputDataCalculate()
    {
        switch (playerMovementMode)
        {
            case MovementMode.CanMoveCanLook:
                CrossMove();
                KeyLookFunc();
                MouseLookFunc();
                break;

            case MovementMode.CanMoveNotLook:
                CrossMove();
                break;

            case MovementMode.NotMoveCanLook:
                KeyLookFunc();
                MouseLookFunc();
                break;

            case MovementMode.NotMoveNotLook:
                break;
        }

    }

    /// <summary>
    /// 前后左右上下移动
    /// </summary>
    private void CrossMove()
    {
        float xMove=inputManager_CF.HorizontalAxis*crossmoveSpeed*Time.deltaTime;
        float yMove = inputManager_CF.VerticalAxis * crossmoveSpeed * Time.deltaTime;
        float h = inputManager_CF.UpAndDownAxis * upAndDownmoveSpeed * Time.deltaTime;
        Vector3 forwardMove = thistransform.forward * yMove;
        Vector3 rightMove = thistransform.right * xMove;
        Vector3 upMove = thistransform.up * h;
        Vector3 delta = forwardMove + rightMove+upMove;
        destinationPos = delta;
    }
    /// <summary>
    /// 键盘控制人物旋转
    /// </summary>
     void KeyLookFunc()
    {
        if(mcon.MouseStateProperty)
        {
            float r = inputManager_CF.KeyRotateAxis * keylookRoateSpeed * Time.deltaTime;
            bodyYDelta = r;
        }

    }
    /// <summary>
    /// 鼠标控制人物旋转
    /// </summary>
    void MouseLookFunc()
    {
        if (mcon.MouseStateProperty.Equals(false))
        {
            float xMove = 0;
            float yMove = 0;
            xMove = inputManager_CF.MouseX * mouseXRotateSpeed * Time.deltaTime;
            yMove = inputManager_CF.MouseY * mouseYRotateSpeed * Time.deltaTime * -1;
            bodyYDelta = xMove;
            headXDelta = yMove;
        }
    }
    /// <summary>
    /// 转化角度,防止角度转换出错
    /// </summary>
    /// <param name="value"></param>
    /// <returns></returns>
    public float CheckAngle(float value)
    {
        float result = 0;
        if(Mathf.Abs(value) > 180)
        {
            if(value>0)
            {
                result = value-360;
            }
            else
            {
                result = value + 360;
            }
        }
        else
        {
            result = value;
        }

        return result;
    }

    /// <summary>
    /// 限制夹角
    /// </summary>
    /// <param name="q"></param>
    /// <returns></returns>
    Quaternion ClampRotationAroundXAxis(Quaternion q)
    {
        q.x /= q.w;
        q.y /= q.w;
        q.z /= q.w;
        q.w = 1.0f;

        float angleX = 2.0f * Mathf.Rad2Deg * Mathf.Atan(q.x);

        angleX = Mathf.Clamp(angleX, MinimumX, MaximumX);

        q.x = Mathf.Tan(0.5f * Mathf.Deg2Rad * angleX);

        return q;
    }


    /// <summary>
    /// 设置玩家位置和角度
    /// </summary>
    /// <param name="Position"></param>
    /// <param name="Rotation"></param>
    public void SetPlayerTransform(Vector3 Position,Vector3 Rotation)
    {
        thistransform.localPosition = Position;
        float y = Rotation.y;
        float x = Rotation.x;
        thistransform.localRotation = Quaternion.Euler(0, y, 0);
        eyes.localRotation = Quaternion.Euler(x, 0, 0);
    }
    /// <summary>
    /// 设置玩家位置和角度
    /// </summary>
    /// <param name="Position"></param>
    /// <param name="Rotation"></param>
    public void SetPlayerTransform(Transform t)
    {
        thistransform.localPosition = t.localPosition;
        float y = t.localEulerAngles.y;
        float x = t.localEulerAngles.x;
        thistransform.localRotation = Quaternion.Euler(0, y, 0);
        eyes.localRotation = Quaternion.Euler(x, 0, 0);
    }

    /// <summary>
    /// 设置玩家位置和角度
    /// </summary>
    /// <param name="Position"></param>
    /// <param name="Rotation"></param>
    public void SetPlayerTransform(string name)
    {
        Transform t = GameObject.Find(name).transform;
        thistransform.localPosition = t.localPosition;
        float y = t.localEulerAngles.y;
        float x = t.localEulerAngles.x;
        thistransform.localRotation = Quaternion.Euler(0, y, 0);
        eyes.localRotation = Quaternion.Euler(x, 0, 0);
    }







    /// <summary>
    /// 播放矩阵路径动画从自身矩阵，参数从场景中来
    /// </summary>
    public void PlayPathAnimationFromSelf(Transform animationParent,float delay=0)
    {
        pathAnimationFromSelf = true;
        SetPathAnimationTransformList(animationParent);

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

        StartCoroutine("MoveFunc", delay);
    }

    /// <summary>
    /// 播放矩阵路径动画从自身矩阵,参数动态加载而来
    /// </summary>
    public void PlayPathAnimationFromSelf(string address, float delay = 0)
    {
        pathAnimationSource = Resources.Load(address);
        pathAnimationFromSelf = true;
        SetPathAnimationTransformList((pathAnimationSource as GameObject).transform);

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


        StartCoroutine("MoveFunc", delay);
    }




    /// <summary>
    /// 播放矩阵路径动画从指定开始点矩阵，参数从场景中来
    /// </summary>
    public void PlayPathAnimationFromStart(Transform animationParent, float delay = 0)
    {
        pathAnimationFromSelf = false;
        SetPathAnimationTransformList(animationParent);

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

        StartCoroutine("MoveFunc", delay);
    }

    /// <summary>
    /// 播放矩阵路径动画从指定开始点矩阵,参数动态加载而来
    /// </summary>
    public void PlayPathAnimationFromStart(string address, float delay = 0)
    {
        pathAnimationSource = Resources.Load(address);
        pathAnimationFromSelf = false;
        SetPathAnimationTransformList((pathAnimationSource as GameObject).transform);

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

        StartCoroutine("MoveFunc", delay);
    }


    /// <summary>
    /// 结束矩阵路径动画
    /// </summary>
    void CompletePathAnimation()
    {
        StopCoroutine("MoveFunc");
        pathAnimationTransformList.Clear();
        pathAnimationTimeList.Clear();
        destTransformCounter = 0;
        EventCenter_CF.Instance.OpenInteractiveControl();
        Vector3 pos = thistransform.localPosition;
        Vector3 angle = thistransform.localEulerAngles;
        Vector3 yAngle = new Vector3(0, angle.y, 0);
        Vector3 xAngle = new Vector3(angle.x, 0, 0);
        thistransform.localPosition = pos;
        thistransform.localEulerAngles = yAngle;
        eyes.localEulerAngles = xAngle;
        pathAnimationSource = null;
        Resources.UnloadUnusedAssets();

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


    }
    /// <summary>
    /// 设置矩阵路径动画所需要的路径点和对应的所需时间 
    /// </summary>
    void SetPathAnimationTransformList(Transform pathAnimation)
    {
        if(pathAnimationTransformList.Count>0)
        {
            pathAnimationTransformList.Clear();
        }
        if(pathAnimationTimeList.Count>0)
        {
            pathAnimationTimeList.Clear();
        }
        for (int i = 0; i < pathAnimation.childCount; i++)
        {
            pathAnimationTransformList.Add(pathAnimation.GetChild(i));
            string timeText=pathAnimation.GetChild(i).name.Split('_')[1];
            float time = 0;
            if(float.TryParse(timeText, out time))
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
                if(i!=pathAnimation.childCount-1)
                {
                    throw new Exception("矩阵路径动画中名为"+pathAnimation.GetChild(i).name+",索引号为"+i+"的位置节点的动画片段所需时间填写有误,请注意格式");
                }
            }

        }
        if(pathAnimationTransformList.Count!=pathAnimation.childCount)
        {
            throw new Exception("矩阵路径动画transform节点列表数量与动画父物体的子物体数量不一致");
        }

        if (pathAnimationTimeList.Count != pathAnimation.childCount-1)
        {
            throw new Exception("矩阵路径动画片段列表数量与动画父物体的子物体数量不匹配");
        }


    }

    /// <summary>
    /// 动画矩阵更新函数
    /// </summary>
    /// <param name="t"></param>
    /// <returns></returns>
    IEnumerator MoveFunc(float t)
    {
        float timer = 0;
        float waitRate = 0.1f;
        while(timer<t)
        {
            if(pauseGame)
            {
                yield return new WaitForSeconds(waitRate);
            }
            else
            {
                timer += waitRate;
                yield return new WaitForSeconds(waitRate);
            }
        }
        //记录玩家在动画播放前所处的位置
        Vector3 recordPlayerPosition = thistransform.localPosition;
        Vector3 recordPlayerRotation = thistransform.localEulerAngles;
        Vector3 recordPlayerEyesRotation = eyes.localEulerAngles;
        Vector3 recordPlayerScale = thistransform.localScale;
        MovementMode tempmode = playerMovementMode;
        //玩家眼睛镜头归置并限制移动能力
        if(pathAnimationFromSelf)
        {
            pathAnimationTransformList[0].localPosition = recordPlayerPosition;
            pathAnimationTransformList[0].localEulerAngles = new Vector3(recordPlayerEyesRotation.x, recordPlayerRotation.y, 0);
            pathAnimationTransformList[0].localScale = recordPlayerScale;
            thistransform.localEulerAngles = new Vector3(recordPlayerEyesRotation.x, recordPlayerRotation.y, 0);
        }
        Dead();
        eyes.localRotation = Quaternion.Euler(0, 0, 0);
        EventCenter_CF.Instance.CloseInteractiveControl();
        for (int i = 0; i < pathAnimationTransformList.Count-1; i++)
        {
            Transform nodeStarTransform;
            Transform nodeEndTransform;
            Vector3 diffrencePosition;
            Vector3 differenceRotation;
            Vector3 diffrenceScale;

            switch (transformAnimationMode)
            {
                case ObjectTransformAnimationMode.P_R_S:
                     destTransformCounter = (int)(pathAnimationTimeList[i]/Time.fixedDeltaTime);
                     nodeStarTransform = pathAnimationTransformList[i];
                     nodeEndTransform = pathAnimationTransformList[i+1];
                     diffrencePosition = nodeEndTransform.localPosition - nodeStarTransform.localPosition;
                    differenceRotation = nodeEndTransform.localEulerAngles - nodeStarTransform.localEulerAngles;
                    differenceRotation = new Vector3(CheckAngle(differenceRotation.x), CheckAngle(differenceRotation.y), CheckAngle(differenceRotation.z));
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
                    }
                    break;
                case ObjectTransformAnimationMode.R:
                    destTransformCounter = (int)(pathAnimationTimeList[i] / Time.fixedDeltaTime);
                    nodeStarTransform = pathAnimationTransformList[i];
                    nodeEndTransform = pathAnimationTransformList[i + 1];
                    differenceRotation = nodeEndTransform.localEulerAngles - nodeStarTransform.localEulerAngles;
                    differenceRotation = new Vector3(CheckAngle(differenceRotation.x), CheckAngle(differenceRotation.y), CheckAngle(differenceRotation.z));
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
                    differenceRotation =nodeEndTransform.localEulerAngles-nodeStarTransform.localEulerAngles;
                    differenceRotation = new Vector3(CheckAngle(differenceRotation.x), CheckAngle(differenceRotation.y), CheckAngle(differenceRotation.z));
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
                    differenceRotation = new Vector3(CheckAngle(differenceRotation.x), CheckAngle(differenceRotation.y), CheckAngle(differenceRotation.z));
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
        playerMovementMode = tempmode;
        CompletePathAnimation();

    }









    private void OnDestroy()
    {
        escape.panelActiveEvent -= PauseGameFunc;
        escape.panelDisActiveEvent -= ReplayGameFunc;
    }



}

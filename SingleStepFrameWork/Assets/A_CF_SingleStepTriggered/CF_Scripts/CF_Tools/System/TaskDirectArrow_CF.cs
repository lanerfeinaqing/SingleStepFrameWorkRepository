using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TaskDirectArrow_CF : MonoBehaviour {

     Transform destination;
     RectTransform rectTransform;
     Camera player;
     PlayerMoveMentController_CF playerMoveMent;
     Image img;
     Text distanceTitleText;

    Color yellow = Color.yellow;
    Color grayYellow = new Color(0.5f, 0.5f, 0, 1);

    /// <summary>
    /// 三维物体的视口坐标
    /// </summary>
    Vector3 viewPoint = new Vector3();
    /// <summary>
    /// 目标点和玩家相机在X、Z平面上的夹角判断左右,此数为大于0，在右边
    /// </summary>
    float leftOrRight = 0;
    /// <summary>
    /// 目标点和玩家相机在X、Y平面上的夹角判断上下
    /// </summary>
    float upOrDown = 0;
    /// <summary>
    /// 箭头在屏幕上的坐标
    /// </summary>
    Vector3 arrowPosition = new Vector3(0, 0, 0);
    /// <summary>
    /// 箭头在屏幕上的旋转
    /// </summary>
    Vector3 arrowRotation = new Vector3(0, 0, 0);

    int layerMask = (1 << 2);
    Vector3 textPosition = new Vector3();
    int resolutionX = 0;
    int resolutionY = 0;

    private void Awake()
    {
        img = GetComponent<Image>();
        rectTransform = transform as RectTransform;
        distanceTitleText = rectTransform.parent.GetComponentInChildren<Text>();
    }

    // Use this for initialization
    void Start()
    {
        layerMask = ~layerMask;
        distanceTitleText.enabled = false;

    }

    // Update is called once per frame
    void Update()
    { //水平判断，z值大于0的情况下，x值大于等于0小于等于1的情况下，y值小于等于1大于等于0的情况下，默认显示即可。


        if (img.enabled.Equals(false))
        {
            return;
        }


        if (GlobalProperty_CF.sceneMode == SceneMode.lianxi)
        {
            if (destination != null)
            {
                if (player != null)
                {
                    resolutionX = Screen.width;
                    resolutionY = Screen.height;
                    // print(resolutionX + "——" + resolutionY);   932.6    504.8
                    float arrowHalf = ((float)resolutionX) / 1920f * 33f;
                    float widthBorder = resolutionX - arrowHalf;
                    float heightBorder = resolutionY - arrowHalf;
                    viewPoint = player.WorldToViewportPoint(destination.position);

                    //玩家正对
                    if (viewPoint.z > 0)
                    {
                        //横在竖在
                        if (viewPoint.x >= 0 && viewPoint.x <= 1 && viewPoint.y <= 1 && viewPoint.y >= 0)
                        {
                            float x = Mathf.Clamp(viewPoint.x * resolutionX, arrowHalf, widthBorder);
                            float y = Mathf.Clamp(viewPoint.y * resolutionY, arrowHalf, heightBorder);
                            arrowPosition.x = x;
                            arrowPosition.y = y;
                            arrowRotation.z = -90;
                            rectTransform.position = arrowPosition;
                            rectTransform.eulerAngles = arrowRotation;

                            //玩家和目标点之间存在阻碍物的话，颜色切换为灰色
                            Vector3 temp = destination.position - player.transform.position;
                            float dis = temp.magnitude;
                            Ray myRay = new Ray(player.transform.position, temp);
                            RaycastHit hitInfo = new RaycastHit();
                            if (Physics.Raycast(myRay, out hitInfo, dis, layerMask))
                            {
                                //img.color = grayYellow;
                                if (distanceTitleText.enabled == false)
                                {
                                    distanceTitleText.enabled = true;
                                    // print(hitInfo.collider.gameObject.name);
                                }
                            }
                            else
                            {
                                //img.color = yellow;
                                if (distanceTitleText.enabled)
                                {
                                    distanceTitleText.enabled = false;
                                }
                            }

                            textPosition.x = x;
                            textPosition.y = y - arrowHalf - (((float)resolutionX) / 1920f * 15f);
                            distanceTitleText.rectTransform.position = textPosition;




                        }
                        //横在竖不在
                        else if (viewPoint.x >= 0 && viewPoint.x <= 1 && (viewPoint.y > 1 || viewPoint.y < 0))
                        {
                            //计算X、Y平面上的夹角
                            Vector3 vertical = destination.position - player.transform.position;
                            Vector3 verticalProject = Vector3.ProjectOnPlane(vertical, player.transform.right);
                            Vector3 verticalCross = Vector3.Cross(player.transform.forward, verticalProject);
                            upOrDown = Vector3.Dot(player.transform.right, verticalCross) * -1;

                            float x = Mathf.Clamp(viewPoint.x * resolutionX, arrowHalf, widthBorder);
                            float y = 0;
                            if (upOrDown > 0)
                            {
                                y = resolutionY - arrowHalf;
                                arrowRotation.z = 90;
                            }
                            else
                            {
                                y = arrowHalf;
                                arrowRotation.z = -90;
                            }
                            arrowPosition.x = x;
                            arrowPosition.y = y;
                            rectTransform.position = arrowPosition;
                            rectTransform.eulerAngles = arrowRotation;
                            if (distanceTitleText.enabled)
                            {
                                distanceTitleText.enabled = false;
                            }

                        }
                        //横不在竖在
                        else if ((viewPoint.x < 0 || viewPoint.x > 1) && viewPoint.y <= 1 && viewPoint.y >= 0)
                        {
                            //计算X、Z平面上的夹角
                            Vector3 horizontal = new Vector3(destination.position.x, 0, destination.position.z) - new Vector3(playerMoveMent.transform.position.x, 0, playerMoveMent.transform.position.z);
                            Vector3 horizontalCross = Vector3.Cross(playerMoveMent.transform.forward, horizontal).normalized;
                            leftOrRight = horizontalCross.y;


                            float x = 0;
                            if (leftOrRight > 0)
                            {
                                x = widthBorder;
                                arrowRotation.z = 0;
                            }
                            else
                            {
                                x = arrowHalf;
                                arrowRotation.z = 180;
                            }
                            float y = Mathf.Clamp(viewPoint.y * resolutionY, arrowHalf, heightBorder);
                            arrowPosition.x = x;
                            arrowPosition.y = y;
                            rectTransform.position = arrowPosition;
                            rectTransform.eulerAngles = arrowRotation;
                            if (distanceTitleText.enabled)
                            {
                                distanceTitleText.enabled = false;
                            }

                        }
                        //横竖都不在
                        else if ((viewPoint.x < 0 || viewPoint.x > 1) && (viewPoint.y > 1 || viewPoint.y < 0))
                        {
                            //计算X、Z平面上的夹角
                            Vector3 horizontal = new Vector3(destination.position.x, 0, destination.position.z) - new Vector3(playerMoveMent.transform.position.x, 0, playerMoveMent.transform.position.z);
                            Vector3 horizontalCross = Vector3.Cross(playerMoveMent.transform.forward, horizontal).normalized;
                            leftOrRight = horizontalCross.y;
                            //计算X、Y平面上的夹角
                            Vector3 vertical = destination.position - player.transform.position;
                            Vector3 verticalProject = Vector3.ProjectOnPlane(vertical, player.transform.right);
                            Vector3 verticalCross = Vector3.Cross(player.transform.forward, verticalProject);
                            upOrDown = Vector3.Dot(player.transform.right, verticalCross) * -1;

                            float x = 0;
                            if (leftOrRight > 0)
                            {
                                x = widthBorder;
                                if (upOrDown > 0)
                                {
                                    arrowRotation.z = 45;
                                }
                                else
                                {
                                    arrowRotation.z = -45;
                                }
                            }
                            else
                            {
                                x = arrowHalf;
                                if (upOrDown > 0)
                                {
                                    arrowRotation.z = 135;
                                }
                                else
                                {
                                    arrowRotation.z = -135;
                                }
                            }
                            float y = 0;
                            if (upOrDown > 0)
                            {
                                y = heightBorder;
                                if (leftOrRight > 0)
                                {
                                    arrowRotation.z = 45;
                                }
                                else
                                {
                                    arrowRotation.z = 135;
                                }
                            }
                            else
                            {
                                y = arrowHalf;
                                if (leftOrRight > 0)
                                {
                                    arrowRotation.z = -45;
                                }
                                else
                                {
                                    arrowRotation.z = -135;
                                }

                            }
                            arrowPosition.x = x;
                            arrowPosition.y = y;
                            rectTransform.position = arrowPosition;
                            rectTransform.eulerAngles = arrowRotation;
                            if (distanceTitleText.enabled)
                            {
                                distanceTitleText.enabled = false;
                            }
                        }
                    }
                    //玩家背对
                    else
                    {
                        if (distanceTitleText.enabled)
                        {
                            distanceTitleText.enabled = false;
                        }
                        //横在竖在
                        if (viewPoint.x >= 0 && viewPoint.x <= 1 && viewPoint.y <= 1 && viewPoint.y >= 0)
                        {
                            float x = 0;
                            float y = 0;
                            if (viewPoint.x > 0.5f)
                            {
                                x = arrowHalf;
                                if (viewPoint.y > 0.5f)
                                {
                                    y = heightBorder;
                                    arrowRotation.z = 135;
                                }
                                else
                                {
                                    y = arrowHalf;
                                    arrowRotation.z = -135;
                                }
                            }
                            else
                            {
                                x = widthBorder;
                                if (viewPoint.y > 0.5f)
                                {
                                    y = heightBorder;
                                    arrowRotation.z = 45;
                                }
                                else
                                {
                                    y = arrowHalf;
                                    arrowRotation.z = -45;
                                }
                            }
                            arrowPosition.x = x;
                            arrowPosition.y = y;
                            rectTransform.position = arrowPosition;
                            rectTransform.eulerAngles = arrowRotation;
                        }
                        //横在竖不在
                        else if (viewPoint.x >= 0 && viewPoint.x <= 1 && (viewPoint.y > 1 || viewPoint.y < 0))
                        {
                            //计算X、Y平面上的夹角
                            Vector3 vertical = destination.position - player.transform.position;
                            Vector3 verticalProject = Vector3.ProjectOnPlane(vertical, player.transform.right);
                            Vector3 verticalCross = Vector3.Cross(player.transform.forward, verticalProject);
                            upOrDown = Vector3.Dot(player.transform.right, verticalCross);


                            float x = Mathf.Clamp(resolutionX - viewPoint.x * resolutionX, arrowHalf, widthBorder);
                            float y = 0;
                            if (upOrDown > 0)
                            {
                                y = heightBorder;
                                arrowRotation.z = 90;
                            }
                            else
                            {
                                y = arrowHalf;
                                arrowRotation.z = -90;
                            }
                            arrowPosition.x = x;
                            arrowPosition.y = y;
                            rectTransform.position = arrowPosition;
                            rectTransform.eulerAngles = arrowRotation;


                        }
                        //横不在竖在
                        else if ((viewPoint.x < 0 || viewPoint.x > 1) && viewPoint.y <= 1 && viewPoint.y >= 0)
                        {
                            //计算X、Z平面上的夹角
                            Vector3 horizontal = new Vector3(destination.position.x, 0, destination.position.z) - new Vector3(playerMoveMent.transform.position.x, 0, playerMoveMent.transform.position.z);
                            Vector3 horizontalCross = Vector3.Cross(playerMoveMent.transform.forward, horizontal).normalized;
                            leftOrRight = horizontalCross.y;


                            float x = 0;
                            if (leftOrRight > 0)
                            {
                                x = widthBorder;
                                arrowRotation.z = 0;
                            }
                            else
                            {
                                x = arrowHalf;
                                arrowRotation.z = 180;
                            }
                            float y = Mathf.Clamp(viewPoint.y * resolutionY, arrowHalf, heightBorder);
                            arrowPosition.x = x;
                            arrowPosition.y = y;
                            rectTransform.position = arrowPosition;
                            rectTransform.eulerAngles = arrowRotation;
                        }
                        //横竖都不在
                        else if ((viewPoint.x < 0 || viewPoint.x > 1) && (viewPoint.y > 1 || viewPoint.y < 0))
                        {
                            //计算X、Z平面上的夹角
                            Vector3 horizontal = new Vector3(destination.position.x, 0, destination.position.z) - new Vector3(playerMoveMent.transform.position.x, 0, playerMoveMent.transform.position.z);
                            Vector3 horizontalCross = Vector3.Cross(playerMoveMent.transform.forward, horizontal).normalized;
                            leftOrRight = horizontalCross.y;
                            //计算X、Y平面上的夹角
                            Vector3 vertical = destination.position - player.transform.position;
                            Vector3 verticalProject = Vector3.ProjectOnPlane(vertical, player.transform.right);
                            Vector3 verticalCross = Vector3.Cross(player.transform.forward, verticalProject);
                            upOrDown = Vector3.Dot(player.transform.right, verticalCross);


                            float x = 0;
                            if (leftOrRight > 0)
                            {
                                x = widthBorder;
                                if (upOrDown > 0)
                                {
                                    arrowRotation.z = 45;
                                }
                                else
                                {
                                    arrowRotation.z = -45;
                                }
                            }
                            else
                            {
                                x = arrowHalf;
                                if (upOrDown > 0)
                                {
                                    arrowRotation.z = 135;
                                }
                                else
                                {
                                    arrowRotation.z = -135;
                                }
                            }
                            float y = 0;
                            if (upOrDown > 0)
                            {
                                y = heightBorder;
                                if (leftOrRight > 0)
                                {
                                    arrowRotation.z = 45;
                                }
                                else
                                {
                                    arrowRotation.z = 135;
                                }
                            }
                            else
                            {
                                y = arrowHalf;
                                if (leftOrRight > 0)
                                {
                                    arrowRotation.z = -45;
                                }
                                else
                                {
                                    arrowRotation.z = -135;
                                }

                            }
                            arrowPosition.x = x;
                            arrowPosition.y = y;
                            rectTransform.position = arrowPosition;
                            rectTransform.eulerAngles = arrowRotation;
                        }
                    }


                }
                else
                {
                    playerMoveMent = FindObjectOfType<PlayerMoveMentController_CF>();
                    player = playerMoveMent.ThisTransform.GetComponentInChildren<Camera>();
                }
            }
        }

    }

    public void Open(string name)
    {

        if (GlobalProperty_CF.sceneMode == SceneMode.lianxi)
        {
            img.enabled = true;
            destination = GameObject.Find(name).transform;
            img.color = yellow;
            if (distanceTitleText == null)
            {
                distanceTitleText = rectTransform.parent.GetComponentInChildren<Text>();
            }
        }

    }

    public void Close()
    {
        if (GlobalProperty_CF.sceneMode == SceneMode.lianxi)
        {
            if (distanceTitleText != null)
            {
                distanceTitleText.enabled = false;
            }
            destination = null;
            img.enabled = false;
        }

    }
}

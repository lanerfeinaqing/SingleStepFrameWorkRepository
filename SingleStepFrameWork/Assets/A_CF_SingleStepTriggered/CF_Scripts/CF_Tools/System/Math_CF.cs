using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Math_CF  {

    /// <summary>
    /// 转化角度,防止角度转换出错
    /// </summary>
    /// <param name="value"></param>
    /// <returns></returns>
    public static float CheckAngle(float value)
    {
        float result = 0;
        if (Mathf.Abs(value) > 180)
        {
            if (value > 0)
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
}

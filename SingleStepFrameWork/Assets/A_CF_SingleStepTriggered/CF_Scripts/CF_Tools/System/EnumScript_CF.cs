

public class EnumScript_CF
{

}
/// <summary>
/// 游戏物体矩阵变换动画的枚举状态
/// </summary>
public enum ObjectTransformAnimationMode
{
    P_R_S,
    P,
    R,
    S,
    P_R,
    R_S,
    P_S,
}
/// <summary>
/// 动画播放的三种模式
/// </summary>
public enum AnimationPlayMode
{
    Once,
    Loop,
    PingPong
}

/// <summary>
/// 停止动画播的两种模式
/// </summary>
public enum AnimationStopMode
{
   Rest,
   Keep
}
/// <summary>
/// animator动画播放的两种模式
/// </summary>
public enum AnimatorComponentPlayMode
{
    Trriger,
    Bool
}

/// <summary>
/// 游戏场景的模式，练习还是考核
/// </summary>
public enum SceneMode
{
    empty,
    lianxi,
    kaohe
}









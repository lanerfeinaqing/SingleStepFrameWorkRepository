using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;

public struct ConfiguratioStruct_CF
{
    string tishi;
    string buzhou;
    string fenshu;
    int score;
    /// <summary>
    /// 提示信息
    /// </summary>
    public string TiShi
    {
        get {return tishi; }
    }
    /// <summary>
    /// 步骤信息
    /// </summary>
    public string BuZhou
    {
        get { return buzhou; }
    }
    /// <summary>
    /// 文本分数
    /// </summary>
    public string FenShu
    {
        get { return fenshu; }
    }
    /// <summary>
    /// 整数分数
    /// </summary>
    public int Score
    {
        get { return score; }
    }
    /// <summary>
    /// 初始化对象
    /// </summary>
    /// <param name="_tishi"></param>
    /// <param name="_buzhou"></param>
    /// <param name="_fenshu"></param>
    public ConfiguratioStruct_CF (string _tishi,string _buzhou,string _fenshu)
    {
        int num = Regex.Matches(_tishi, "#").Count;
        if (num>0)
        {
            tishi = "nothing";
        }
        else
        {
            tishi = _tishi;
        }
        buzhou = _buzhou;
        fenshu = _fenshu;
        score = int.Parse(_fenshu);
    }

    public override string ToString()
    {
        string s = "提示信息：" + TiShi + "  步骤信息： " + BuZhou + "  分数信息： " + FenShu;
        return s;
    }


}

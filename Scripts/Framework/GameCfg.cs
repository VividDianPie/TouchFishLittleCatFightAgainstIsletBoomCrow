using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//游戏存档的数据类
public class GameCfg 
{
    //音乐相关设置
    public int MusicOn;

    //音效相关设置
    public int EffectOn;

    //背包数据
    public Dictionary<int, int> BagItems;

    //todo

    public GameCfg()
    {
        BagItems = new Dictionary<int, int>();
        MusicOn = 1;
        EffectOn = 1;
    }

}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//��Ϸ�浵��������
public class GameCfg 
{
    //�����������
    public int MusicOn;

    //��Ч�������
    public int EffectOn;

    //��������
    public Dictionary<int, int> BagItems;

    //todo

    public GameCfg()
    {
        BagItems = new Dictionary<int, int>();
        MusicOn = 1;
        EffectOn = 1;
    }

}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//对象同化
using Object = System.Object;


public enum EEventType
{
    None = -1,
    PlayMusic,
    JoyStickDown, //摇杆刚被按中
    JoyStickDrag, //摇杆儿拖动
    JoyStickUp,   //摇杆抬起来
    BagItemSelect,//背包中一个小物品被选中
    BackBengMeiLeftHandAtkAt,//魂妖刀血樱寂灭击中目标
    BackBengMeiRightHandAtkAt,//雾切之回光击中目标
    ScreenFingerZero,
    DownLook,
    HerrscherOfThunderMeiRightHandWeaPonBeHit,
    HerrscherOfThunderMeiDoubleHandAtkAtEvent,
    HerrscherOfThunderMeiEventSkillBig,
    GameStop,
    GameRun,
    Count,
}

    public class MyEvent
    {
        //事件类型
        public EEventType type;

        public Object data;


        public MyEvent()
        {
            data = null;
        }


        public MyEvent(EEventType t, Object datas)
        {
            type = t;
            data = datas;
        }
    }



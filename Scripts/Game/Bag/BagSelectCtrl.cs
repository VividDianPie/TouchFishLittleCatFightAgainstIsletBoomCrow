using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class BagSelectCtrl : ItemSelectCtrl
{
    //按钮函数 由背包物品点击触发
    new public void OnSelectBtn()
    { 
        //事件数据为 装备   Id 与 Id对应装备数量 监听者为 BagPopup
        EventManager.Instance.DispatchEvent(new MyEvent(EEventType.BagItemSelect, new BagEvtData(excelsToOutFitInformation.Id, mOutfitCount)));
    }
}

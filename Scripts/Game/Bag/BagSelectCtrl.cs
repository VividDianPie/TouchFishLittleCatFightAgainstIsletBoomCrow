using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class BagSelectCtrl : ItemSelectCtrl
{
    //��ť���� �ɱ�����Ʒ�������
    new public void OnSelectBtn()
    { 
        //�¼�����Ϊ װ��   Id �� Id��Ӧװ������ ������Ϊ BagPopup
        EventManager.Instance.DispatchEvent(new MyEvent(EEventType.BagItemSelect, new BagEvtData(excelsToOutFitInformation.Id, mOutfitCount)));
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Config;


public class BagSystem 
{
    //key:��Ʒid value:����
    Dictionary<int, int> dicOutfitIdAndCount;

    public Dictionary<int, int> dicOutfitIdAndCounts
    {
        get
        {
            return dicOutfitIdAndCount;
        }
    }


    static BagSystem sInstance = null;
    public static BagSystem Instance
    {
        get
        {
            if (sInstance == null)
            {
                sInstance = new BagSystem();
            }
            return sInstance;
        }
    }


    BagSystem()
    {
        //���ݼ���
        dicOutfitIdAndCount = GameCfgMgr.Instance.BagItems;
        if (dicOutfitIdAndCount == null)
        {
            dicOutfitIdAndCount = new Dictionary<int, int>();
        }
    }


    //������Ʒ
    public void AddItem(int id, int count)          //  1001      �ٵٵĻ�������                //     1002      ����֮�ع�                   //     1003     ������_Ѫӣ����
    {
        //��������û����Ӧ��Ʒ������
        ExcelsToOutFitInformation outfitInfo = ExcelsToItemHelpClass.Instance.IdToFindOutfitInformation(id);
        if (outfitInfo != null)
        {
            if (dicOutfitIdAndCount.ContainsKey(id))
            {
                dicOutfitIdAndCount[id] += count;
            }
            else
            {
                dicOutfitIdAndCount.Add(id, count);
            }

            if (dicOutfitIdAndCount[id] <= 0)
            {
                dicOutfitIdAndCount.Remove(id);
            }
        }
        else
        {
            throw new System.Exception($"û��id: {id} ������!");
        }
        SaveCfg();
    }


    //�洢����
    public void SaveCfg()
    {
        GameCfgMgr.Instance.SaveBag(dicOutfitIdAndCount);
    }

}
